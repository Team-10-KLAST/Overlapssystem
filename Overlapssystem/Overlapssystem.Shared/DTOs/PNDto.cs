using System.ComponentModel.DataAnnotations;
using Overlapssystem.Shared.Validation;

namespace Overlapssystem.Shared.DTOs;

// DTO for transferring PN medication registration data between the client and the API.
public class PNDto
{
    public int PNId { get; set; }

    [Required]
    [ValidTimeFormat]
    public string GivenTime { get; set; } = string.Empty;

    public int ResidentSchemaId { get; set; }
}
