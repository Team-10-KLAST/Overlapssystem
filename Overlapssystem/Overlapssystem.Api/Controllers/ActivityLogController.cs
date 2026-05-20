using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Api.Mappers;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

// API controller for the activity log.
// Only accessible by Admin users.
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ActivityLogController : ControllerBase
{
    private readonly IActivityLogService _service;

    // Constructor — receives the activity log service via dependency injection.
    public ActivityLogController(IActivityLogService service)
    {
        _service = service;
    }

    // GET api/activitylog?employeeId=3&from=2026-05-01&to=2026-05-11
    // Returns all logged actions for a specific employee in the given period.
    [HttpGet]
    public async Task<ActionResult<List<ActivityLogEntryDto>>> GetByEmployee(
        [FromQuery] int employeeId,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        // Fetch log entries from the database via service
        var entries = await _service.GetByEmployeeAsync(employeeId, from, to);

        // Map entities to DTOs in the API layer (our convention)
        var dtos = ActivityLogMapper.ToDtoList(entries);

        return Ok(dtos);
    }
}