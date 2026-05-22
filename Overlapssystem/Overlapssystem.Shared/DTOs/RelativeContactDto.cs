namespace Overlapssystem.Shared.DTOs;

// DTO for transferring relative contact registration data between the client and the API.
public class RelativeContactDto
{
    public int Id { get; set; }
    public int ResidentSchemaId { get; set; }
    public string ContactNote { get; set; } = string.Empty;
    public string ContactType { get; set; } = "Note";
}
