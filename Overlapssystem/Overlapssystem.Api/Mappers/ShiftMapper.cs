using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

public static class ShiftMapper
{
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