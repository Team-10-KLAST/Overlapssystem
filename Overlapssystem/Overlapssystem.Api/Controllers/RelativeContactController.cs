using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
// API controller for relative contact registrations on a resident schema.
public class RelativeContactController : ControllerBase
{
    private readonly IRelativeContactService _service;

    public RelativeContactController(IRelativeContactService service)
    {
        _service = service;
    }

    // GET api/relativecontact/{residentSchemaId}
    // Returns all contacts for the given resident schema, ordered by ID.
    [HttpGet("{residentSchemaId}")]
    public async Task<ActionResult<IEnumerable<RelativeContactDto>>> GetBySchema(int residentSchemaId)
    {
        var contacts = await _service.GetBySchemaAsync(residentSchemaId);

        var dtos = contacts.Select(c => new RelativeContactDto
        {
            Id = c.Id,
            ResidentSchemaId = c.ResidentSchemaId,
            ContactNote = c.ContactNote,
            ContactType = c.ContactType
        });

        return Ok(dtos);
    }

    // POST api/relativecontact
    // Creates a new relative contact and returns the saved DTO with its new ID.
    [HttpPost]
    public async Task<ActionResult<RelativeContactDto>> Create(RelativeContactDto dto)
    {
        var contact = new RelativeContact
        {
            ResidentSchemaId = dto.ResidentSchemaId,
            ContactNote = dto.ContactNote,
            ContactType = dto.ContactType
        };

        var created = await _service.CreateAsync(contact);

        dto.Id = created.Id;
        return Ok(dto);
    }

    // PUT api/relativecontact/{id}
    // Updates an existing relative contact. Returns 404 if not found.
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RelativeContactDto dto)
    {
        var contact = new RelativeContact
        {
            Id = id,
            ResidentSchemaId = dto.ResidentSchemaId,
            ContactNote = dto.ContactNote,
            ContactType = dto.ContactType
        };

        try
        {
            await _service.UpdateAsync(contact);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    // DELETE api/relativecontact/{id}
    // Deletes a relative contact. Returns 404 if not found.
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
