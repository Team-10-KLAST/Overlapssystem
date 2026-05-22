using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Api.Mappers;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

// API controller responsible for managing relative contact registrations on a resident schema.
[ApiController]
[Route("api/[controller]")]
public class RelativeContactController : ControllerBase
{
    private readonly IRelativeContactService _service;

    // Constructor — receives the relative contact service via dependency injection.
    public RelativeContactController(IRelativeContactService service)
    {
        _service = service;
    }

    // PUT api/relativecontact/{residentSchemaId}
    // Updates the contact note for a resident schema. Expects a RelativeContactDto in the request body.
    [HttpPut("{residentSchemaId}")]
    public async Task<IActionResult> Update(int residentSchemaId, RelativeContactDto dto)
    {
        try
        {
            await _service.UpdateAsync(residentSchemaId, dto.ContactNote);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
