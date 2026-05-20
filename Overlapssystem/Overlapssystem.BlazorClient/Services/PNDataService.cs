using System.Net.Http.Json;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Communicates with the PN API endpoint.
// HttpClient is injected by the DI container.
public class PNDataService : IPNDataService
{
    private readonly HttpClient _http;

    public PNDataService(HttpClient http)
    {
        _http = http;
    }

    // Calls GET api/pn/{residentSchemaId} to fetch all PN registrations for a schema.
    public async Task<IEnumerable<PNDto>> GetPNsBySchemaAsync(int residentSchemaId)
    {
        var result = await _http.GetFromJsonAsync<IEnumerable<PNDto>>($"api/pn/{residentSchemaId}");
        return result ?? Enumerable.Empty<PNDto>();
    }

    // Calls POST api/pn to register a new PN medication time.
    public async Task<PNDto> RegisterPNAsync(PNDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/pn", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PNDto>() ?? dto;
    }

    // Calls DELETE api/pn/{id} to permanently delete a PN registration.
    public async Task DeletePNAsync(int pnId)
    {
        var response = await _http.DeleteAsync($"api/pn/{pnId}");
        response.EnsureSuccessStatusCode();
    }
}
