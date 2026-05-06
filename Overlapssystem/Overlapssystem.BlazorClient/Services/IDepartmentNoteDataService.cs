namespace Overlapssystem.BlazorClient.Services
{
    public interface IDepartmentNoteDataService
    {
            Task<string> GetNoteByDepartmentIdAsync(int id);
            Task SaveNoteAsync(int id, string content);
    }
}
