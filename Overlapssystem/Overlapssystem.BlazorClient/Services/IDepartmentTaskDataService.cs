using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for fetching and updating department tasks through the API.
public interface IDepartmentTaskDataService
{
    Task<Result<IEnumerable<DepartmentTaskDto>>> GetTasksAsync(int departmentId, int shiftId);
    Task<Result> UpdateTaskNameAsync(DepartmentTaskDto dto);
    Task<Result> SaveAssignmentAsync(int shiftId, DepartmentTaskDto dto);
    Task<Result<DepartmentTaskDto>> CreateTaskAsync(int departmentId);
    Task<Result> DeleteTaskAsync(int taskId);
} 