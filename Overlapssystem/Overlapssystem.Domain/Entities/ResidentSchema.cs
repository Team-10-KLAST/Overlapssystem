using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Domain.Entities;

public class ResidentSchema
{
    // Primary key. 
    public int ResidentSchemaId { get; set; }
    public string RelativeContact { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    // Visual indicator of a resident's state. Enum -> stored as int in DB. 
    public TrafficLight TrafficLight { get; set; }
    // Foreign Key to the Resident this schema belongs to.
    public int ResidentId { get; set; }

    // Navigation property - lets us access the actual Resident object
    public Resident Resident { get; set; } = null!;


    //NOTE: ShiftId will be added later then Shift feature is ready.
}
