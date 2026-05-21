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
    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
    {
        var employees = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>("api/employees");
        return employees ?? Enumerable.Empty<EmployeeDto>();
    }
}
