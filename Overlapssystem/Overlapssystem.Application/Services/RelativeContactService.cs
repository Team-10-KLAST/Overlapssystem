using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

// concrete implementation of IRelativeContactService
// Handles the business logic for relative contact registrations on a resident schema.
public class RelativeContactService : IRelativeContactService
{
    private readonly IAppDbContext _context;

    public RelativeContactService(IAppDbContext context)
    {
        _context = context;
    }

    // Fetches all relative contacts for a specific resident schema, ordered by ID.
    public async Task<IEnumerable<RelativeContact>> GetBySchemaAsync(int residentSchemaId)
    {
        return await _context.RelativeContacts
            .Where(rc => rc.ResidentSchemaId == residentSchemaId)
            .OrderBy(rc => rc.Id)
            .ToListAsync();
    }

    // Creates a new relative contact and saves it to the database.
    public async Task<RelativeContact> CreateAsync(RelativeContact contact)
    {
        _context.RelativeContacts.Add(contact);
        await _context.SaveChangesAsync();
        return contact;
    }

    // Updates an existing relative contact. Throws KeyNotFoundException if not found.
    public async Task UpdateAsync(RelativeContact contact)
    {
        var existing = await _context.RelativeContacts.FindAsync(contact.Id);
        if (existing == null)
            throw new KeyNotFoundException();

        existing.ContactNote = contact.ContactNote;
        existing.ContactType = contact.ContactType;
        await _context.SaveChangesAsync();
    }

    // Deletes a relative contact by ID. Throws KeyNotFoundException if not found.
    public async Task DeleteAsync(int id)
    {
        var existing = await _context.RelativeContacts.FindAsync(id);
        if (existing == null)
            throw new KeyNotFoundException();

        _context.RelativeContacts.Remove(existing);
        await _context.SaveChangesAsync();
    }
}
