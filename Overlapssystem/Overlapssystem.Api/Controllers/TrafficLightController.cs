using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

// API controller for managing traffic light statuses for resident schemas.
[ApiController]
[Route("api/[controller]")]
public class TrafficLightController : ControllerBase
{
    private readonly ITrafficLightService _trafficLightService;

    public TrafficLightController(ITrafficLightService trafficLightService)
    {
        _trafficLightService = trafficLightService;
    }

    // PUT api/trafficlight/{id}
    // Updates the traffic light status for a specific resident schema.
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrafficLight(int id, UpdateTrafficLightDto dto)
    {
        try
        {
            var employeeId = int.Parse(User.FindFirstValue("EmployeeId")!);
            await _trafficLightService.UpdateTrafficLightAsync(id, dto.TrafficLight, employeeId);
            return NoContent();
        }
        catch (ArgumentException)
        {
            return BadRequest("Ugyldigt trafiklys-niveau");
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(500, "Noget gik galt");
        }
    }
}