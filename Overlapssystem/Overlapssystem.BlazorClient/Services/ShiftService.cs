using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Handles fetching shift data from the API.
public class ShiftService : IShiftService
{
    private readonly HttpClient _http;

    public ShiftService(HttpClient http)
    {
        _http = http;
    }

    // Fetches a shift by its ID from the API.
    public async Task<Result<ShiftDto>> GetByIdAsync(int id)
    {
        try
        {
            var shift = await _http.GetFromJsonAsync<ShiftDto>($"api/shift/{id}");

            if (shift == null)
                return Result<ShiftDto>.Failure("Vagten blev ikke fundet.");

            return Result<ShiftDto>.Success(shift);
        }
        catch (HttpRequestException)
        {
            return Result<ShiftDto>.Failure("Ingen forbindelse til serveren.");
        }
    }
}