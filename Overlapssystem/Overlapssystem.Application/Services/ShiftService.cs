using Microsoft.EntityFrameworkCore;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Infrastructure.Repositories.Data;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Application.Services;

public class ShiftService : IShiftService
{
    //DbContext til databaseadgang
    private readonly AppDbContext _context;

    //Dependency injection af AppDbContext  
    public ShiftService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ShiftDto> CreateShiftAsync(CreateShiftDto dto)
    {
        //Validering af dato
        if (dto.Date > DateTime.Today)
            throw new ArgumentException("Dato må ikke være i fremtiden.");

        if (!Enum.IsDefined(typeof(DepartmentType), dto.Department) || dto.Department == DepartmentType.None)
            throw new ArgumentException("Husk at vælge afdeling.");

        //Opretter Shift-objekt
        var shift = new Shift
        {
            Date = dto.Date,
            Type = dto.ShiftType,
            Department = dto.Department
        };

        //Gemmer i database
        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();

        return new ShiftDto
        {
            Id = shift.Id,
            DateTime = shift.Date,
            ShiftType = shift.Type,
            Department = shift.Department
        };
    }

    public async Task<IEnumerable<ShiftDto>> GetShiftsAsync()
    {
        //Henter alle vagter og mapper til ShiftDto (asynkron databaseforespørgsel)
        return await _context.Shifts
            .Select(s => new ShiftDto
            {
                Id = s.Id,
                DateTime = s.Date,
                ShiftType = s.Type,
                Department = s.Department
            })
            .ToListAsync();
    }

    public async Task<ShiftDto?> GetShiftByIdAsync(int id)
    {
        //Henter vagt efter id og mapper til ShiftDto (asynkron databaseforespørgsel)
        var shift = await _context.Shifts.FindAsync(id);
        if (shift == null) return null;

        return new ShiftDto
        {
            Id = shift.Id,
            DateTime = shift.Date,
            ShiftType = shift.Type,
            Department = shift.Department
        };
    }
}