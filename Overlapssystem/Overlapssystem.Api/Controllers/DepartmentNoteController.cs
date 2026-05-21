using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Overlapssystem.Api.Mappers;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentNoteController : ControllerBase
{
    private readonly IDepartmentNoteService _departmentNoteService;

    public DepartmentNoteController(IDepartmentNoteService departmentNoteService)
    {
        _departmentNoteService = departmentNoteService;
    }

    [HttpGet("{departmentId}")]
    public async Task<ActionResult<DepartmentNoteDto>> Get(int departmentId)
    {
        // 1. Hent entity fra service
        var note = await _departmentNoteService.GetNoteAsync(departmentId);

        // 2. Map til DTO via mapper
        var dto = DepartmentNoteMapper.ToDto(note);

        return Ok(dto);
    }

    [HttpPut]
    public async Task<IActionResult> Put(DepartmentNoteDto dto)
    {
        // 1. Map DTO til Entity via mapper
        var note = DepartmentNoteMapper.ToEntity(dto);

        // 2. Gem via service
        await _departmentNoteService.SaveNoteAsync(note);

        return NoContent();
    }
}