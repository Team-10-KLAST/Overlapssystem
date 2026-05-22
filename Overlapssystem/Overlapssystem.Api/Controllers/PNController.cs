using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Api.Mappers;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

// API controller for PN medication registrations.
// Handles registering and retrieving as-needed medication for a resident schema.
[ApiController]
[Route("api/[controller]")]
public class PNController : ControllerBase
{
    private readonly IPNService _service;

    // Constructor — receives the PN service via dependency injection.
    public PNController(IPNService service)
    {
        _service = service;
    }

    // POST api/pn
    // Registers a new PN medication with a given time for a resident schema.
    [HttpPost]
    public async Task<ActionResult<PNDto>> Register(PNDto dto)
    {
        var employeeId = int.Parse(User.FindFirstValue("EmployeeId")!);
        var id = await _service.RegisterPNAsync(dto.ResidentSchemaId, TimeOnly.Parse(dto.GivenTime), employeeId);
        return Ok(PNMapper.ToDto(id, dto.ResidentSchemaId, dto.GivenTime));
    }

    // GET api/pn/{residentSchemaId}
    // Returns all PN registrations for a specific resident schema.
    [HttpGet("{residentSchemaId}")]
    public async Task<ActionResult<IEnumerable<PNDto>>> GetBySchema(int residentSchemaId)
    {
        var pns = await _service.GetPNsBySchemaAsync(residentSchemaId);
        var dtos = pns.Select(PNMapper.ToDto);
        return Ok(dtos);
    }

    // DELETE api/pn/{id}
    // Deletes a PN registration permanently. Returns 404 if not found.
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employeeId = int.Parse(User.FindFirstValue("EmployeeId")!);
        var success = await _service.DeletePNAsync(id, employeeId);
        if (!success) return NotFound();
        return NoContent();
    }
}