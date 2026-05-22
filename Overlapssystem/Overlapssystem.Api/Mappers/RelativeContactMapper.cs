using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

// Mapper responsible for converting between RelativeContact entities and RelativeContactDto objects.
public static class RelativeContactMapper
{
    // Maps a RelativeContact entity to a RelativeContactDto.
    public static RelativeContactDto ToDto(RelativeContact contact)
    {
        return new RelativeContactDto
        {
            Id = contact.Id,
            ResidentSchemaId = contact.ResidentSchemaId,
            ContactNote = contact.ContactNote,
            ContactType = contact.ContactType
        };
    }

    // Maps a RelativeContactDto to a new RelativeContact entity (without ID).
    public static RelativeContact ToEntity(RelativeContactDto dto)
    {
        return new RelativeContact
        {
            ResidentSchemaId = dto.ResidentSchemaId,
            ContactNote = dto.ContactNote,
            ContactType = dto.ContactType
        };
    }

    // Maps a RelativeContactDto to a RelativeContact entity with a specified ID (used for updates).
    public static RelativeContact ToEntity(int id, RelativeContactDto dto)
    {
        return new RelativeContact
        {
            Id = id,
            ResidentSchemaId = dto.ResidentSchemaId,
            ContactNote = dto.ContactNote,
            ContactType = dto.ContactType
        };
    }
}