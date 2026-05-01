using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Overlapssystem.Shared.DTOs;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.BlazorClient.Services
{
    public class SaveShiftService
    {
        private readonly HttpClient _http;

        public SaveShiftService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> SaveShiftAsync(CreateShiftDto model)
        {
            if (model.Date.Date > DateTime.Today)
                return "Du kan ikke vælge en dato i fremtiden.";

            if (model.Department == DepartmentType.None)
                return "Husk at vælge afdeling.";

            try
            {
                var response = await _http.PostAsJsonAsync("api/shift", model);
                if (response.IsSuccessStatusCode)
                {
                    return $"Vagt gemt: {model.Date:yyyy-MM-dd} - {model.ShiftType} - {model.Department}";
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return $"Der skete en fejl, da vagten skulle gemmes: {error}";
                }
            }
            catch (Exception ex)
            {
                return $"Uventet fejl: {ex.Message}";
            }
        }
    }
}