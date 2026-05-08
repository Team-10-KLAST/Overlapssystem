using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services
{
    public interface IDepartmentNoteDataService
    {
            Task<IEnumerable<DepartmentNoteDto>> GetNoteByDepartmentIdAsync(int departmentId);
            Task UpdateNoteAsync(DepartmentNoteDto dto);
    }
}
