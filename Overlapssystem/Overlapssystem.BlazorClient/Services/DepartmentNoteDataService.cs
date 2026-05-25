using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Handles fetching and updating department notes from the API.
public class DepartmentNoteDataService : IDepartmentNoteDataService
{
    private readonly HttpClient _http;

    public DepartmentNoteDataService(HttpClient http)
    {
        _http = http;
    }

    // Fetches the note for a specific department. Returns an empty list if no note exists.
    public async Task<Result<IEnumerable<DepartmentNoteDto>>> GetNoteByDepartmentIdAsync(int departmentId)
    {
        try
        {
            var dto = await _http.GetFromJsonAsync<DepartmentNoteDto>($"api/departmentnote/{departmentId}");

            if (dto == null)
                return Result<IEnumerable<DepartmentNoteDto>>.Success(new List<DepartmentNoteDto>());

            return Result<IEnumerable<DepartmentNoteDto>>.Success(new List<DepartmentNoteDto> { dto });
        }
        catch (HttpRequestException)
        {
            return Result<IEnumerable<DepartmentNoteDto>>.Failure("Kunne ikke hente note.");
        }
    }

    // Updates the note for a department.
    public async Task<Result> UpdateNoteAsync(DepartmentNoteDto dto)
    {
        try
        {
            var response = await _http.PutAsJsonAsync("api/departmentnote", dto);
            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke gemme note.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }
}