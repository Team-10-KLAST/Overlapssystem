namespace Overlapssystem.Domain.Entities;

// Represents a single logged action performed by an employee.
// Each action (medication, traffic light, status, PN) creates a new row.
// This ensures full history — nothing is overwritten.
public class ActivityLogEntry
{
    public int ActivityLogEntryId { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }

    // Navigation properties to link the log entry to the employee and resident schema involved in the action.
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public int ResidentSchemaId { get; set; }
    public ResidentSchema ResidentSchema { get; set; } = null!;
}