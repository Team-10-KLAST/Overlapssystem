namespace Overlapssystem.Shared.DTOs;

// DTO combining data from both Resident and ResidentSchema into a single card representation.
// Used to display a resident's full overview on the overlap dashboard.
public class ResidentSchemaDto
{
    public int ResidentSchemaId { get; set; }
    public string Initials { get; set; } = string.Empty;
    public string RelativeContact { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    // Sent as string so the frontend does not need to handle the TrafficLight enum directly.
    public string? TrafficLight { get; set; }

    public string ShoppingDay { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public List<MedicationTimeDto> MedicationTimes { get; set; } = new();
    public int? SelectedPrimaryId { get; set; }
    public int? SelectedSecondaryId { get; set; }
}
