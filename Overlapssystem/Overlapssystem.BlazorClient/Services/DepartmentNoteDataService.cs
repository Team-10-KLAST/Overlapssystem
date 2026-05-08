using Overlapssystem.Shared.DTOs;
using System.Net.Http.Json; // VIGTIG: Giver adgang til GetFromJsonAsync

namespace Overlapssystem.BlazorClient.Services
{
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
            // Vi henter hele DTO'en i stedet for bare en streng
            var dto = await _http.GetFromJsonAsync<DepartmentNoteDto>($"api/departmentnote/{departmentId}");
            return (IEnumerable<DepartmentNoteDto>)(dto ?? new DepartmentNoteDto { DepartmentId = departmentId, Content = string.Empty });
        }

        public async Task UpdateNoteAsync(DepartmentNoteDto dto)
        {
            await _http.PutAsJsonAsync($"api/departmentnote", dto);
        }
    }
}