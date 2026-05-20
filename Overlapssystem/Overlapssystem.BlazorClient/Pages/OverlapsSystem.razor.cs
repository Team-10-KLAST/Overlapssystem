using Microsoft.AspNetCore.Components;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Pages;

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

    protected override async Task OnInitializedAsync()
    {
        var shift = await ShiftService.GetByIdAsync(ShiftId);
        if (shift != null)
        {
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
        }
        schemas = await SchemaService.GetByShiftAsync(ShiftId);
    }

    private async Task HandleLogout()
    {
        await AuthService.LogoutAsync();
        Navigation.NavigateTo("/login");
    }
}