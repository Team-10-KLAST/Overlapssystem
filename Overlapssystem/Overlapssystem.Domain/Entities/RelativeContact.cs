namespace Overlapssystem.Domain.Entities;

// Represents a relative contact entry for a resident.
public class RelativeContact
{
    public int Id { get; set; }

    // Foreign key — which resident schema does this contact belong to?
    public int ResidentSchemaId { get; set; }

    // Navigation property back to ResidentSchema.
    public ResidentSchema ResidentSchema { get; set; } = null!;

    // Descriptive note about the contact.
    public string ContactNote { get; set; } = string.Empty;

    // Type of contact: "Note" for a regular message, "Begivenhed" for a future event.
    public string ContactType { get; set; } = "Note";
}
