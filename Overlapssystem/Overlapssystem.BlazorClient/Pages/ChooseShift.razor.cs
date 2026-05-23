using System.Globalization;
using Microsoft.AspNetCore.Components;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Pages;

public partial class ChooseShift
{
    [Inject] private ISaveShiftService SaveShiftService { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;

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
