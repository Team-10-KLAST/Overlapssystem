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

    // Calls GET api/relativecontact/{residentSchemaId} to fetch all contact registrations.
    public async Task<Result<IEnumerable<RelativeContactDto>>> GetBySchemaAsync(int residentSchemaId)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<IEnumerable<RelativeContactDto>>($"api/relativecontact/{residentSchemaId}");
            return Result<IEnumerable<RelativeContactDto>>.Success(result ?? Enumerable.Empty<RelativeContactDto>());
        }
        catch (HttpRequestException)
        {
            return Result<IEnumerable<RelativeContactDto>>.Failure("Kunne ikke hente pårørendekontakt.");
        }
    }

    // Calls POST api/relativecontact to register a new relative contact.
    public async Task<Result<RelativeContactDto>> CreateAsync(RelativeContactDto dto)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/relativecontact", dto);

            if (!response.IsSuccessStatusCode)
                return Result<RelativeContactDto>.Failure("Kunne ikke oprette pårørendekontakt.");

            var saved = await response.Content.ReadFromJsonAsync<RelativeContactDto>();
            return Result<RelativeContactDto>.Success(saved ?? dto);
        }
        catch (HttpRequestException)
        {
            return Result<RelativeContactDto>.Failure("Ingen forbindelse til serveren.");
        }
    }

    // Calls PUT api/relativecontact/{id} to update an existing registration.
    public async Task<Result> UpdateAsync(RelativeContactDto dto)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"api/relativecontact/{dto.Id}", dto);

            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke opdatere pårørendekontakt.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }

    // Calls DELETE api/relativecontact/{id} to delete a registration.
    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/relativecontact/{id}");

            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke slette pårørendekontakt.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }
}
