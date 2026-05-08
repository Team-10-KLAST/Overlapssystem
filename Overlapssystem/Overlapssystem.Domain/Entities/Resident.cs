using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Domain.Entities;

// Represent a resident (borger) with its "master data" 
// A resident can have many ResidentSchemas over time (one per shift). 
public class Resident
{
    public int ResidentId { get; set; }
    public string Initials { get; set; } = string.Empty;
    public string ShoppingDay { get; set; } = string.Empty;
    public PaymentMethod PaymentMethod { get; set; }

    // Foreign key to the department this resident lives in.
    // Navigation is uni-directional: Resident -> Department only. 
    public int DepartmentId { get; set; }

    // Navigation property to MedicationTimes. A resident can have multiple medication times.
    public ICollection<MedicationTime> MedicationTimes { get; set; } = new List<MedicationTime>();
}
