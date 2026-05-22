namespace Overlapssystem.Domain.Entities;

// Represents a single PN medication registration for a resident.
// A resident can have multiple PN registrations per shift.
public class PN
{
    public int PNId { get; set; }
    public TimeOnly GivenTime { get; set; }
    public int ResidentSchemaId { get; set; }

    // Navigation property to the resident schema this PN registration belongs to.
    public ResidentSchema ResidentSchema { get; set; } = null!;
}