using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;
using static System.Net.WebRequestMethods;

namespace Overlapssystem.Application.Services;

public class ShiftService : IShiftService
{
    //DbContext til databaseadgang
    private readonly IAppDbContext _context;

    //Dependency injection af IAppDbContext  
    public ShiftService(IAppDbContext context)
    {
        _context = context;
    }

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

    public async Task<IEnumerable<Shift>> GetAllShiftsAsync()
    {
        //Henter alle vagter
        return await _context.Shifts.ToListAsync();
    }

    public async Task<Shift> GetShiftByIdAsync(int id)
    {
        //Henter vagt efter id
        var shift = await _context.Shifts.FindAsync(id);
        if (shift == null) return null;

        return shift;
    }
}