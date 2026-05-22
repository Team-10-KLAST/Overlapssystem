using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

// Mapper responsible for converting Employee entities to EmployeeDto objects,
// including generating initials for display purposes.
public static class EmployeeMapper
{
    // Maps an Employee entity to an EmployeeDto, including initials
    public static EmployeeDto ToDto(Employee e)
    {
        return new EmployeeDto
        {
            EmployeeId = e.EmployeeId,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Initials = GetInitials(e.FirstName, e.LastName)
        };
    }

    // Helper method to generate initials from first and last name
    private static string GetInitials(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            return "??";

        return $"{firstName[0]}{lastName[0]}".ToUpper();
    }
}