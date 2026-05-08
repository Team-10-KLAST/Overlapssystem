using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for a service that saves shifts to the backend API.
public interface ISaveShiftService
{
    Task<(int? ShiftId, string Message)> SaveShiftAsync(CreateShiftDto model);
}
