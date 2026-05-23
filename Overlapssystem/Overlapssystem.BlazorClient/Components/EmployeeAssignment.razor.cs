using Microsoft.AspNetCore.Components;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Components;

// Code-behind for EmployeeAssignment.razor.
// Manages primary and secondary employee selection for a resident schema entry.
public partial class EmployeeAssignment
{
    [Parameter] public int ResidentSchemaId { get; set; }
    [Parameter] public List<EmployeeDto> AllEmployees { get; set; } = new();
    [Parameter] public int? SelectedPrimaryId { get; set; }
    [Parameter] public int? SelectedSecondaryId { get; set; }
    [Parameter] public EventCallback<(int position, int? employeeId)> OnEmployeeChanged { get; set; }

    // Filters the employee list so that the employee selected in one dropdown
    // is excluded from the other, preventing duplicate assignments.
    private IEnumerable<EmployeeDto> GetAvailableEmployees(int position)
    {
        if (position == 1)
        {
            return AllEmployees.Where(e => e.EmployeeId != SelectedSecondaryId);
        }
        else
        {
            return AllEmployees.Where(e => e.EmployeeId != SelectedPrimaryId);
        }
    }

    // Handles the change event when an employee is selected from the dropdown.
    private async Task OnEmployeeSelected(ChangeEventArgs e, int position)
    {
        string? stringValue = e.Value?.ToString();

        if (int.TryParse(stringValue, out var employeeId))
        {
            if (position == 1) SelectedPrimaryId = employeeId;
            else SelectedSecondaryId = employeeId;

            await OnEmployeeChanged.InvokeAsync((position, employeeId));
        }
        else
        {
            if (position == 1) SelectedPrimaryId = null;
            else SelectedSecondaryId = null;

            await OnEmployeeChanged.InvokeAsync((position, null));
        }
    }
}
