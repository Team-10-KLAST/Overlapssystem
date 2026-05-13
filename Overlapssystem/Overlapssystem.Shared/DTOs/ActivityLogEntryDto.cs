namespace Overlapssystem.Shared.DTOs;

// DTO for activity log entries shown in the admin activity log table.
// Combines data from multiple entity types (MedicationRegistration, ResidentSchema, PN).
public class ActivityLogEntryDto
{
    public string ActionType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ResidentInitials { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}