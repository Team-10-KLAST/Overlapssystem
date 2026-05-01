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


    // Fetches all resident schemas for a specifik shift. 
    // iincludes related Resident to read initials etc. 
    public async Task<IEnumerable<ResidentSchema>> GetByShiftAsync(int shiftId)
    {
        return await _context.ResidentSchemas
            .Where(s=>s.ShiftId == shiftId)
            .Include(s => s.Resident)
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

            // TODO: Uncomment when MedicationRegistration entity is available.
            // Copies medication records so the new team can see what was given earlier.
            //
            // if (previousSchema != null)
            // {
            //     var previousMedications = await _context.MedicationRegistrations
            //         .Where(m => m.ResidentSchemaId == previousSchema.ResidentSchemaId)
            //         .ToListAsync();
            //
            //     foreach (var med in previousMedications)
            //     {
            //         _context.MedicationRegistrations.Add(new MedicationRegistration
            //         {
            //             ResidentSchemaId = newSchema.ResidentSchemaId,
            //             GivenTime = med.GivenTime,
            //             GivenBy = med.GivenBy,
            //             Given = med.Given
            //         });
            //     }
            // }

            // TODO: Uncomment when PN entity is available.
            // Copies PN records so the new team can see what was given.
            //
            // if (previousSchema != null)
            // {
            //     var previousPNs = await _context.PNs
            //         .Where(p => p.ResidentSchemaId == previousSchema.ResidentSchemaId)
            //         .ToListAsync();
            //
            //     foreach (var pn in previousPNs)
            //     {
            //         _context.PNs.Add(new PN
            //         {
            //             ResidentSchemaId = newSchema.ResidentSchemaId,
            //             GivenTime = pn.GivenTime,
            //             GivenBy = pn.GivenBy
            //         });
            //     }
            // }

           

        }

        // Save all new schemas to databsae
        _context.ResidentSchemas.AddRange(newSchemas);
        await _context.SaveChangesAsync();

        return newSchemas;

    }

    
}
