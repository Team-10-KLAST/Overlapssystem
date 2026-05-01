using System.Net.Http.Json;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Fetches resident schema data from the API via HTTP 
// HttpClient is injected by DI container 
public class ResidentSchemaDataService : IResidentSchemaDataService
{
    private readonly HttpClient _http;

    public ResidentSchemaDataService( HttpClient http)
    {
        _http = http;
    }

    // Calls GET/api/residentschema/shift/{shiftId}
    public async Task<IEnumerable<ResidentSchemaDto>> GetByShiftAsync(int shiftId)
    {
        var result = await _http.GetFromJsonAsync<List<ResidentSchemaDto>>($"api/residentschema/shift/{shiftId}");

        // return empty list if the API returns null 
        return result ?? new List<ResidentSchemaDto>();
    }

  

}
