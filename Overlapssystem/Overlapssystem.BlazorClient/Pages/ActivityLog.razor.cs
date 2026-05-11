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

    protected override async Task OnInitializedAsync()
    {
        employees = await EmployeeService.GetEmployeesAsync();
    }

    private void OnEmployeeChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var id))
            selectedEmployeeId = id;
        else
            selectedEmployeeId = null;
    }

    private async Task Search()
    {
        if (!selectedEmployeeId.HasValue) return;

        isLoading = true;
        logEntries = await LogService.GetByEmployeeAsync(
            selectedEmployeeId.Value, fromDate, toDate);
        isLoading = false;
    }
}