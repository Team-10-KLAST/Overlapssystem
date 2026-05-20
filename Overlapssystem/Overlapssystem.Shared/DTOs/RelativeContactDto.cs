namespace Overlapssystem.Shared.DTOs;

// Data Transfer Object for a relative contact registration.
// Used to send relative contact data between frontend and backend.
public class RelativeContactDto
{
    public int Id { get; set; }

    public int ResidentSchemaId { get; set; }

    // Free-text description of the contact (who called, what was discussed, etc.).
    public string ContactNote { get; set; } = string.Empty;

    // Type of contact: "Note" for a regular message, "Begivenhed" for a future event.
    public string ContactType { get; set; } = "Note";
}
