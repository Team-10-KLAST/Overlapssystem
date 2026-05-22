using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

// Mapper responsible for converting ActivityLogEntry entities to ActivityLogEntryDto objects.
// Now simple one-to-one mapping since each action is its own row.
public static class ActivityLogMapper
{
    // Maps a single ActivityLogEntry entity to its corresponding DTO.
    public static ActivityLogEntryDto ToDto(ActivityLogEntry entry)
    {
        return new ActivityLogEntryDto
        {
            ActionType = entry.ActionType,
            Description = entry.Description,
            ResidentInitials = entry.ResidentSchema.Resident.Initials,
            EmployeeName = $"{entry.Employee.FirstName} {entry.Employee.LastName}",
            EmployeeId = entry.EmployeeId,
            DepartmentName = entry.ResidentSchema.Resident.Department.DepartmentName,
            Timestamp = entry.Timestamp
        };
    }

    // Maps a list of ActivityLogEntry entities to a list of DTOs.
    public static List<ActivityLogEntryDto> ToDtoList(List<ActivityLogEntry> entries)
    {
        return entries.Select(ToDto).ToList();
    }
}