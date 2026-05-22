namespace Overlapssystem.Domain.Entities;

// Represents a work phone that can be assigned to an employee during a shift.
public class Phone
{
    public int Id { get; set; } 
    public string Number { get; set; } = string.Empty;
    public bool IsTested { get; set; }

    // Navigation property to the employee currently assigned to this phone. Nullable as the phone may be unassigned.
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
