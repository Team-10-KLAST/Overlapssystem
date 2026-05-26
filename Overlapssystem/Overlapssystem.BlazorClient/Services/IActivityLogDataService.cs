using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for fetching activity log entries for employees from the API.
public interface IActivityLogDataService
{
    Task<Result<List<ActivityLogEntryDto>>> GetByEmployeeAsync(
        int employeeId, DateTime from, DateTime to);
}