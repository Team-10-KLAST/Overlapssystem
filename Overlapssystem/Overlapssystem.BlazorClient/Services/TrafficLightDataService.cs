using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Handles updating traffic light status for resident schemas through the API.
public class TrafficLightDataService : ITrafficLightDataService
{
    private readonly HttpClient _http;

    public TrafficLightDataService(HttpClient http)
    {
        _http = http;
    }

    // Updates the traffic light status for a specific resident schema.
    public async Task<Result> UpdateTrafficLightAsync(int residentSchemaId, string? trafficLight)
    {
        try
        {
            var dto = new UpdateTrafficLightDto { TrafficLight = trafficLight };
            var response = await _http.PutAsJsonAsync($"api/trafficlight/{residentSchemaId}", dto);

            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke opdatere risikovurdering.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }
}
