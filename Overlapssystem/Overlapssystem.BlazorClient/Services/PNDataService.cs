using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Handles fetching and updating PN medication times from the API.
public class PNDataService : IPNDataService
{
    private readonly HttpClient _http;

    public PNDataService(HttpClient http)
    {
        _http = http;
    }

    // Retrieves all PN medication times for a given resident schema ID.
    public async Task<Result<IEnumerable<PNDto>>> GetPNsBySchemaAsync(int residentSchemaId)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<IEnumerable<PNDto>>($"api/pn/{residentSchemaId}");
            return Result<IEnumerable<PNDto>>.Success(result ?? Enumerable.Empty<PNDto>());
        }
        catch (HttpRequestException)
        {
            return Result<IEnumerable<PNDto>>.Failure("Kunne ikke hente PN-tider.");
        }
    }

    // Registers a new PN medication time.
    public async Task<Result<PNDto>> RegisterPNAsync(PNDto dto)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/pn", dto);

            if (!response.IsSuccessStatusCode)
                return Result<PNDto>.Failure("Kunne ikke registrere PN-tid.");

            var saved = await response.Content.ReadFromJsonAsync<PNDto>();
            return Result<PNDto>.Success(saved ?? dto);
        }
        catch (HttpRequestException)
        {
            return Result<PNDto>.Failure("Ingen forbindelse til serveren.");
        }
    }

    // Calls PUT api/pn/{id} to update the GivenTime of an existing PN registration.
    public async Task<Result> UpdatePNAsync(PNDto dto)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"api/pn/{dto.PNId}", dto);

            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke opdatere PN-tid.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }

    // Calls DELETE api/pn/{id} to permanently delete a PN registration.
    public async Task<Result> DeletePNAsync(int pnId)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/pn/{pnId}");

            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke slette PN-tid.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }
}
