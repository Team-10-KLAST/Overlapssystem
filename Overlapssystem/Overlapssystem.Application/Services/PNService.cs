using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

// Concrete implementation of IPNService.
// Handles the business logic for PN medication registrations.
public class PNService : IPNService
{
    private readonly IAppDbContext _context;

    public PNService(IAppDbContext context)
    {
        _context = context;
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

        _context.ActivityLogEntries.Add(new ActivityLogEntry
        {
            ActionType = "PN",
            Description = $"Markerede PN medicin kl. {givenTime.ToString("HH:mm")}",
            Timestamp = DateTime.UtcNow,
            EmployeeId = employeeId,
            ResidentSchemaId = residentSchemaId
        });

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

    // Deletes a PN registration permanently by ID. Returns false if not found.
    public async Task<bool> DeletePNAsync(int pnId)
    {
        var pn = await _context.PNs.FindAsync(pnId);
        if (pn == null) return false;

        _context.PNs.Remove(pn);
        await _context.SaveChangesAsync();
        return true;
    }
}
