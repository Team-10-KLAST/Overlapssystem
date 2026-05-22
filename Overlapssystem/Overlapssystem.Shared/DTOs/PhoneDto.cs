using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs;

// DTO representing a work phone entry with its current employee assignment and tested status.
public class PhoneDto
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public int? EmployeeId { get; set; } // Used to bind the selected employee in the dropdown
    public string EmployeeInitials { get; set; } = string.Empty;
    public bool IsTested { get; set; }
}
