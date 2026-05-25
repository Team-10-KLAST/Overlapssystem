using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Pages;

// Code-behind for the ActivityLog.razor page.
// Admin-only page that shows a log of actions performed by a selected employee.
public partial class ActivityLog
{
    [Inject] private IActivityLogDataService LogService { get; set; } = default!;
    [Inject] private IEmployeeDataService EmployeeService { get; set; } = default!;

    private IEnumerable<EmployeeDto>? employees;
    private List<ActivityLogEntryDto>? logEntries;
    private int? selectedEmployeeId;
    private DateTime fromDate = DateTime.Today.AddDays(-7);
    private DateTime toDate = DateTime.Today;
    private bool isLoading;
    private bool CanSearch => selectedEmployeeId.HasValue;
    private string? _errorMessage;

    // Load the list of employees when the component initializes.
    protected override async Task OnInitializedAsync()
    {
        var result = await EmployeeService.GetEmployeesAsync();

        if (result.IsSuccess)
            employees = result.Data;
        else
            _errorMessage = result.Error;
    }

    // Update the selected employee ID when the user changes the selection.
    private void OnEmployeeChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var id))
            selectedEmployeeId = id;
        else
            selectedEmployeeId = null;
    }

    // Fetches the activity log entries for the selected employee and date range.
    private async Task Search()
    {
        if (!selectedEmployeeId.HasValue) return;

        if (toDate < fromDate)
        {
            _errorMessage = "Slutdato må ikke være tidligere end startdato.";
            return;
        }

        _errorMessage = null;
        isLoading = true;

        var result = await LogService.GetByEmployeeAsync(
            selectedEmployeeId.Value, fromDate, toDate);

        if (result.IsSuccess)
            logEntries = result.Data;
        else
            _errorMessage = result.Error;

        isLoading = false;
    }
}