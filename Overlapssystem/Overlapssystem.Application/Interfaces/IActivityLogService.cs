using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

// Interface for the ActivityLogService.
// Defines the contract for saving and retrieving activity log entries.
public interface IActivityLogService
{
    // Saves a new log entry to the database.
    Task LogActionAsync(ActivityLogEntry entry);

    // Returns all log entries for a specific employee within a date range.
    Task<List<ActivityLogEntry>> GetByEmployeeAsync(
        int employeeId, DateTime from, DateTime to);
}