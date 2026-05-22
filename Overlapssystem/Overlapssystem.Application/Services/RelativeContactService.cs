using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

// concrete implementation of IRelativeContactService
// Handles updating the RelativeContact string field on ResidentSchema.
public class RelativeContactService : IRelativeContactService
{
    private readonly IAppDbContext _context;

    public RelativeContactService(IAppDbContext context)
    {
        _context = context;
    }

    // Updates the RelativeContact string field on the given ResidentSchema.
    public async Task UpdateAsync(int residentSchemaId, string contactNote)
    {
        var schema = await _context.ResidentSchemas.FindAsync(residentSchemaId);
        if (schema == null)
            throw new KeyNotFoundException();

        schema.RelativeContact = contactNote;
        await _context.SaveChangesAsync();
    }
}
