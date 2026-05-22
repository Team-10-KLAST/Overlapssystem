using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

// Defines the contract for managing department-level tasks and their shift assignments. 
public interface IDepartmentTaskService
{
    Task<IEnumerable<DepartmentTask>> GetTasksByDepartmentAsync(int departmentId);
    Task UpdateTaskNameAsync(int taskId, string taskName);
    Task SaveAssignmentAsync(int shiftId, int departmentTaskId, string assignedEmployee);
    Task<IEnumerable<ShiftTaskAssignment>> GetAssignmentsByShiftAsync(int shiftId);
    Task<DepartmentTask> CreateTaskAsync(int departmentId, string taskName);
    Task DeleteTaskAsync(int taskId);
}