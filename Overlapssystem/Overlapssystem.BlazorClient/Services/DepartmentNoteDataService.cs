using Overlapssystem.Shared.DTOs;
using System.Net.Http.Json; // VIGTIG: Giver adgang til GetFromJsonAsync

namespace Overlapssystem.BlazorClient.Services;

public class DepartmentNoteDataService : IDepartmentNoteDataService
{
    private readonly HttpClient _http;

    public DepartmentNoteDataService(HttpClient http)
    {
        _http = http;
    }

    // Hent noten fra API'en
    public async Task<IEnumerable<DepartmentNoteDto>> GetNoteByDepartmentIdAsync(int departmentId)
    {
        var dto = await _http.GetFromJsonAsync<DepartmentNoteDto>($"api/departmentnote/{departmentId}");

        if (dto == null)
            return new List<DepartmentNoteDto>();

        return new List<DepartmentNoteDto> { dto };
    }
    public async Task UpdateNoteAsync(DepartmentNoteDto dto)
    {
        await _http.PutAsJsonAsync($"api/departmentnote", dto);
    }
}