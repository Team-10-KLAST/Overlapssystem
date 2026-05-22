using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

// Handles saving and retrieving activity log entries for auditing purposes
public class ActivityLogService : IActivityLogService
{
    private readonly IAppDbContext _context;

    public ActivityLogService(IAppDbContext context)
    {
        _context = context;
    }

    // Adds a new log entry to the context. Caller is responsible for calling SaveChangesAsync. 
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

    // Returns all log entries for a specific employee within a date range,
    // including related data. 
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