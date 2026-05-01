using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

// API controller for updating resident status

[ApiController]
[Route("api/[controller]")]
public class ResidentStatusController : ControllerBase
{
    private readonly IResidentStatusService _service;

    public ResidentStatusController(IResidentStatusService service)
    {
        _service = service;
            
    }


    // PUT api/residentstatus/5
    // Updates the status text for the resident schema with the given ID.
    // Returns 204 No Content on success, 404 Not Found if schema doesn't exist.
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto dto)
    {
        // Call service to update the status in the database
        var success = await _service.UpdateStatusAsync(id, dto.Status);

        // if schema was not found, return 404
        if (!success)
        {
            return NotFound();
        }

        // return 204 no content - update succeeded, no data tyo return. 
        return NoContent();
    }
}
