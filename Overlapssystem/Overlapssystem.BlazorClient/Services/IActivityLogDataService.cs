using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public interface IActivityLogDataService
{
    Task<List<ActivityLogEntryDto>> GetByEmployeeAsync(
        int employeeId, DateTime from, DateTime to);
}