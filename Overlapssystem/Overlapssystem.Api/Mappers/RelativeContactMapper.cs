using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

public static class RelativeContactMapper
{
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

    public static RelativeContact ToEntity(RelativeContactDto dto)
    {
        return new RelativeContact
        {
            ResidentSchemaId = dto.ResidentSchemaId,
            ContactNote = dto.ContactNote,
            ContactType = dto.ContactType
        };
    }

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