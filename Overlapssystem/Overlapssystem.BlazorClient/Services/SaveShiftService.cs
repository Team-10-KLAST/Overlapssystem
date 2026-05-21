using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public class SaveShiftService : ISaveShiftService
{
    private readonly HttpClient _http;

    public SaveShiftService(HttpClient http)
    {
        _http = http;
    }

    // Returns the created shift's ID on success, or null on failure.
    // Client-side validation for immediate user feedback.
    // The same rules are enforced by model validation on the CreateShiftDto in the Shared project.
    public async Task<(int? ShiftId, string Message)> SaveShiftAsync(CreateShiftDto model)
    {
        if (model.Date.Date > DateTime.Today)
            return (null, "Du kan ikke vælge en dato i fremtiden.");
        if (model.ShiftType == 0)
            return (null, "Husk at vælge vagt type.");
        if (model.DepartmentId == 0)
            return (null, "Husk at vælge afdeling.");

        try
        {
            var response = await _http.PostAsJsonAsync("api/shift", model);
            if (response.IsSuccessStatusCode)
            {
                var shift = await response.Content.ReadFromJsonAsync<ShiftDto>();
                if (int.TryParse(shift?.Id, out var shiftId))
                    return (shiftId, string.Empty);
                return (null, "Kunne ikke læse vagt-id fra serveren.");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return (null, $"Der skete en fejl, da vagten skulle gemmes: {error}");
            }
        }
        catch (Exception ex)
        {
            return (null, $"Uventet fejl: {ex.Message}");
        }
    }
}

