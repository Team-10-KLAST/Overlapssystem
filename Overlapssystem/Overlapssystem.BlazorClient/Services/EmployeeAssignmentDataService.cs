using System.Net.Http.Json;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public class EmployeeAssignmentDataService : IEmployeeAssignmentDataService
{
    private readonly HttpClient _httpClient;

    public EmployeeAssignmentDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> AssignEmployeeAsync(int residentSchemaId, int employeeId, int position)
    {
        var dto = new AssignEmployeeDto
        {
            ResidentSchemaId = residentSchemaId,
            EmployeeId = employeeId,
            Position = position
        };

        // Stien matcher [Route("api/[controller]")] + [HttpPost("assign")]
        var response = await _httpClient.PostAsJsonAsync("api/residentschema/assign", dto);

        return response.IsSuccessStatusCode;
    }
}