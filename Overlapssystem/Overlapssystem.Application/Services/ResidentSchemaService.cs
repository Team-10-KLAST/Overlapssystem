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
    public async Task<IEnumerable<ResidentSchema>> InitializeSchemaForShiftAsync(int shiftId, int departmentId)
    {
        // Find all residents
        var residents = await _context.Residents
            .Where(r => r.DepartmentId == departmentId)
            .ToListAsync();

        // Check if schemas already exists for this shift 
        // Prefvents duplicates if this method is called twice 
        var existingSchemaResidentIds = await _context.ResidentSchemas
            .Where(s => s.ShiftId == shiftId)
            .Select(s => s.ResidentId)
            .ToListAsync();

        var newSchemas = new List<ResidentSchema>();

        foreach(var resident in residents)
        {
            // skip if schema already exists for this resident in this shift
            if (existingSchemaResidentIds.Contains(resident.ResidentId))
                continue;

            // Find most recent schema for this resident
            var previousSchema = await _context.ResidentSchemas
                .Where(s => s.ResidentId == resident.ResidentId && s.ShiftId != shiftId)
                .OrderByDescending(s => s.ShiftId)
                .FirstOrDefaultAsync();

            // Create new schema, copying all firlds from previous shift
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

        // Save all new schemas to database
        _context.ResidentSchemas.AddRange(newSchemas);
        await _context.SaveChangesAsync();

        return newSchemas;
    }    
}
