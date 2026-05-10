using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public interface IShiftService
{
    Task<ShiftDto?> GetByIdAsync(int id);
}