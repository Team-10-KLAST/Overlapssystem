using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

// Mapper responsible for converting PN entities to PNDto objects,
// including formatting the given time for display purposes.
public static class PNMapper
{
    // Maps a PN entity to a PNDto, formatting the given time as "HH:mm".
    public static PNDto ToDto(PN pn)
    {
        return new PNDto
        {
            PNId = pn.PNId,
            ResidentSchemaId = pn.ResidentSchemaId,
            GivenTime = pn.GivenTime.ToString("HH:mm")
        };
    }

    // Maps individual properties to a PNDto, used when creating a new PN registration.
    public static PNDto ToDto(int pnId, int residentSchemaId, string givenTime)
    {
        return new PNDto
        {
            PNId = pnId,
            ResidentSchemaId = residentSchemaId,
            GivenTime = givenTime
        };
    }
}
