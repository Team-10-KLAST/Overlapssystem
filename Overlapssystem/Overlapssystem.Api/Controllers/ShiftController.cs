using System;
using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateShift(CreateShiftDto dto)
    {
        // midlertidigt:
        return Ok(dto);
    }
}
