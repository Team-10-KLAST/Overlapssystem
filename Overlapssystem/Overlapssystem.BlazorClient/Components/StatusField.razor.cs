using Microsoft.AspNetCore.Components;

namespace Overlapssystem.BlazorClient.Components;

// Code-behind for StatusField.razor.
// Handles editing and saving of a resident's status text.
public partial class StatusField
{
    // The current status text, passed in from ResidentCard.
    [Parameter]
    public string Status { get; set; } = string.Empty;

    // The ResidentSchema ID, needed to identify which schema to update.
    [Parameter]
    public int ResidentSchemaId { get; set; }

    // Event callback to notify ResidentCard when status is saved.
    [Parameter]
    public EventCallback<string> OnStatusSaved { get; set; }

    // Local copy of the status text for editing.
    private string currentStatus = string.Empty;

    // Controls confirm dialog visibility.
    private bool showConfirm = false;

    // True if the user has changed the text.
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