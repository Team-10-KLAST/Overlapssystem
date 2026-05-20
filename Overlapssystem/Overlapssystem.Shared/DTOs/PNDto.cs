using System.ComponentModel.DataAnnotations;
using Overlapssystem.Shared.Validation;

namespace Overlapssystem.Shared.DTOs;

// Data Transfer Object for a PN medication registration.
// Used to send PN data between frontend and backend.
public class PNDto
{
    public int PNId { get; set; }

    // Sent as string so frontend doesn't need to know about TimeOnly.
    // Validated to ensure it is a validd time format.
    [Required]
    [ValidTimeFormat]
    public string GivenTime { get; set; } = string.Empty;

    public int ResidentSchemaId { get; set; }
}
