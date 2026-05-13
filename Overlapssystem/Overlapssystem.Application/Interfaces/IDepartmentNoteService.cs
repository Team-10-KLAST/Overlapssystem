using Overlapssystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Application.Interfaces
{
    public interface IDepartmentNoteService
    {
        // Henter noten baseret på afdelings-ID
        Task<DepartmentNote> GetNoteAsync(int departmentId);

        // Gemmer eller opdaterer noten for afdelingen
        Task SaveNoteAsync(DepartmentNote note);
    }
}
