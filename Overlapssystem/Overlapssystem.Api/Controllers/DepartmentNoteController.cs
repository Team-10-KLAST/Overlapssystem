using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers
{
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
            //Kalder sercvice
            var note = await _departmentNoteService.GetNoteAsync(departmentId);

            //Map Entity tilbage til dto
            var dto = new DepartmentNoteDto
            {
                DepartmentId = note.DepartmentId,
                Content = note.Content
            };

            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Put(DepartmentNoteDto dto)
        {
            // 1. Map DTO til Entity (da din service tager imod entiteter)
            var note = new DepartmentNote
            {
                DepartmentId = dto.DepartmentId,
                Content = dto.Content
            };

            // 2. Send til din Application Service
            await _departmentNoteService.SaveNoteAsync(note);

            return NoContent();
        }
    }
}