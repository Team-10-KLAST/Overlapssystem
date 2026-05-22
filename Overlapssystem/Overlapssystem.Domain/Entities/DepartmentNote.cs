using System.ComponentModel.DataAnnotations;

namespace Overlapssystem.Domain.Entities;

// Represents a shared note (huskeseddel) for a department.
// There is one note per department, identified by DepartmentId as the primary key.
public class DepartmentNote
{
    [Key]
    public int DepartmentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; } = DateTime.Now;
}
