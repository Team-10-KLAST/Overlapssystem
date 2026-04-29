using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Domain.Entities;

public class ResidentSchema
{
    public int ResidentSchemaId { get; set; }
    public string RelativeContact { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public TrafficLight TrafficLight { get; set; }
    public int ResidentId { get; set; }

    // Navigation property - lets us access the actual Resident object
    public Resident Resident { get; set; } = null!;


    //NOTE: ShiftId will be added later then Shift feature is ready.
}
