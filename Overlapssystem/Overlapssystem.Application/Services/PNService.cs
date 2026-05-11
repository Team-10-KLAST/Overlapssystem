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
    public async Task<int> RegisterPNAsync(int residentSchemaId, TimeOnly givenTime)
    {
        // step 1: create new PN entity with the given time
        var pn = new PN
        {
            ResidentSchemaId = residentSchemaId,
            GivenTime = givenTime
        };

        // step 2: add to database and save
        _context.PNs.Add(pn);
        await _context.SaveChangesAsync();

        // step 3: return the generated ID
        return pn.PNId;
    }

    // Returns all PN registrations for a specific resident schema.
    public async Task<IEnumerable<PN>> GetPNsBySchemaAsync(int residentSchemaId)
    {
        return await _context.PNs
            .Where(p => p.ResidentSchemaId == residentSchemaId)
            .ToListAsync();
    }
}
