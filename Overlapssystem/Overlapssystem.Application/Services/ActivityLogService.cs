using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

// Fetches auditable entities filtered by employee and date range.
// Used by the activity log feature to show what a specific employee
// has done in a given period.
public class ActivityLogService : IActivityLogService
{
    private readonly IAppDbContext _context;

    public ActivityLogService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MedicationRegistration>> GetMedicationLogAsync(
        int employeeId, DateTime from, DateTime to)
    {
        return await _context.MedicationRegistrations
            .Include(m => m.ModifiedByEmployee)
            .Include(m => m.ResidentSchema)
                .ThenInclude(s => s.Resident)
                    .ThenInclude(r => r.Department)
            .Where(m => m.ModifiedByEmployeeId == employeeId
                     && m.CreatedAt >= from
                     && m.CreatedAt <= to)
            .ToListAsync();
    }

    public async Task<List<ResidentSchema>> GetResidentSchemaLogAsync(
        int employeeId, DateTime from, DateTime to)
    {
        return await _context.ResidentSchemas
            .Include(s => s.ModifiedByEmployee)
            .Include(s => s.Resident)
                .ThenInclude(r => r.Department)
            .Where(s => s.ModifiedByEmployeeId == employeeId
                     && s.UpdatedAt != null
                     && s.UpdatedAt >= from
                     && s.UpdatedAt <= to)
            .ToListAsync();
    }
}