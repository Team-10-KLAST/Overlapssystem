using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Shared.DTOs;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Application.Interfaces;

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
        var result = await _shiftService.CreateShiftAsync(dto);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShiftById(int id)
    {
        //Hent vagt via id
        var result = await _shiftService.GetShiftByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllShifts()
    {
        var shifts = await _shiftService.GetShiftsAsync();
        return Ok(shifts);
    }
}
