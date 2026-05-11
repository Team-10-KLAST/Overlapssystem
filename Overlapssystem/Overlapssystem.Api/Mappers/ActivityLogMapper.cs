using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

// Maps auditable entities to ActivityLogEntryDto.
// Combines MedicationRegistrations and ResidentSchema changes
// into a single sorted list for the activity log view.
public static class ActivityLogMapper
{
    public static List<ActivityLogEntryDto> ToDtoList(
        List<MedicationRegistration> medications,
        List<ResidentSchema> schemas)
    {
        var entries = new List<ActivityLogEntryDto>();

        foreach (var m in medications)
        {
            entries.Add(new ActivityLogEntryDto
            {
                ActionType = "Medicin",
                Description = m.Given
                    ? "Markerede medicin som givet"
                    : "Afmarkerede medicin",
                ResidentInitials = m.ResidentSchema.Resident.Initials,
                EmployeeName = $"{m.ModifiedByEmployee!.FirstName} {m.ModifiedByEmployee.LastName}",
                EmployeeId = m.ModifiedByEmployeeId!.Value,
                DepartmentName = m.ResidentSchema.Resident.Department.DepartmentName,
                Timestamp = m.CreatedAt
            });
        }

        foreach (var s in schemas)
        {
            entries.Add(new ActivityLogEntryDto
            {
                ActionType = "Status/Trafiklys",
                Description = "Opdaterede borgerskema",
                ResidentInitials = s.Resident.Initials,
                EmployeeName = $"{s.ModifiedByEmployee!.FirstName} {s.ModifiedByEmployee.LastName}",
                EmployeeId = s.ModifiedByEmployeeId!.Value,
                DepartmentName = s.Resident.Department.DepartmentName,
                Timestamp = s.UpdatedAt!.Value
            });
        }

        return entries
            .OrderByDescending(e => e.Timestamp)
            .ToList();
    }
}