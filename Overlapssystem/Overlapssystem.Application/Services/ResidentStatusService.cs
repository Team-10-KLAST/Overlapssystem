using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

public class ResidentStatusService : IResidentStatusService
{
    private readonly IAppDbContext _context;

    public ResidentStatusService(IAppDbContext context)
    {
        _context = context;
            
    }

    // Updates status text for a specifik resident schema. 
    // Returns true if found and updated, false if schema doesnt exist 
    // also logs the action in the activity log
    public async Task<bool> UpdateStatusAsync(int residentSchemaId, string newStatus, int employeeId)
    {
        // step 1: find schema by primary key
        var schema = await _context.ResidentSchemas.FindAsync(residentSchemaId);

        // step 2: ifi not found, return false 
        if (schema == null)
        {
            return false;
        }

        // Step 3: update status field 
        schema.Status = newStatus;

        _context.ActivityLogEntries.Add(new ActivityLogEntry
        {
            ActionType = "Status",
            Description = $"Tilføjede til status: {newStatus}",
            Timestamp = DateTime.UtcNow,
            EmployeeId = employeeId,
            ResidentSchemaId = residentSchemaId
        });

        // step 4: save changes to database
        await _context.SaveChangesAsync();

        return true;
    }
}
