using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Service class responsible for communicating with the API
// to update traffic light statuses for resident schemas.
public class TrafficLightDataService : ITrafficLightDataService
{
    private readonly HttpClient _http;

    public TrafficLightDataService(HttpClient http)
    {
        _http = http;
    }

    // Sends a PUT request to the API to update the traffic light status
    // for a specific resident schema.
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
