using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Domain.Entities;

// Represents a resident's data for a specific shift.
// A new ResidentSchema is created for each shift.
public class ResidentSchema
{
    public int ResidentSchemaId { get; set; }
    public string RelativeContact { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public TrafficLight? TrafficLight { get; set; }

    // Navigation properties linking this schema to its resident, shift, and assigned employees.
    public int ResidentId { get; set; }
    public Resident Resident { get; set; } = null!;

    public int ShiftId { get; set; }
    public Shift Shift { get; set; } = null!;

    // A resident can have one or two employees assigned per shift.
    public int? PrimaryEmployeeId { get; set; }
    public Employee? PrimaryEmployee { get; set; }

    public int? SecondaryEmployeeId { get; set; }
    public Employee? SecondaryEmployee { get; set; }

    // Navigation property to the medication registrations belonging to this schema
    public ICollection<MedicationRegistration> MedicationRegistrations { get; set; } = new List<MedicationRegistration>();
}
