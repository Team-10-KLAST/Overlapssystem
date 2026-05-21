using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

public static class PNMapper
{
    public static PNDto ToDto(PN pn)
    {
        return new PNDto
        {
            PNId = pn.PNId,
            ResidentSchemaId = pn.ResidentSchemaId,
            GivenTime = pn.GivenTime.ToString("HH:mm")
        };
    }

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
