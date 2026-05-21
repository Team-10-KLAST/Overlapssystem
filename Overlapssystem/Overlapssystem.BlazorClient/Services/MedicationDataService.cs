using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Communicates with the Medication API endpoint.
public class MedicationDataService : IMedicationDataService
{
    private readonly HttpClient _http;

    public MedicationDataService(HttpClient http)
    {
        _http = http;
    }

    // Posts a request to the API to mark a planned medication time as given.
    public async Task<Result> MarkAsGivenAsync(int residentSchemaId, int medicationTimeId)
    {
        try
        {
            var dto = new MarkMedicationGivenDto
            {
                ResidentSchemaId = residentSchemaId,
                MedicationTimeId = medicationTimeId
            };

            var response = await _http.PostAsJsonAsync("api/medication/mark-given", dto);

            return response.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure("Kunne ikke registrere medicin.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure("Ingen forbindelse til serveren.");
        }
    }
}
