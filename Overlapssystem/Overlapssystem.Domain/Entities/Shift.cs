using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Domain.Entities;

// Represents a shift (dag, aften, or nat) for a specific department on a specific date.
public class Shift
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public ShiftType Type { get; set; }

    // Navigation property to the department this shift belongs to.
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
}