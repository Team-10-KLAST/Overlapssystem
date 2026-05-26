using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Handles assigning employees to resident schemas through the API.
public class EmployeeAssignmentDataService : IEmployeeAssignmentDataService
{
    private readonly HttpClient _httpClient;

    public EmployeeAssignmentDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Assigns an employee to a resident schema with a specific position.
    // 1 for primary, 2 for secondary.
    public async Task<Result> AssignEmployeeAsync(int residentSchemaId, int employeeId, int position)
    {
        try
        {
            var dto = new AssignEmployeeDto
            {
                ResidentSchemaId = residentSchemaId,
                EmployeeId = employeeId,
                Position = position
            };
            var response = await _httpClient.PostAsJsonAsync("api/residentschema/assign", dto);
            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke tildele medarbejder.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }
}