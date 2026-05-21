using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Shared.DTOs;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Enums;
using Overlapssystem.Application.UseCases;
using Overlapssystem.Api.Mappers;

namespace Overlapssystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftController : ControllerBase
{
    //Service til håndtering af shift-logik
    private readonly IShiftService _shiftService;
    private readonly ICreateShiftUseCase _createShiftUseCase;

    //Dependency injection af ShiftService
    public ShiftController(IShiftService shiftService, ICreateShiftUseCase createShiftUseCase)
    {
        _shiftService = shiftService;
        _createShiftUseCase = createShiftUseCase;
    }

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShiftById(int id)
    {
        var shift = await _shiftService.GetShiftByIdAsync(id);
        if (shift == null)
            return NotFound($"Vagt med id {id} blev ikke fundet.");

        return Ok(ShiftMapper.ToDto(shift));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllShifts()
    {
        var shifts = await _shiftService.GetAllShiftsAsync();

        // Mapping fra Shift-entity til ShiftDto
        var dtos = shifts.Select(ShiftMapper.ToDto);

        return Ok(dtos);
    }
}
