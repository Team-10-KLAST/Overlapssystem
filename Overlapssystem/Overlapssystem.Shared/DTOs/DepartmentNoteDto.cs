using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs;

public class DepartmentNoteDto
{
    public int DepartmentId { get; set; }
    public string Content { get; set; } = string.Empty;
}
