using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Fetches resident schema data from the API.
public class ResidentSchemaDataService : IResidentSchemaDataService
{
    private readonly HttpClient _http;

    public ResidentSchemaDataService( HttpClient http)
    {
        _http = http;
    }

    // Fetches resident schemas for a specific shift.
    public async Task<Result<IEnumerable<ResidentSchemaDto>>> GetByShiftAsync(int shiftId)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<List<ResidentSchemaDto>>($"api/residentschema/shift/{shiftId}");
            return Result<IEnumerable<ResidentSchemaDto>>.Success(result ?? new List<ResidentSchemaDto>());
        }
        catch (HttpRequestException)
        {
            return Result<IEnumerable<ResidentSchemaDto>>.Failure("Kunne ikke hente borgerskemaer.");
        }
    }
}
