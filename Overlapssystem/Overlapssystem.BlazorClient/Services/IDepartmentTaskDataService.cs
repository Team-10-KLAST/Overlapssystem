using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Defines the contract for fetching and updating department tasks from the API
public interface IDepartmentTaskDataService
{
    Task<IEnumerable<DepartmentTaskDto>> GetTasksAsync(int departmentId, int shiftId);
    Task UpdateTaskNameAsync(DepartmentTaskDto dto);
    Task SaveAssignmentAsync(int shiftId, DepartmentTaskDto dto);
    Task<DepartmentTaskDto?> CreateTaskAsync(int departmentId);

    Task DeleteTaskAsync(int taskId);
}