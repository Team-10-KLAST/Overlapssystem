using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Shared.DTOs;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Enums;
using Overlapssystem.Application.UseCases;
using Overlapssystem.Api.Mappers;

namespace Overlapssystem.Api.Controllers;

// API controller responsible for managing shifts, including creating new shifts and retrieving existing ones.
[ApiController]
[Route("api/[controller]")]
public class ShiftController : ControllerBase
{
    private readonly IShiftService _shiftService;
    private readonly ICreateShiftUseCase _createShiftUseCase;

    // Constructor — receives the shift service and create shift use case via dependency injection.
    public ShiftController(IShiftService shiftService, ICreateShiftUseCase createShiftUseCase)
    {
        _shiftService = shiftService;
        _createShiftUseCase = createShiftUseCase;
    }

    // Creates a new shift based on the provided data transfer object (DTO).
    // POST api/shift
    [HttpPost]
    public async Task<IActionResult> CreateShift(CreateShiftDto dto)
    {
        var shift = await _createShiftUseCase.ExecuteAsync(
            dto.Date,
            (ShiftType)dto.ShiftType,
            dto.DepartmentId
        );
        return Ok(ShiftMapper.ToDto(shift));
    }

    // Retrieves a specific shift by its ID.
    // GET api/shift/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetShiftById(int id)
    {
        var shift = await _shiftService.GetShiftByIdAsync(id);
        if (shift == null)
            return NotFound($"Vagt med id {id} blev ikke fundet.");

        return Ok(ShiftMapper.ToDto(shift));
    }

    // Retrieves all shifts in the system.
    // GET api/shift
    [HttpGet]
    public async Task<IActionResult> GetAllShifts()
    {
        var shifts = await _shiftService.GetAllShiftsAsync();

        var dtos = shifts.Select(ShiftMapper.ToDto);

        return Ok(dtos);
    }
}
