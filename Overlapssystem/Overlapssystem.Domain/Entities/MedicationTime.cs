namespace Overlapssystem.Domain.Entities;

// Represents a scheduled medication time for a resident.
// A resident can have multiple medication times throughout the day.
public class MedicationTime
{
    public int MedicationTimeId { get; set; }
    public int ResidentId { get; set; }
    public TimeOnly PlannedTime { get; set; }

    // Navigation property to the resident this medication time belongs to.
    public Resident Resident { get; set; } = null!;
}

