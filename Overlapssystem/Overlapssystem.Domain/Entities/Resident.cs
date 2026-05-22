using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Domain.Entities;

// Represent a resident (borger) with their master data 
// A resident can have many ResidentSchemas over time (one per shift). 
public class Resident
{
    public int ResidentId { get; set; }
    public string Initials { get; set; } = string.Empty;
    public string ShoppingDay { get; set; } = string.Empty;
    public PaymentMethod PaymentMethod { get; set; }

    // Navigation property to the department this resident belongs to.
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    // Navigation property to the medication times scheduled for this resident.
    public ICollection<MedicationTime> MedicationTimes { get; set; } = new List<MedicationTime>();
}
