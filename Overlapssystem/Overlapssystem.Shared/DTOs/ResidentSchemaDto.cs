namespace Overlapssystem.Shared.DTOs;

// Data Transfer Object for a resident's schema card. 
// Combines data from both Resident and ResidentSchema into one 

public class ResidentSchemaDto
{
    public int ResidentSchemaId { get; set; }
    public string Initials { get; set; } = string.Empty;
    public string RelativeContact { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    // Trafficlight send as string so frontend doesnt need to know about our enum.
    public string? TrafficLight { get; set; }
    public string ShoppingDay { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public List<MedicationTimeDto> MedicationTimes { get; set; } = new();
    public int? SelectedPrimaryId { get; set; }
    public int? SelectedSecondaryId { get; set; }
}
