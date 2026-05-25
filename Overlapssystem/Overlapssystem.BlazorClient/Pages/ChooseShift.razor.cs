using System.Globalization;
using Microsoft.AspNetCore.Components;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Pages;

// Code-behind for ChooseShift.razor.
// Creates a new shift from the selected date, type and department,
// then navigates to the overlap page for that shift.
public partial class ChooseShift
{
    [Inject] private ISaveShiftService SaveShiftService { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;

    // Prevents selecting a future date in the date input.
    private string MaxDate => DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

    private CreateShiftDto model = new()
    {
        Date = DateTime.Today
    };

    private string message = string.Empty;

    private async Task SaveShift()
    {
        var result = await SaveShiftService.SaveShiftAsync(model);

        if (result.IsSuccess)
            Navigation.NavigateTo($"/overlapssystem/{result.Data}");
        else
            message = result.Error!;
    }
}
