using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Shared.DTOs;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftController : ControllerBase
{
    //Service til håndtering af shift-logik
    private readonly IShiftService _shiftService;

    //Dependency injection af ShiftService
    public ShiftController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShift(CreateShiftDto dto)
    {
        var shift = await _shiftService.CreateShiftAsync(
        dto.Date,
        (ShiftType)dto.ShiftType,
        dto.DepartmentId
        );
        return Ok(shift);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShiftById(int id)
    {
        //Hent vagt via id  
        var shift = await _shiftService.GetShiftByIdAsync(id);

        var shiftDto = new ShiftDto
        {
            Id = shift.Id.ToString(),
            DateTime = shift.Date.ToString("o"), //ISO 8601 format
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
