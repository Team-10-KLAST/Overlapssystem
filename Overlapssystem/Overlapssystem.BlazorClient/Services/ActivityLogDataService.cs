using System.Net.Http.Json;
using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Fetches activity log entries from the API for a given employee and date range.
public class ActivityLogDataService : IActivityLogDataService
{
    private readonly HttpClient _http;

    public ActivityLogDataService(HttpClient http)
    {
        _http = http;
    }

    // Retrieves log entries for the specified employee between the given dates.
    public async Task<Result<List<ActivityLogEntryDto>>> GetByEmployeeAsync(
        int employeeId, DateTime from, DateTime to)
    {
        try
        {
            var fromStr = from.ToString("yyyy-MM-dd");
            var toStr = to.AddDays(1).ToString("yyyy-MM-dd");

            var result = await _http.GetFromJsonAsync<List<ActivityLogEntryDto>>(
                $"api/activitylog?employeeId={employeeId}&from={fromStr}&to={toStr}");

            return Result<List<ActivityLogEntryDto>>.Success(result ?? new List<ActivityLogEntryDto>());
        }
        catch (HttpRequestException)
        {
            return Result<List<ActivityLogEntryDto>>.Failure("Kunne ikke hente aktivitetslog.");
        }
    }
}