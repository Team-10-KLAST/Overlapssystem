using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Shared.DTOs;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Enums;
using Overlapssystem.Application.UseCases;

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
        try
        {
            var shift = await _createShiftUseCase.ExecuteAsync(
                dto.Date,
                (ShiftType)dto.ShiftType,
                dto.DepartmentId
            );
            return Ok(new ShiftDto
            {
                Id = shift.Id.ToString(),
                DateTime = shift.Date.ToString("o"),
                ShiftType = shift.Type.ToString(),
                DepartmentId = shift.DepartmentId.ToString()
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShiftById(int id)
    {
        var shift = await _shiftService.GetShiftByIdAsync(id);
        if (shift == null)
            return NotFound($"Vagt med id {id} blev ikke fundet.");

        var shiftDto = new ShiftDto
        {
            Id = shift.Id.ToString(),
            DateTime = shift.Date.ToString("o"),
            ShiftType = shift.Type.ToString(),
            DepartmentId = shift.DepartmentId.ToString()
        };
        return Ok(shiftDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllShifts()
    {
        var shifts = await _shiftService.GetAllShiftsAsync();

        // Mapping fra Shift-entity til ShiftDto
        var dtos = shifts.Select(shift => new ShiftDto
        {
            Id = shift.Id.ToString(),
            DateTime = shift.Date.ToString("o"), // ISO 8601 format
            ShiftType = shift.Type.ToString(),
            DepartmentId = shift.DepartmentId.ToString()
        });

        return Ok(dtos);
    }
}
