using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Application.Services
{
    public class DepartmentNoteService : IDepartmentNoteService
    {
        private readonly IAppDbContext _context;

        public DepartmentNoteService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetNoteByDepartmentIdAsync(int departmentId)
        {
            var note = await _context.DepartmentNotes.FindAsync(departmentId);
            return note?.Content ?? string.Empty;
        }

        public async Task SaveNoteAsync(int departmentId, string content)
        {
            var existingNote = await _context.DepartmentNotes.FindAsync(departmentId);

            if (existingNote == null)
            {
                _context.DepartmentNotes.Add(new DepartmentNote
                {
                    DepartmentId = departmentId,
                    Content = content,
                    LastUpdated = DateTime.Now
                });
            }
            else
            {
                existingNote.Content = content;
                existingNote.LastUpdated = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }
    }
}