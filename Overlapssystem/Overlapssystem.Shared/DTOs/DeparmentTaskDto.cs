using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs;

// DTO representing a department task with its current employee assignment for a specific shift.
public class DepartmentTaskDto
{
    public int Id { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public string AssignedEmployee { get; set; } = string.Empty;
}