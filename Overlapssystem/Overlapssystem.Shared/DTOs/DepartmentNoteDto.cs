using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs;

// DTO representing a department note that persists across shifts for a specific department.
public class DepartmentNoteDto
{
    public int DepartmentId { get; set; }
    public string Content { get; set; } = string.Empty;
}
