namespace Overlapssystem.Domain.Entities;

// Represents a department (e.g. Skoven or Slottet) that houses residents and is associated with shifts.
public class Department
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
}
