namespace Overlapssystem.Domain.Entities;

// Represents a single logged action performed by an employee.
// Each action (medication, traffic light, status, PN) creates a new row.
// This ensures full history — nothing is overwritten.
public class ActivityLogEntry
{
    public int ActivityLogEntryId { get; set; }

    // What type of action: "Medicin", "Trafiklys", "Status", "PN"
    public string ActionType { get; set; } = string.Empty;

    // Human-readable description of what happened
    // e.g. "Markerede medicin som givet kl. 08:00"
    // e.g. "Ændrede trafiklys til Grøn"
    // e.g. "Tilføjede til status: borger har en depressiv episode"
    public string Description { get; set; } = string.Empty;

    // When the action was performed
    public DateTime Timestamp { get; set; }

    // Who performed the action
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    // Which resident schema was affected
    public int ResidentSchemaId { get; set; }
    public ResidentSchema ResidentSchema { get; set; } = null!;
}