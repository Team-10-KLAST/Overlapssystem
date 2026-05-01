using Microsoft.AspNetCore.Mvc;
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
        // Fetch entities from the service 
        var schemas = await _service.GetByShiftAsync(shiftId);

        // Map entities to DTOs 
        var dtos = schemas.Select(s => new ResidentSchemaDto
        {
            ResidentSchemaId = s.ResidentSchemaId,
            Initials = s.Resident.Initials,
            RelativeContact = s.RelativeContact,
            Status = s.Status,
            TrafficLight = s.TrafficLight?.ToString(),
            ShoppingDay = s.Resident.ShoppingDay,
            MedicinTime = s.Resident.MedicinTime.ToString("HH:mm"),
            PaymentMethod = s.Resident.PaymentMethod.ToString()
        });

        // Return with HTTP 200 OK
        return Ok(dtos);
    }
}
