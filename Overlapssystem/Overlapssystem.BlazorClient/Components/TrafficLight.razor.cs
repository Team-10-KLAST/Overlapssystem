using Microsoft.AspNetCore.Components;

namespace Overlapssystem.BlazorClient.Components;

// Code-behind for TrafficLight.razor.
// Displays the traffic light color for a resident and allows changing it with confirmation.
public partial class TrafficLight
{
    [Parameter] public string? CurrentColor { get; set; }
    [Parameter] public string? Initials { get; set; }
    [Parameter] public EventCallback<string> OnColorChanged { get; set; }

    private bool showConfirm = false;
    private string? pendingColor;

    private void HandleClick(string newColor)
    {
        if (CurrentColor != newColor)
        {
            pendingColor = newColor;
            showConfirm = true;
        }
    }

    private async Task ConfirmChange()
    {
        await OnColorChanged.InvokeAsync(pendingColor);
        showConfirm = false;
        pendingColor = null;
    }

    private void CancelChange()
    {
        showConfirm = false;
        pendingColor = null;
    }

    // This method translates the internal color values to user-friendly text.
    private string TranslateColor(string? color) => color switch
    {
        "Green" => "Grøn",
        "Yellow" => "Gul",
        "Red" => "Rød",
        null => "Ikke vurderet",
        _ => color
    };
}
