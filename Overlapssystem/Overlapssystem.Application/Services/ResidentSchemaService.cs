using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

// concrete implementation of IResidentSchemaService 
// Handles the business logic of fetching resident schemas. 
public class ResidentSchemaService : IResidentSchemaService
{
    
    private readonly IAppDbContext _context;

    public ResidentSchemaService(IAppDbContext context)
    {
        _context = context;
    }

    // Fetches all resident schemas for a given shift, including related resident and medication data for that day.
    public async Task<IEnumerable<ResidentSchema>> GetByShiftAsync(int shiftId)
    {
        var shift = await _context.Shifts.FindAsync(shiftId);
        if (shift == null) return Enumerable.Empty<ResidentSchema>();

        var schemas = await _context.ResidentSchemas
            .Where(s => s.ShiftId == shiftId)
            .Include(s => s.Resident)
                .ThenInclude(r => r.MedicationTimes)
            .Include(s => s.PrimaryEmployee)   
            .Include(s => s.SecondaryEmployee) 
            .ToListAsync();

        var shiftIdsOnSameDay = await GetShiftIdsOnSameDayAsync(shift);

        foreach (var schema in schemas)
            schema.MedicationRegistrations = await GetMedicationRegistrationsForResidentAsync(schema.ResidentId, shiftIdsOnSameDay);

        return schemas;
    }

    // Finds all shift IDs on the same date and department as the given shift.
    // Used to fetch medication registrations across all shifts in the same day.
    private async Task<List<int>> GetShiftIdsOnSameDayAsync(Shift shift)
    {
        return await _context.Shifts
            .Where(s => s.Date.Date == shift.Date.Date && s.DepartmentId == shift.DepartmentId)
            .Select(s => s.Id)
            .ToListAsync();
    }

    // Fetches all given medication registrations for a resident across a set of shifts.
    // Includes registrations from all shifts on the same day, not just the current shift.
    private async Task<List<MedicationRegistration>> GetMedicationRegistrationsForResidentAsync(int residentId, List<int> shiftIds)
    {
        return await _context.MedicationRegistrations
            .Where(mr => shiftIds.Contains(mr.ResidentSchema.ShiftId)
                      && mr.ResidentSchema.ResidentId == residentId
                      && mr.Given)
            .ToListAsync();
    }

    // Creates new ResidentSchemas for all residents in a department wehen a new shift starts
    // Find all residents -> Check for existing residentshema -> List of resident ID -> Get previous schemas -> Post info into new schemas
    public async Task<IEnumerable<ResidentSchema>> InitializeSchemaForShiftAsync(int shiftId, int departmentId)
    {        
        var residents = await _context.Residents
            .Where(r => r.DepartmentId == departmentId)
            .ToListAsync();

        var existingSchemaResidentIds = await _context.ResidentSchemas
            .Where(s => s.ShiftId == shiftId)
            .Select(s => s.ResidentId)
            .ToListAsync();

        var residentIds = residents.Select(r => r.ResidentId).ToList();

        var previousSchemasLookup = (await _context.ResidentSchemas
            .Where(s => s.ShiftId != shiftId && residentIds.Contains(s.ResidentId))
            .AsNoTracking()
            .ToListAsync())
            .GroupBy(s => s.ResidentId)
            .ToDictionary(
                g => g.Key,
                g => g.OrderByDescending(s => s.ShiftId).FirstOrDefault()
            );

        var newSchemas = new List<ResidentSchema>();

        foreach(var resident in residents)
        {            
            if (existingSchemaResidentIds.Contains(resident.ResidentId))
                continue;

            previousSchemasLookup.TryGetValue(resident.ResidentId, out var previousSchema);

            var newSchema = new ResidentSchema
            {
                ResidentId = resident.ResidentId,
                ShiftId = shiftId,
                TrafficLight = previousSchema?.TrafficLight,
                Status = previousSchema?.Status ?? string.Empty,
                RelativeContact = previousSchema?.RelativeContact ?? string.Empty
            };

            newSchemas.Add(newSchema);          
        }

        _context.ResidentSchemas.AddRange(newSchemas);
        await _context.SaveChangesAsync();

        return newSchemas;
    }    
}
