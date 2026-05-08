using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Domain.Entities;

public class MedicationTime
{
    public int MedicationTimeId { get; set; }
    public int ResidentId { get; set; }
    public TimeOnly PlannedTime { get; set; }

    // Navigation property to Resident. Each MedicationTime is associated with one Resident.
    public Resident Resident { get; set; } = null!;
}

