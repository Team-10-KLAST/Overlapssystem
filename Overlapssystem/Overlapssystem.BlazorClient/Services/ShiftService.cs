using System.Net.Http.Json;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public class ShiftService : IShiftService
{
    private readonly HttpClient _http;

    public ShiftService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ShiftDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<ShiftDto>($"api/shift/{id}");
    }
}