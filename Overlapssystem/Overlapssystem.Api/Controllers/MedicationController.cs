using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

// API controller for medication administration.
// Handles marking planned medication times as given in a specific shift.
[ApiController]
[Route("api/[controller]")]
public class MedicationController : ControllerBase
{
    private readonly IMedicationService _service;

    // Constructor — receives the medication service via dependency injection.
    public MedicationController(IMedicationService service)
    {
        _service = service;
    }

    // Marks a planned medication time as given in a specific shift.
    // POST api/medication/mark-given
    [HttpPost("mark-given")]
    public async Task<IActionResult> MarkGiven(MarkMedicationGivenDto dto)
    {
        var employeeId = int.Parse(User.FindFirstValue("EmployeeId")!);
        await _service.MarkAsGivenAsync(dto.ResidentSchemaId, dto.MedicationTimeId, employeeId);
        return NoContent();
    }
}
