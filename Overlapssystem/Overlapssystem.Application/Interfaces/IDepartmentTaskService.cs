using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

public interface IDepartmentTaskService
{
    Task<IEnumerable<DepartmentTask>> GetTasksByDepartmentAsync(int departmentId);
    Task UpdateTaskNameAsync(int taskId, string taskName);
    Task SaveAssignmentAsync(int shiftId, int departmentTaskId, string assignedEmployee);
    Task<IEnumerable<ShiftTaskAssignment>> GetAssignmentsByShiftAsync(int shiftId);
    Task<DepartmentTask> CreateTaskAsync(int departmentId, string taskName);
    Task DeleteTaskAsync(int taskId);
}