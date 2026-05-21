using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

// Handles saving and retrieving activity log entries.
// Each action (medication, traffic light, status, PN) is stored
// as a separate row in the ActivityLogEntries table.
// This ensures full history — nothing is overwritten.
public class ActivityLogService : IActivityLogService
{
    private readonly IAppDbContext _context;

    // Constructor — receives the database context via dependency injection.
    public ActivityLogService(IAppDbContext context)
    {
        _context = context;
    }

    // Adds a new log entry to the database.
    // Called by other services (MedicationService, TrafficLightService, etc.)
    // whenever an employee performs a loggable action. 
    public Task AddLogEntry(string actionType, string description, int employeeId, int residentSchemaId)
    {
        _context.ActivityLogEntries.Add(new ActivityLogEntry
        {
            ActionType = actionType,
            Description = description,
            Timestamp = DateTime.UtcNow,
            EmployeeId = employeeId,
            ResidentSchemaId = residentSchemaId
        });
        return Task.CompletedTask;
    }

    // Returns all log entries for a specific employee within a date range.
    // Includes related data (Employee name, Resident initials, Department name)
    // so the mapper can build a complete DTO for the frontend table.
    public async Task<List<ActivityLogEntry>> GetByEmployeeAsync(
        int employeeId, DateTime from, DateTime to)
    {
        return await _context.ActivityLogEntries
            .Include(a => a.Employee)
            .Include(a => a.ResidentSchema)
                .ThenInclude(s => s.Resident)
                    .ThenInclude(r => r.Department)
            .Where(a => a.EmployeeId == employeeId
                     && a.Timestamp >= from
                     && a.Timestamp <= to)
            .OrderByDescending(a => a.Timestamp)
            .ToListAsync();
    }
}