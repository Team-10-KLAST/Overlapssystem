using System.Net.Http.Json;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Communicates with the RelativeContact API endpoint.
// HttpClient is injected by the DI container.
public class RelativeContactDataService : IRelativeContactDataService
{
    private readonly HttpClient _http;

    public RelativeContactDataService(HttpClient http)
    {
        _http = http;
    }

    // Calls GET api/relativecontact/{residentSchemaId} to fetch all contact registrations.
    public async Task<IEnumerable<RelativeContactDto>> GetBySchemaAsync(int residentSchemaId)
    {
        var result = await _http.GetFromJsonAsync<IEnumerable<RelativeContactDto>>($"api/relativecontact/{residentSchemaId}");
        return result ?? Enumerable.Empty<RelativeContactDto>();
    }

    // Calls POST api/relativecontact to register a new relative contact.
    public async Task<RelativeContactDto> CreateAsync(RelativeContactDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/relativecontact", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RelativeContactDto>() ?? dto;
    }

    // Calls PUT api/relativecontact/{id} to update an existing registration.
    public async Task UpdateAsync(RelativeContactDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/relativecontact/{dto.Id}", dto);
        response.EnsureSuccessStatusCode();
    }

    // Calls DELETE api/relativecontact/{id} to delete a registration.
    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/relativecontact/{id}");
        response.EnsureSuccessStatusCode();
    }
}
