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
        public async Task<string> GetNoteByDepartmentIdAsync(int id)
        {
            // Vi henter hele DTO'en i stedet for bare en streng
            var dto = await _http.GetFromJsonAsync<DepartmentNoteDto>($"api/departmentnote/{id}");
            return dto?.Content ?? string.Empty;
        }

        public async Task SaveNoteAsync(int id, string content)
        {
            var dto = new DepartmentNoteDto { DepartmentId = id, Content = content };
            await _http.PostAsJsonAsync($"api/departmentnote", dto);
        }
    }
}