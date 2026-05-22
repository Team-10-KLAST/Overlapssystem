using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Application.Services;

// Handles creating and retrieving shifts. 
public class ShiftService : IShiftService
{
    private readonly IAppDbContext _context;

    public ShiftService(IAppDbContext context)
    {
        _context = context;
    }

    // Creates a new shift or returns the existing one if a matching shift already exists. 
    public async Task<Shift> CreateShiftAsync(DateTime date, ShiftType shiftType, int departmentId)
    {
        var existingShift = await _context.Shifts
            .FirstOrDefaultAsync(s => s.Date.Date == date.Date
                                   && s.Type == shiftType
                                   && s.DepartmentId == departmentId);

        if (existingShift != null)
            return existingShift;

        var shift = new Shift
        {
            Date = date,
            Type = shiftType,
            DepartmentId = departmentId
        };

        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();
        return shift;
    }

    // Returns all shifts 
    public async Task<IEnumerable<Shift>> GetAllShiftsAsync()
    {
        return await _context.Shifts.ToListAsync();
    }

    // Returns a shift by its ID, or null if not found. 
    public async Task<Shift> GetShiftByIdAsync(int id)
    {
        var shift = await _context.Shifts.FindAsync(id);
        if (shift == null) return null;

        return shift;
    }
}