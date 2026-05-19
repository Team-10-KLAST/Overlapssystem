using System.Net.Http.Json;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public class ActivityLogDataService : IActivityLogDataService
{
    private readonly HttpClient _http;

    public ActivityLogDataService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ActivityLogEntryDto>> GetByEmployeeAsync(
        int employeeId, DateTime from, DateTime to)
    {
        var fromStr = from.ToString("yyyy-MM-dd");
        var toStr = to.ToString("yyyy-MM-dd");

        return await _http.GetFromJsonAsync<List<ActivityLogEntryDto>>(
            $"api/activitylog?employeeId={employeeId}&from={fromStr}&to={toStr}")
            ?? new List<ActivityLogEntryDto>();
    }
}