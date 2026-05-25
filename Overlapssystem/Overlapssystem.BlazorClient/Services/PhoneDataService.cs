using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Handles fetching phone data from the API.
public class PhoneDataService : IPhoneDataService
{
    private readonly HttpClient _httpClient;

    public PhoneDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Fetches the list of phones from the API and returns it as a collection of PhoneDto.
    public async Task<Result<IEnumerable<PhoneDto>>> GetPhonesAsync()
    {
        try
        {
            var phones = await _httpClient.GetFromJsonAsync<IEnumerable<PhoneDto>>("api/phones");
            return Result<IEnumerable<PhoneDto>>.Success(phones ?? Enumerable.Empty<PhoneDto>());
        }
        catch (HttpRequestException)
        {
            return Result<IEnumerable<PhoneDto>>.Failure("Kunne ikke hente telefoner.");
        }
    }
    
    // Updates a phone's information through the API.
    public async Task<Result> UpdatePhoneAsync(PhoneDto phoneDto)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/phones/{phoneDto.Id}", phoneDto);
            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke opdatere telefon.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }
}
