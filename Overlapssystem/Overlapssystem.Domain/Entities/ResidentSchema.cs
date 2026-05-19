using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Domain.Entities;

public class ResidentSchema
{
    public int ResidentSchemaId { get; set; }
    public string RelativeContact { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public TrafficLight? TrafficLight { get; set; }

    public int ResidentId { get; set; }
    public Resident Resident { get; set; } = null!;

    public int? PrimaryEmployeeId { get; set; }
    public Employee? PrimaryEmployee { get; set; }

    public int? SecondaryEmployeeId { get; set; }
    public Employee? SecondaryEmployee { get; set; }

    public int ShiftId { get; set; }
    public Shift Shift { get; set; } = null!;

    // Navigation property to MedicationRegistrations. A resident schema can have multiple medication registrations.
    public ICollection<MedicationRegistration> MedicationRegistrations { get; set; } = new List<MedicationRegistration>();

}
