using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for fetching and updating department notes through the API.
public interface IDepartmentNoteDataService
{
        Task<Result<IEnumerable<DepartmentNoteDto>>> GetNoteByDepartmentIdAsync(int departmentId);
        Task<Result> UpdateNoteAsync(DepartmentNoteDto dto);
}
