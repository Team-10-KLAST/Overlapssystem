using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Components;

// Code-behind for PNMedication.razor.
// Displays and manages the list of PN medication times for a resident schema.
public partial class PNMedication
{
    [Parameter] public ResidentSchemaDto Schema { get; set; } = default!;
    [Parameter] public List<PNDto> ExistingPNTimes { get; set; } = new();
    [Parameter] public EventCallback<PNDto> OnPNAdded { get; set; }
    [Parameter] public EventCallback<PNDto> OnPNDeleted { get; set; }

    private int EditingIndex { get; set; } = -1;
    private bool IsAddingNew { get; set; } = false;
    private PNDto? pendingDelete = null;
    private bool showDeleteConfirm = false;
    private string pendingTime = string.Empty;

    // Opens the inline edit input for the time at the given index.
    private void StartEdit(int index)
    {
        EditingIndex = index;
        IsAddingNew = false;
    }

    // Shows the new-time input field and clears any previously tracked time.
    private void StartAddNew()
    {
        IsAddingNew = true;
        EditingIndex = -1;
        pendingTime = string.Empty;
    }

    private void StartDelete(PNDto pn)
    {
        pendingDelete = pn;
        showDeleteConfirm = true;
    }

    private async Task ConfirmDelete()
    {
        if (pendingDelete == null)
            return;

        await OnPNDeleted.InvokeAsync(pendingDelete);
        showDeleteConfirm = false;
        pendingDelete = null;
    }

    private void CancelDelete()
    {
        showDeleteConfirm = false;
        pendingDelete = null;
    }

    // Formats a time string to HH:mm for compact display.
    private string FormatTime(string time)
    {
        if (time.Length >= 5)
            return time.Substring(0, 5);
        return time;
    }

    // Handles time change when editing an existing entry — updates the local display only.
    private void OnEditTimeChanged(ChangeEventArgs e, int index)
    {
        var time = e.Value?.ToString() ?? string.Empty;
        if (!string.IsNullOrEmpty(time))
            ExistingPNTimes[index].GivenTime = time;
        EditingIndex = -1;
    }

    // Handles time input when the user leaves the field — skipped if Enter already saved the entry.
    private async Task OnNewTimeChanged(ChangeEventArgs e)
    {
        if (!IsAddingNew)
            return;

        var time = e.Value?.ToString() ?? string.Empty;
        if (!string.IsNullOrEmpty(time))
        {
            var dto = new PNDto { ResidentSchemaId = Schema.ResidentSchemaId, GivenTime = time };
            await OnPNAdded.InvokeAsync(dto);
        }
        IsAddingNew = false;
        pendingTime = string.Empty;
    }

    // Closes the edit input on Enter or Escape.
    private void OnKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter" || e.Key == "Escape")
        {
            EditingIndex = -1;
            IsAddingNew = false;
        }
    }

    // Saves pendingTime on Enter, or discards on Escape. onchange handles blur without Enter.
    private async Task OnKeyDownNew(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            if (!string.IsNullOrEmpty(pendingTime))
            {
                var dto = new PNDto { ResidentSchemaId = Schema.ResidentSchemaId, GivenTime = pendingTime };
                await OnPNAdded.InvokeAsync(dto);
            }
            IsAddingNew = false;
            pendingTime = string.Empty;
        }
        else if (e.Key == "Escape")
        {
            IsAddingNew = false;
            pendingTime = string.Empty;
        }
    }
}
