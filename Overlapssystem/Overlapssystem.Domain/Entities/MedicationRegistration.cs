namespace Overlapssystem.Domain.Entities;

// Represents a registration of medication given to a resident
// at a specific scheduled medication time within a resident schema.
public class MedicationRegistration
{
    public int MedicationRegistrationId { get; set; }
    public DateTime GivenTime { get; set; }
    public bool Given { get; set; }
    public int ResidentSchemaId { get; set; }
    public int MedicationTimeId { get; set; }

    // Navigation properties to the resident schema and medication time this registration belongs to.
    public ResidentSchema ResidentSchema { get; set; } = null!;
    public MedicationTime MedicationTime { get; set; } = null!;
}

