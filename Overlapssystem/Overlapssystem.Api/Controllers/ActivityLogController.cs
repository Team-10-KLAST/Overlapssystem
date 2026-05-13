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
        var medications = await _service.GetMedicationLogAsync(employeeId, from, to);
        var schemas = await _service.GetResidentSchemaLogAsync(employeeId, from, to);

        var dtos = ActivityLogMapper.ToDtoList(medications, schemas);

        return Ok(dtos);
    }
}