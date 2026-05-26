using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

// This service handles the business logic for PN medication registrations.
public class PNService : IPNService
{
    private readonly IAppDbContext _context;
    private readonly IActivityLogService _activityLogService;

    public PNService(IAppDbContext context, IActivityLogService activityLogService)
    {
        _context = context;
        _activityLogService = activityLogService;
    }

    // Registers a new PN medication with a given time for a resident schema.
    public async Task<int> RegisterPNAsync(int residentSchemaId, TimeOnly givenTime, int employeeId)
    {
        var pn = new PN
        {
            ResidentSchemaId = residentSchemaId,
            GivenTime = givenTime
        };

        _context.PNs.Add(pn);

        await _activityLogService.AddLogEntry(
            "PN",
            $"Markerede PN medicin kl. {givenTime:HH:mm}",
            employeeId,
            residentSchemaId);

        await _context.SaveChangesAsync();

        return pn.PNId;
    }

    // Returns all PN registrations for a specific resident schema.
    public async Task<IEnumerable<PN>> GetPNsBySchemaAsync(int residentSchemaId)
    {
        return await _context.PNs
            .Where(p => p.ResidentSchemaId == residentSchemaId)
            .ToListAsync();
    }

    // Finds the PN by id, updates GivenTime, and saves. Returns false if not found.
    public async Task<bool> UpdatePNAsync(int pnId, TimeOnly givenTime)
    {
        // step 1: find PN by primary key
        var pn = await _context.PNs.FindAsync(pnId);

        // step 2: if not found, return false
        if (pn == null)
            return false;

        // step 3: update the given time
        pn.GivenTime = givenTime;

        // step 4: save changes to database
        await _context.SaveChangesAsync();

        return true;
    }

    // Deletes a PN registration permanently by ID. Returns false if not found.
    public async Task<bool> DeletePNAsync(int pnId, int employeeId)
    {
        var pn = await _context.PNs.FindAsync(pnId);
        if (pn == null) return false;

        _context.PNs.Remove(pn);

        await _activityLogService.AddLogEntry(
            "PN",
            $"Slettede PN registrering for kl. {pn.GivenTime:HH:mm}",
            employeeId,
            pn.ResidentSchemaId);

        await _context.SaveChangesAsync();
        return true;
    }
}
