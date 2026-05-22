using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Api.Mappers;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

//  API controller for reisent schemas
// Handles HTTP requests from Blazor frontend
[ApiController]
[Route("api/[controller]")]
public class ResidentSchemaController : ControllerBase
{   
    private readonly IResidentSchemaService _service;
    private readonly IAssignEmployeeService _assignService;

    // Constructor — receives the resident schema service and assign employee service via dependency injection.
    public ResidentSchemaController(IResidentSchemaService service, IAssignEmployeeService assignService)
    {
        _service = service;
        _assignService = assignService;
    }

    // Returns all resident schemas as DTOs. 
    // GET api/residentschema
    [HttpGet("shift/{shiftId}")]
    public async Task<ActionResult<IEnumerable<ResidentSchemaDto>>> GetByShift(int shiftId)
    {
        var schemas = await _service.GetByShiftAsync(shiftId);
        var dtos = schemas.Select(ResidentSchemaMapper.ToDto);
        return Ok(dtos);
    }

    // Assigns an employee to a resident schema.
    // POST api/residentschema/assign
    [HttpPost("assign")]
    public async Task<IActionResult> AssignEmployee([FromBody] AssignEmployeeDto dto)
    {
        var success = await _assignService.AssignEmployeeAsync(
            dto.ResidentSchemaId,
            dto.EmployeeId,
            dto.Position);

        if (!success)
        {
            return BadRequest("Kunne ikke udføre tildelingen i databasen.");
        }

        return Ok();
    }
}
