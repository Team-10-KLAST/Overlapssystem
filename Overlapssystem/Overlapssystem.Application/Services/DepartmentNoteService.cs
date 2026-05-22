using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

// Handles retrieving and saving department-level notes. 
public class DepartmentNoteService : IDepartmentNoteService
{
    private readonly IAppDbContext _context;

    public DepartmentNoteService(IAppDbContext context)
    {
        _context = context;
    }

    // Returns the note for a department, or an empty note if none exists. 
    public async Task<DepartmentNote> GetNoteAsync(int departmentId)
    {
        var note = await _context.DepartmentNotes.FindAsync(departmentId);
        return note ?? new DepartmentNote { DepartmentId = departmentId, Content = string.Empty };
    }

    // Creates or updates the note for a department. 
    public async Task SaveNoteAsync(DepartmentNote note)
    {
        var existingNote = await _context.DepartmentNotes.FindAsync(note.DepartmentId);

        if (existingNote == null)
        {
            _context.DepartmentNotes.Add(new DepartmentNote
            {
                DepartmentId = note.DepartmentId,
                Content = note.Content,
                LastUpdated = DateTime.Now
            });
        }
        else
        {
            existingNote.Content = note.Content;
            existingNote.LastUpdated = DateTime.Now;
        }

        await _context.SaveChangesAsync();
    }
}