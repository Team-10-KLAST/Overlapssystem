using Microsoft.AspNetCore.Components;

namespace Overlapssystem.BlazorClient.Components;

public partial class TrafficLight
{
    [Parameter] public string? CurrentColor { get; set; }
    [Parameter] public string? Initials { get; set; }

    // This allows the parent (ResidentCard) to respond to the change
    [Parameter] public EventCallback<string> OnColorChanged { get; set; }

    private bool showConfirm = false;
    private string? pendingColor;


    // Method to handle clicks on the traffic light. 
    // It sets the pending color and shows the confirmation dialog.
    private void HandleClick(string newColor)
    {
        if (CurrentColor != newColor)
        {
            pendingColor = newColor;
            showConfirm = true;
        }
    }

    // If the user confirms, we invoke the callback to notify the parent of the change.
    private async Task ConfirmChange()
    {
        await OnColorChanged.InvokeAsync(pendingColor);
        showConfirm = false;
        pendingColor = null;
    }

    // If the user cancels, we hide the confirmation dialog and clear the pending color.
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
