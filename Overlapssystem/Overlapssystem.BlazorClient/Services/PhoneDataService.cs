using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public class PhoneDataService : IPhoneDataService
{
    private readonly HttpClient _httpClient;

    public PhoneDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    
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
