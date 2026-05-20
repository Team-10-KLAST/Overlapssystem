using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs;

// DTO for transferring data when assigning an employee to a resident schema,
// including the position (primary or secondary).
public class AssignEmployeeDto
{
    public int ResidentSchemaId { get; set; }
    public int EmployeeId { get; set; }

    // Used to distinguish between primary (1) and secondary (2) positions for an employee in a resident schema.
    [Range(1, 2, ErrorMessage = "Position skal være 1 (primær) eller 2 (sekundær).")]
    public int Position { get; set; }
}

