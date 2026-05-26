using Microsoft.AspNetCore.Components;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Pages;

// Code-behind for OverlapsSystem.razor.
// Loads shift details and resident schemas on init, and exposes logout.
public partial class OverlapsSystem
{
    [Inject] private IAuthService AuthService { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;
    [Inject] private IResidentSchemaDataService SchemaService { get; set; } = default!;
    [Inject] private IShiftService ShiftService { get; set; } = default!;

    [Parameter] public int ShiftId { get; set; }

    private IEnumerable<ResidentSchemaDto>? schemas;
    private int currentDepartment;
    private string departmentName = string.Empty;
    private string shiftDate = string.Empty;
    private string shiftType = string.Empty;
    private string? _errorMessage;

    // Loads the shift and its resident schemas.
    // Parses department, shift type and date for display in the top bar.
    protected override async Task OnInitializedAsync()
    {
        var shiftResult = await ShiftService.GetByIdAsync(ShiftId);

        if (!shiftResult.IsSuccess)
        {
            _errorMessage = shiftResult.Error;
            return;
        }

        var shift = shiftResult.Data!;
        int.TryParse(shift.DepartmentId, out currentDepartment);
        departmentName = currentDepartment switch
        {
            1 => "Skoven",
            2 => "Slottet",
            _ => "Ukendt afdeling"
        };
        shiftType = shift.ShiftType;
        if (DateTime.TryParse(shift.DateTime, out var parsedDate))
            shiftDate = parsedDate.ToString("dd-MM-yyyy");
        else
            shiftDate = shift.DateTime;

        var schemasResult = await SchemaService.GetByShiftAsync(ShiftId);
        if (schemasResult.IsSuccess)
            schemas = schemasResult.Data;
        else
            _errorMessage = schemasResult.Error;
    }

    private async Task HandleLogout()
    {
        await AuthService.LogoutAsync();
        Navigation.NavigateTo("/login");
    }
}