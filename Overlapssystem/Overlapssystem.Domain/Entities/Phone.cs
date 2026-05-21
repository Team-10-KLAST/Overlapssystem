using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Domain.Entities;

public class Phone
{
    public int Id { get; set; } 
    public string Number { get; set; } = string.Empty;

    // Tilføj denne til at holde styr på den valgte medarbejders ID i dropdownen
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public bool IsTested { get; set; }
}
