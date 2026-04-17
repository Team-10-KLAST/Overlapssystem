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

    public async Task<IEnumerable<ResidentSchemaDto>> GetAllAsync()
    {
        var result = await _http.GetFromJsonAsync<List<ResidentSchemaDto>>("api/residentschema");

        // return empty list if the API returns null 
        return result ?? new List<ResidentSchemaDto>();
    }

}
