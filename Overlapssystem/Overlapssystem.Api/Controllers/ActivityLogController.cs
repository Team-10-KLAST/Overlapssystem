using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Api.Mappers;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

// API controller responsible for exposing the activity log endpoints.
// Only accessible by users with the "Admin" role.
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

    // Returns all logged actions for a specific employee in the given period.
    // GET api/activitylog?employeeId=123&from=2024-01-01&to=2024-01-31
    [HttpGet]
    public async Task<ActionResult<List<ActivityLogEntryDto>>> GetByEmployee(
        [FromQuery] int employeeId,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        var entries = await _service.GetByEmployeeAsync(employeeId, from, to);

        var dtos = ActivityLogMapper.ToDtoList(entries);

        return Ok(dtos);
    }
}