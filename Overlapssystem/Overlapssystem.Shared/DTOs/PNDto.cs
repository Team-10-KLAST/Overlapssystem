namespace Overlapssystem.Shared.DTOs;

// Data Transfer Object for a PN medication registration.
// Used to send PN data between frontend and backend.
public class PNDto
{
    public int PNId { get; set; }

    // Sent as string so frontend doesn't need to know about TimeOnly.
    public string GivenTime { get; set; } = string.Empty;

    public int ResidentSchemaId { get; set; }
}
