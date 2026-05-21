using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public interface IShiftService
{
    Task<Result<ShiftDto>> GetByIdAsync(int id);
}