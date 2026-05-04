using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers
{
    // API controller for medication administration.
    // Handles marking planned medication times as given in a specific shift.
    [ApiController]
    [Route("api/[controller]")]
    public class MedicationController : ControllerBase
    {
        private readonly IMedicationService _service;

        public MedicationController(IMedicationService service)
        {
            _service = service;
        }

        // POST api/medication/mark-given
        // Marks a planned medication time as given in a specific shift.
        [HttpPost("mark-given")]
        public async Task<IActionResult> MarkGiven(MarkMedicationGivenDto dto)
        {
            try
            {
                await _service.MarkAsGivenAsync(dto.ResidentSchemaId, dto.MedicationTimeId);
                return NoContent();
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
}
