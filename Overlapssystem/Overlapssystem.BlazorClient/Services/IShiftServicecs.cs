using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for fetching shift data from the API.
public interface IShiftService
{
    Task<Result<ShiftDto>> GetByIdAsync(int id);
}