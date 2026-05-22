using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs;

// DTO combining data from MedicationTime and MedicationRegistration.
// Represents a single planned medication time and its administration status for a specific shift.
public class MedicationTimeDto
{
    public int MedicationTimeId { get; set; }
    public string PlannedTime { get; set; } = string.Empty;
    public bool IsGiven { get; set; }
    public DateTime? GivenTime { get; set; }
}
