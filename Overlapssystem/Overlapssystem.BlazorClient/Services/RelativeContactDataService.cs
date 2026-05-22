using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
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

    // Calls PUT api/relativecontact/{residentSchemaId} to update the contact string.
    public async Task<Result> UpdateAsync(int residentSchemaId, string contactNote)
    {
        try
        {
            var dto = new RelativeContactDto { ContactNote = contactNote };
            var response = await _http.PutAsJsonAsync($"api/relativecontact/{residentSchemaId}", dto);
            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke opdatere pårørendekontakt.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }
}
