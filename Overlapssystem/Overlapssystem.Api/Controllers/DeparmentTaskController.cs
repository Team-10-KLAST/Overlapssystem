using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentTaskController : ControllerBase
{
    private readonly IDepartmentTaskService _departmentTaskService;

    public DepartmentTaskController(IDepartmentTaskService departmentTaskService)
    {
        _departmentTaskService = departmentTaskService;
    }

    // Returns all tasks for a department with employee assignments for a specific shift
    [HttpGet("{departmentId}/{shiftId}")]
    public async Task<ActionResult<IEnumerable<DepartmentTaskDto>>> GetTasks(int departmentId, int shiftId)
    {
        var tasks = await _departmentTaskService.GetTasksByDepartmentAsync(departmentId);
        var assignments = await _departmentTaskService.GetAssignmentsByShiftAsync(shiftId);

        var dtos = tasks.Select(t => new DepartmentTaskDto
        {
            Id = t.Id,
            TaskName = t.TaskName,
            AssignedEmployee = assignments
                .FirstOrDefault(a => a.DepartmentTaskId == t.Id)?.AssignedEmployee ?? string.Empty
        });

        return Ok(dtos);
    }

    // Updates the task name for a department task
    [HttpPut("taskname")]
    public async Task<IActionResult> UpdateTaskName(DepartmentTaskDto dto)
    {
        await _departmentTaskService.UpdateTaskNameAsync(dto.Id, dto.TaskName);
        return NoContent();
    }

    // Saves or updates an employee assignment for a task in a specific shift
    [HttpPut("assignment/{shiftId}")]
    public async Task<IActionResult> SaveAssignment(int shiftId, DepartmentTaskDto dto)
    {
        await _departmentTaskService.SaveAssignmentAsync(shiftId, dto.Id, dto.AssignedEmployee);
        return NoContent();
    }

    // Creates a new task for a department
    [HttpPost("{departmentId}")]
    public async Task<IActionResult> CreateTask(int departmentId, DepartmentTaskDto dto)
    {
        var task = await _departmentTaskService.CreateTaskAsync(departmentId, dto.TaskName);
        return Ok(new DepartmentTaskDto
        {
            Id = task.Id,
            TaskName = task.TaskName,
            AssignedEmployee = string.Empty
        });
    }
    
    // Deletes a task
    [HttpDelete("{taskId}")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        await _departmentTaskService.DeleteTaskAsync(taskId);
        return NoContent();
    }
}