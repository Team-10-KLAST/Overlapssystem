using Microsoft.AspNetCore.Components;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Components;

public partial class EmployeeAssignment
{
    [Parameter] public int ResidentSchemaId { get; set; }
    [Parameter] public List<EmployeeDto> AllEmployees { get; set; } = new();

    // Disse bindes til data fra ResidentSchema
    [Parameter] public int? SelectedPrimaryId { get; set; }
    [Parameter] public int? SelectedSecondaryId { get; set; }

    [Parameter] public EventCallback<(int position, int? employeeId)> OnEmployeeChanged { get; set; }

    // Filtrerer listen så den valgte medarbejder ikke vises i den anden dropdown
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
