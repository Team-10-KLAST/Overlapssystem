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

    public ResidentSchemaController(IResidentSchemaService service)
    {
        _service = service;
    }

    // GET api/residentschema
    // Returns all resident schemas as DTOs. 
    [HttpGet("shift/{shiftId}")]
    public async Task<ActionResult<IEnumerable<ResidentSchemaDto>>> GetByShift(int shiftId)
    {
        var schemas = await _service.GetByShiftAsync(shiftId);
        var dtos = schemas.Select(ResidentSchemaMapper.ToDto);
        return Ok(dtos);
    }
}
