namespace Overlapssystem.Application.Interfaces;

// Interface for the TrafficLightService,
// which defines the contract for updating a resident's traffic light status.
public interface ITrafficLightService
{
    Task<bool> UpdateTrafficLightAsync(int residentSchemaId, string? trafficLight, int employeeId);
}
