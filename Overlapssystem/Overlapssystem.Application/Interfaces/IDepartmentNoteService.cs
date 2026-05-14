using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

// Defines the contract for managing department notes
public interface IDepartmentNoteService
{
    Task<DepartmentNote> GetNoteAsync(int departmentId);
    Task SaveNoteAsync(DepartmentNote note);
}