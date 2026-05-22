using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Application.Interfaces;

// Defines the contract for creating and retrieving shifts
public interface IShiftService
{
    Task<Shift> CreateShiftAsync(DateTime date, ShiftType shiftType, int departmentId);
    Task<IEnumerable<Shift>> GetAllShiftsAsync();
    Task<Shift?> GetShiftByIdAsync(int id);
}
