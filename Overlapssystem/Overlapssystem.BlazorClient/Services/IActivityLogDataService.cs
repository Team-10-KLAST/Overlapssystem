using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public interface IActivityLogDataService
{
    Task<Result<List<ActivityLogEntryDto>>> GetByEmployeeAsync(
        int employeeId, DateTime from, DateTime to);
}