using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Domain.Entities;

// Represents a record of medication administration for a resident in
// a specific resident schema at a specific medication time.
public class MedicationRegistration
{
    public int MedicationRegistrationId { get; set; }
    public DateTime GivenTime { get; set; }
    public bool Given { get; set; }
    public int ResidentSchemaId { get; set; }
    public int MedicationTimeId { get; set; }

    // Navigation properties
    public ResidentSchema ResidentSchema { get; set; } = null!;
    public MedicationTime MedicationTime { get; set; } = null!;
}

