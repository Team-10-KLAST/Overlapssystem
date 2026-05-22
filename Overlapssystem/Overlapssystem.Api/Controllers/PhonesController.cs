using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Api.Mappers;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

// API controller responsible for managing phone entries, allowing retrieval,
// creation, updating, and deletion of phone data.
[ApiController]
[Route("api/[controller]")]
public class PhonesController : ControllerBase
{
    private readonly IPhoneService _phoneService;

    // Constructor — receives the phone service via dependency injection.
    public PhonesController(IPhoneService phoneService)
    {
        _phoneService = phoneService;
    }

    // Gets a list of all phone entries mapped to DTOs
    // GET: api/phones
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PhoneDto>>> GetAll()
    {
        var phones = await _phoneService.GetAllPhonesAsync();

        var dtos = phones.Select(PhoneMapper.ToDto);

        return Ok(dtos);
    }

    // Creates a new phone entry and returns its generated Id
    // POST: api/phones
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return BadRequest("Telefonnummer må ikke være tomt.");
        }

        var phoneId = await _phoneService.CreatePhoneAsync(phoneNumber);

        return Ok(phoneId);
    }

    // Updates the employee assignment and checked status for a specific phone entry
    // PUT: api/phones/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PhoneDto dto)
    {
        if (dto == null || id != dto.Id)
        {
            return BadRequest("Ugyldige data eller ID-match fejl.");
        }

        await _phoneService.UpdatePhoneAssignmentAsync(id, dto.EmployeeId, dto.IsTested);

        return NoContent();
    }

    // Permanently deletes a phone entry by its Id
    // DELETE: api/phones/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _phoneService.DeletePhoneAsync(id);

        return NoContent();
    }
}
