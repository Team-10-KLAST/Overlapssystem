using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for a service that saves shifts to the backend API.
public interface ISaveShiftService
{
    Task<Result<int>> SaveShiftAsync(CreateShiftDto model);
}
