using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

// Mapper responsible for converting Phone entities to PhoneDto objects,
// including related employee information for display purposes.
public static class PhoneMapper
{
    // Maps a Phone entity to a PhoneDto, including the employee's initials if available.
    public static PhoneDto ToDto(Phone p)
    {
        if (p == null) return null;

        EmployeeDto? employeeDto = p.Employee != null ? EmployeeMapper.ToDto(p.Employee) : null;

        return new PhoneDto
        {
            Id = p.Id,
            Number = p.Number,
            EmployeeId = p.EmployeeId,

            EmployeeInitials = employeeDto != null ? employeeDto.Initials : "",
            IsTested = p.IsTested
        };
    }
}
