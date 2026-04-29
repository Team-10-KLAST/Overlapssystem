using Overlapssystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Application.Interfaces;

public interface IShiftService
{
    Task<ShiftDto> CreateShiftAsync(CreateShiftDto dto);
    Task<IEnumerable<ShiftDto>> GetShiftsAsync();
    Task<ShiftDto?> GetShiftByIdAsync(int id);
}
