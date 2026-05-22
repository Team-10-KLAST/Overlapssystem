using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public class EmployeeAssignmentDataService : IEmployeeAssignmentDataService
{
    private readonly HttpClient _httpClient;

    public EmployeeAssignmentDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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