namespace Overlapssystem.Domain.Entities;

// Represents the assignment of a department task to an employee for a specific shift.
public class ShiftTaskAssignment
{
    public int Id { get; set; }
    public int ShiftId { get; set; }
    public int DepartmentTaskId { get; set; }
    public string AssignedEmployee { get; set; } = string.Empty;

    // Navigation properties to the shift and department task this assignment belongs to.
    public Shift Shift { get; set; } = null!;
    public DepartmentTask DepartmentTask { get; set; } = null!;
}
