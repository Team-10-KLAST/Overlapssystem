using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

public static class DepartmentNoteMapper
{
    public static DepartmentNoteDto ToDto(DepartmentNote note)
    {
        return new DepartmentNoteDto
        {
            DepartmentId = note.DepartmentId,
            Content = note.Content
        };
    }

    public static DepartmentNote ToEntity(DepartmentNoteDto dto)
    {
        return new DepartmentNote
        {
            DepartmentId = dto.DepartmentId,
            Content = dto.Content
        };
    }
}
