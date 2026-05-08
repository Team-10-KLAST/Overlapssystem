using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

// Ansvarlig for at mappe Employee entiteter til DTOs.
public static class EmployeeMapper
{
    public static EmployeeDto ToDto(Employee e)
    {
        return new EmployeeDto
        {
            EmployeeId = e.EmployeeId,
            FirstName = e.FirstName,
            LastName = e.LastName,
            // Genererer initialer (f.eks. "Jens Sørensen" -> "JS")
            Initials = GetInitials(e.FirstName, e.LastName)
        };
    }

    private static string GetInitials(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            return "??";

        return $"{firstName[0]}{lastName[0]}".ToUpper();
    }
}