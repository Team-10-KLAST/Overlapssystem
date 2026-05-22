using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
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
    public async Task<Result<IEnumerable<DepartmentTaskDto>>> GetTasksAsync(int departmentId, int shiftId)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<List<DepartmentTaskDto>>(
                $"api/departmenttask/{departmentId}/{shiftId}");
            return Result<IEnumerable<DepartmentTaskDto>>.Success(result ?? new List<DepartmentTaskDto>());
        }
        catch (HttpRequestException)
        {
            return Result<IEnumerable<DepartmentTaskDto>>.Failure("Kunne ikke hente opgaver.");
        }
    }

    // Updates the task name for a department task
    public async Task<Result> UpdateTaskNameAsync(DepartmentTaskDto dto)
    {
        try
        {
            var response = await _http.PutAsJsonAsync("api/departmenttask/taskname", dto);
            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke opdatere opgavenavn.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }

    // Saves or updates an employee assignment for a task in a specific shift
    public async Task<Result> SaveAssignmentAsync(int shiftId, DepartmentTaskDto dto)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"api/departmenttask/assignment/{shiftId}", dto);
            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke gemme tildeling.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }

    // Creates a new task for a department
    public async Task<Result<DepartmentTaskDto>> CreateTaskAsync(int departmentId)
    {
        try
        {
            var dto = new DepartmentTaskDto { TaskName = string.Empty };
            var response = await _http.PostAsJsonAsync($"api/departmenttask/{departmentId}", dto);

            if (!response.IsSuccessStatusCode)
                return Result<DepartmentTaskDto>.Failure("Kunne ikke oprette opgave.");

            var created = await response.Content.ReadFromJsonAsync<DepartmentTaskDto>();
            return Result<DepartmentTaskDto>.Success(created!);
        }
        catch (HttpRequestException)
        {
            return Result<DepartmentTaskDto>.Failure("Ingen forbindelse til serveren.");
        }
    }

    // Deletes a task
    public async Task<Result> DeleteTaskAsync(int taskId)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/departmenttask/{taskId}");
            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke slette opgave.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }
}