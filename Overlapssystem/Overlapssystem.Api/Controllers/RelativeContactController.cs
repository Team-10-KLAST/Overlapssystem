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

    // Returns all contacts for the given resident schema, ordered by ID.
    // GET api/relativecontact/{residentSchemaId}
    [HttpGet("{residentSchemaId}")]
    public async Task<ActionResult<IEnumerable<RelativeContactDto>>> GetBySchema(int residentSchemaId)
    {
        var contacts = await _service.GetBySchemaAsync(residentSchemaId);

        var dtos = contacts.Select(RelativeContactMapper.ToDto);

        return Ok(dtos);
    }

    // Creates a new relative contact and returns the saved DTO with its new ID.
    // POST api/relativecontact
    [HttpPost]
    public async Task<ActionResult<RelativeContactDto>> Create(RelativeContactDto dto)
    {
        var contact = RelativeContactMapper.ToEntity(dto);
        var created = await _service.CreateAsync(contact);
        return Ok(RelativeContactMapper.ToDto(created));
    }

    // Updates an existing relative contact. Returns 404 if not found.
    // PUT api/relativecontact/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RelativeContactDto dto)
    {
        var contact = RelativeContactMapper.ToEntity(id, dto);

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

    // Permanently deletes a relative contact. Returns 404 if not found.
    // DELETE api/relativecontact/{id}
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
