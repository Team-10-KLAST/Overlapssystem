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
        Task<string> GetNoteByDepartmentIdAsync(int departmentId);

        // Gemmer eller opdaterer noten for afdelingen
        Task SaveNoteAsync(int departmentId, string content);
    }
}
