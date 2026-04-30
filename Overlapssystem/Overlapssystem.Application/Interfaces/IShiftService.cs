using Overlapssystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Application.Interfaces;

public interface IShiftService
{
    Task<Shift> CreateShiftAsync(DateTime date, ShiftType shiftType, int departmentId);
    Task<IEnumerable<Shift>> GetAllShiftsAsync();
    Task<Shift?> GetShiftByIdAsync(int id);
}
