using Microsoft.AspNetCore.Components;

namespace Overlapssystem.BlazorClient.Components;

// Code-behind for StatusField.razor.
// Handles editing and saving of a resident's status text.
public partial class StatusField
{
    [Parameter] public string Status { get; set; } = string.Empty;
    [Parameter] public int ResidentSchemaId { get; set; }
    [Parameter] public string? Initials { get; set; }
    [Parameter] public EventCallback<string> OnStatusSaved { get; set; }

    private string currentStatus = string.Empty;
    private bool showConfirm = false;

    // True if the user has made changes to the status that haven't been saved yet.
    private bool HasChanges => currentStatus != Status;

    // Initialize local copy when component first loads.
    protected override void OnParametersSet()
    {
        if (string.IsNullOrEmpty(currentStatus) && !string.IsNullOrEmpty(Status))
        {
            currentStatus = Status;
        }
    }

    private void ShowConfirmDialog()
    {
        showConfirm = true;
    }

    private void CancelSave()
    {
        showConfirm = false;
    }

    private async Task SaveStatus()
    {
        showConfirm = false;
        await OnStatusSaved.InvokeAsync(currentStatus);
    }
}