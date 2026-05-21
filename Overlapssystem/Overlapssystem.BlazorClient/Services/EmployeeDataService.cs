using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;
using System.Net.Http.Json;

namespace Overlapssystem.BlazorClient.Services;

public class EmployeeDataService : IEmployeeDataService
{
    private readonly HttpClient _httpClient;

    public EmployeeDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Fetches the list of employees from the API and returns it as a collection of EmployeeDto.
    public async Task<Result<IEnumerable<EmployeeDto>>> GetEmployeesAsync()
    {
        try
        {
            var employees = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>("api/employees");
            return Result<IEnumerable<EmployeeDto>>.Success(employees ?? Enumerable.Empty<EmployeeDto>());
        }
        catch (HttpRequestException)
        {
            return Result<IEnumerable<EmployeeDto>>.Failure("Kunne ikke hente medarbejdere.");
        }
    }
}
