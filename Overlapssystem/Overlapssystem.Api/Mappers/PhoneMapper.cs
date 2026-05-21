using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

public static class PhoneMapper
{
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
