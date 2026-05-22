using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

// Mapper responsible for converting between DepartmentNote entities and DepartmentNoteDto objects.
public static class DepartmentNoteMapper
{
    // Maps a DepartmentNote entity to a DepartmentNoteDto.
    public static DepartmentNoteDto ToDto(DepartmentNote note)
    {
        return new DepartmentNoteDto
        {
            DepartmentId = note.DepartmentId,
            Content = note.Content
        };
    }

    // Maps a DepartmentNoteDto to a DepartmentNote entity.
    public static DepartmentNote ToEntity(DepartmentNoteDto dto)
    {
        return new DepartmentNote
        {
            DepartmentId = dto.DepartmentId,
            Content = dto.Content
        };
    }
}
