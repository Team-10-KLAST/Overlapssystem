namespace Overlapssystem.Application.Interfaces;

// Interface for managing relative contact registrations on a resident schema.
public interface IRelativeContactService
{
    Task UpdateAsync(int residentSchemaId, string contactNote);
}
