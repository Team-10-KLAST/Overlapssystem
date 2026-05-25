using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Handles saving new shifts to the API.
public class SaveShiftService : ISaveShiftService
{
    private readonly HttpClient _http;

    public SaveShiftService(HttpClient http)
    {
        _http = http;
    }

    // Saves a new shift to the API. Validates the input model before sending the request.
    public async Task<Result<int>> SaveShiftAsync(CreateShiftDto model)
    {
        if (model.Date.Date > DateTime.Today)
            return Result<int>.Failure("Du kan ikke vælge en dato i fremtiden.");
        if (model.ShiftType == 0)
            return Result<int>.Failure("Husk at vælge vagt type.");
        if (model.DepartmentId == 0)
            return Result<int>.Failure("Husk at vælge afdeling.");

        try
        {
            var response = await _http.PostAsJsonAsync("api/shift", model);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return Result<int>.Failure($"Der skete en fejl, da vagten skulle gemmes: {error}");
            }

            var shift = await response.Content.ReadFromJsonAsync<ShiftDto>();
            if (int.TryParse(shift?.Id, out var shiftId))
                return Result<int>.Success(shiftId);

            return Result<int>.Failure("Kunne ikke læse vagt-id fra serveren.");
        }
        catch (HttpRequestException)
        {
            return Result<int>.Failure("Ingen forbindelse til serveren.");
        }
    }
}


