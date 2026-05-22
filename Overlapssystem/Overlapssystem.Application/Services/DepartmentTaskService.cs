using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

// Handles business logic for department tasks and shift assignments
public class DepartmentTaskService : IDepartmentTaskService
{
    private readonly IAppDbContext _context;

    public DepartmentTaskService(IAppDbContext context)
    {
        _context = context;
    }

    // Fetches all tasks for a given department
    public async Task<IEnumerable<DepartmentTask>> GetTasksByDepartmentAsync(int departmentId)
    {
        return await _context.DepartmentTasks
            .Where(t => t.DepartmentId == departmentId)
            .ToListAsync();
    }

    // Updates the task name for a given task
    public async Task UpdateTaskNameAsync(int taskId, string taskName)
    {
        var task = await _context.DepartmentTasks.FindAsync(taskId);
        if (task != null)
        {
            task.TaskName = taskName;
            await _context.SaveChangesAsync();
        }
    }

    // Saves or updates an employee assignment for a task and shift
    public async Task SaveAssignmentAsync(int shiftId, int departmentTaskId, string assignedEmployee)
    {
        var existing = await _context.ShiftTaskAssignments
            .FirstOrDefaultAsync(a => a.ShiftId == shiftId && a.DepartmentTaskId == departmentTaskId);

        if (existing == null)
        {
            _context.ShiftTaskAssignments.Add(new ShiftTaskAssignment
            {
                ShiftId = shiftId,
                DepartmentTaskId = departmentTaskId,
                AssignedEmployee = assignedEmployee
            });
        }
        else
        {
            existing.AssignedEmployee = assignedEmployee;
        }

        await _context.SaveChangesAsync();
    }

    // Fetches all assignments for a given shift
    public async Task<IEnumerable<ShiftTaskAssignment>> GetAssignmentsByShiftAsync(int shiftId)
    {
        return await _context.ShiftTaskAssignments
            .Where(a => a.ShiftId == shiftId)
            .ToListAsync();
    }

    // Creates a new task for a department
    public async Task<DepartmentTask> CreateTaskAsync(int departmentId, string taskName)
    {
        var task = new DepartmentTask
        {
            DepartmentId = departmentId,
            TaskName = taskName
        };
        _context.DepartmentTasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    // Deletes a task from a department
    public async Task DeleteTaskAsync(int taskId)
    {
        var task = await _context.DepartmentTasks.FindAsync(taskId);
        if (task != null)
        {
            _context.DepartmentTasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}