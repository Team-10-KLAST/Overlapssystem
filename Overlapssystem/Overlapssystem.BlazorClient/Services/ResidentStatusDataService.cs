using System.Net.Http.Json;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Calls the ResidentStatus API to update status text.
// Separate from ResidentSchemaDataService (Single Responsibility).
public class ResidentStatusDataService : IResidentStatusDataService
{
    private readonly HttpClient _http;

    public ResidentStatusDataService(HttpClient http)
    {
        _http = http;
    }

    // Calls PUT /api/residentstatus/{id} to update the status text.
    public async Task<bool> UpdateStatusAsync(int residentSchemaId, string newStatus)
    {
        var dto = new UpdateStatusDto { Status = newStatus };
        var response = await _http.PutAsJsonAsync($"api/residentstatus/{residentSchemaId}", dto);
        return response.IsSuccessStatusCode;
    }
}