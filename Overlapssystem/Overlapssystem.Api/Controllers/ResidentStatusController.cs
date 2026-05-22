using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

// API controller for updating resident status text in the resident schema.
[ApiController]
[Route("api/[controller]")]
public class ResidentStatusController : ControllerBase
{
    private readonly IResidentStatusService _service;

    // Constructor — receives the resident status service via dependency injection.
    public ResidentStatusController(IResidentStatusService service)
    {
        _service = service;
    }

    // Updates the status text for the resident schema with the given ID.
    // Returns 204 No Content on success, 404 Not Found if schema doesn't exist.
    // PUT api/residentstatus/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto dto)
    {
        var employeeId = int.Parse(User.FindFirstValue("EmployeeId")!);
        var success = await _service.UpdateStatusAsync(id, dto.Status, employeeId);

        if (!success)
            return NotFound();

        return NoContent();
    }
}