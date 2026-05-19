using System.Net.Http.Json;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Handles HTTP communication with the DepartmentTask API endpoint
public class DepartmentTaskDataService : IDepartmentTaskDataService
{
    private readonly HttpClient _http;

    public DepartmentTaskDataService(HttpClient http)
    {
        _http = http;
    }

    // Fetches all tasks for a department with employee assignments for a specific shift
    public async Task<IEnumerable<DepartmentTaskDto>> GetTasksAsync(int departmentId, int shiftId)
    {
        var result = await _http.GetFromJsonAsync<List<DepartmentTaskDto>>($"api/departmenttask/{departmentId}/{shiftId}");
        return result ?? new List<DepartmentTaskDto>();
    }

    // Updates the task name for a department task
    public async Task UpdateTaskNameAsync(DepartmentTaskDto dto)
    {
        await _http.PutAsJsonAsync("api/departmenttask/taskname", dto);
    }

    // Saves or updates an employee assignment for a task in a specific shift
    public async Task SaveAssignmentAsync(int shiftId, DepartmentTaskDto dto)
    {
        await _http.PutAsJsonAsync($"api/departmenttask/assignment/{shiftId}", dto);
    }

    // Creates a new task for a department
    public async Task<DepartmentTaskDto?> CreateTaskAsync(int departmentId)
    {
        var dto = new DepartmentTaskDto { TaskName = string.Empty };
        var response = await _http.PostAsJsonAsync($"api/departmenttask/{departmentId}", dto);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<DepartmentTaskDto>();
        return null;
    }

    // Deletes a task
    public async Task DeleteTaskAsync(int taskId)
    {
        await _http.DeleteAsync($"api/departmenttask/{taskId}");
    }
}