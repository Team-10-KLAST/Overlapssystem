using Microsoft.AspNetCore.Components;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Components;

// Code-behind for ShiftTaskSection.razor.
// Displays and manages the list of tasks for a given department and shift, allowing adding,
// renaming, deleting, and assigning tasks to employees.
public partial class ShiftTaskSection
{
    [Inject] private IDepartmentTaskDataService TaskService { get; set; } = default!;
    [Parameter] public int DepartmentId { get; set; }
    [Parameter] public int ShiftId { get; set; }

    private List<DepartmentTaskDto>? tasks;
    private string? _errorMessage;

    // Load tasks when parameters change (e.g., when a different shift is selected).
    protected override async Task OnParametersSetAsync()
    {
        if (DepartmentId > 0 && ShiftId > 0)
        {
            var result = await TaskService.GetTasksAsync(DepartmentId, ShiftId);
            if (result.IsSuccess)
                tasks = result.Data!.ToList();
            else
                _errorMessage = result.Error;
        }
    }

    private async Task UpdateTaskName(DepartmentTaskDto task)
    {
        var result = await TaskService.UpdateTaskNameAsync(task);
        if (!result.IsSuccess)
            _errorMessage = result.Error;
    }

    private async Task SaveAssignment(DepartmentTaskDto task)
    {
        var result = await TaskService.SaveAssignmentAsync(ShiftId, task);
        if (!result.IsSuccess)
            _errorMessage = result.Error;
    }

    private async Task AddTask()
    {
        var result = await TaskService.CreateTaskAsync(DepartmentId);
        if (result.IsSuccess)
            tasks?.Add(result.Data!);
        else
            _errorMessage = result.Error;
    }

    private async Task DeleteTask(DepartmentTaskDto task)
    {
        var result = await TaskService.DeleteTaskAsync(task.Id);
        if (result.IsSuccess)
            tasks?.Remove(task);
        else
            _errorMessage = result.Error;
    }
}
