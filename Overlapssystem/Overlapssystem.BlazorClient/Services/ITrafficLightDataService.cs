namespace Overlapssystem.BlazorClient.Services
{
    // Interface defining the contract for the TrafficLightDataService.
    // This service is responsible for sending traffic light updates to the API.
    public interface ITrafficLightDataService
    {
        Task UpdateTrafficLightAsync(int residentSchemaId, string? trafficLight);
    }
}
