namespace Overlapssystem.Domain.Entities;

// Represents a task (e.g. Medicinansvarlig, Aftensmad) that belongs to a department.
public class DepartmentTask
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public string TaskName { get; set; } = string.Empty;

    // Navigation property to the department this task belongs to.
    public Department Department { get; set; } = null!;
}