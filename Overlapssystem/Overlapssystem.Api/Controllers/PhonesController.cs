using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Api.Mappers;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhonesController : ControllerBase
{
    private readonly IPhoneService _phoneService;

    public PhonesController(IPhoneService phoneService)
    {
        _phoneService = phoneService;
    }

    // GET: api/phones
    // Gets a list of all phone entries mapped to DTOs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PhoneDto>>> GetAll()
    {
        var phones = await _phoneService.GetAllPhonesAsync();

        var dtos = phones.Select(PhoneMapper.ToDto);

        return Ok(dtos);
    }

    // POST: api/phones
    // Creates a new phone entry and returns its generated Id
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

    // PUT: api/phones/{id}
    // Updates the employee assignment and checked status for a specific phone entry
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

    // DELETE: api/phones/{id}
    // Deletes a phone entry by its Id
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _phoneService.DeletePhoneAsync(id);

        return NoContent();
    }
}
