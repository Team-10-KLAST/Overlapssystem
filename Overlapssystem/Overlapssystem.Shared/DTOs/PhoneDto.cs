using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs;

public class PhoneDto
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public int? EmployeeId { get; set; } // Bruges til at binde den valgte medarbejder i dropdownen
    public string EmployeeInitials { get; set; } = string.Empty; // Det, der vises i tabellen
    public bool IsTested { get; set; }
}
