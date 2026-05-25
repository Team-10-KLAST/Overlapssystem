using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Calls the ResidentStatus API to update status text.
public class ResidentStatusDataService : IResidentStatusDataService
{
    private readonly HttpClient _http;

    public ResidentStatusDataService(HttpClient http)
    {
        _http = http;
    }

    // Updates the status for a resident.
    public async Task<Result> UpdateStatusAsync(int residentSchemaId, string newStatus)
    {
        try
        {
            var dto = new UpdateStatusDto { Status = newStatus };
            var response = await _http.PutAsJsonAsync($"api/residentstatus/{residentSchemaId}", dto);

            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke gemme status.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }
}