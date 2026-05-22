using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

// Mapper responsible for converting Shift entities to ShiftDto objects,
// including formatting date and time for display purposes.
public static class ShiftMapper
{
    // Maps a Shift entity to a ShiftDto, formatting the date and time in ISO 8601 format for consistency.
    public static ShiftDto ToDto(Shift shift)
    {
        return new ShiftDto
        {
            Id = shift.Id.ToString(),
            DateTime = shift.Date.ToString("o"),
            ShiftType = shift.Type.ToString(),
            DepartmentId = shift.DepartmentId.ToString()
        };
    }
}