using System.ComponentModel.DataAnnotations;

namespace Overlapssystem.Shared.DTOs;

// Data Transfer Object for a PN medication registration.
// Used to send PN data between frontend and backend.
public class PNDto
{
    public int PNId { get; set; }

    // Sent as string so frontend doesn't need to know about TimeOnly.
    // Validated to ensure it follows the HH:mm format.
    [Required]
    [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "Tidsformat skal være HH:mm")]
    public string GivenTime { get; set; } = string.Empty;

    public int ResidentSchemaId { get; set; }
}
