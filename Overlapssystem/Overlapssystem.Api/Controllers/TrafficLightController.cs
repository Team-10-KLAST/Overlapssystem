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
        var employeeId = int.Parse(User.FindFirstValue("EmployeeId")!);
        var success = await _trafficLightService.UpdateTrafficLightAsync(id, dto.TrafficLight, employeeId);

        if (!success) return NotFound();

        return NoContent();
    }
}