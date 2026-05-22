using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Overlapssystem.Api.Mappers;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

// API controller responsible for managing department notes, allowing retrieval and updates
// of notes associated with specific departments.
// Each department has one persistent note that persits across shifts and it is updated manually.
[ApiController]
[Route("api/[controller]")]
public class DepartmentNoteController : ControllerBase
{
    private readonly IDepartmentNoteService _departmentNoteService;

    // Constructor — receives the department note service via dependency injection.
    public DepartmentNoteController(IDepartmentNoteService departmentNoteService)
    {
        _departmentNoteService = departmentNoteService;
    }

    // Returns the note for a specific department
    // GET api/departmentnote/{departmentId}
    [HttpGet("{departmentId}")]
    public async Task<ActionResult<DepartmentNoteDto>> Get(int departmentId)
    {
        var note = await _departmentNoteService.GetNoteAsync(departmentId);

        var dto = DepartmentNoteMapper.ToDto(note);

        return Ok(dto);
    }

    // Updates the note for a specific department
    // PUT api/departmentnote
    [HttpPut]
    public async Task<IActionResult> Put(DepartmentNoteDto dto)
    {
        var note = DepartmentNoteMapper.ToEntity(dto);

        await _departmentNoteService.SaveNoteAsync(note);

        return NoContent();
    }
}