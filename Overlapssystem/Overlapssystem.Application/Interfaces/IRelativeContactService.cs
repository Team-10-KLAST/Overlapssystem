using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

// Interface for managing relative contact registrations on a resident schema.
public interface IRelativeContactService
{
    Task<IEnumerable<RelativeContact>> GetBySchemaAsync(int residentSchemaId);
    Task<RelativeContact> CreateAsync(RelativeContact contact);
    Task UpdateAsync(RelativeContact contact);
    Task DeleteAsync(int id);
}
