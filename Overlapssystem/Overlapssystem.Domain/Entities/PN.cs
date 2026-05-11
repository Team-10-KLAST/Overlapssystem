namespace Overlapssystem.Domain.Entities;

// Represents a single PN medication registration for a resident.
// A resident can have multiple PN registrations per shift.
public class PN
{
    public int PNId { get; set; }

    // The time the PN medication was given.
    public TimeOnly GivenTime { get; set; }

    // Foreign key — which resident schema does this PN belong to?
    public int ResidentSchemaId { get; set; }

    // Navigation property back to ResidentSchema.
    public ResidentSchema ResidentSchema { get; set; } = null!;
}