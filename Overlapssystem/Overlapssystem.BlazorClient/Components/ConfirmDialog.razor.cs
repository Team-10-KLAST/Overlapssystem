using Microsoft.AspNetCore.Components;

namespace Overlapssystem.BlazorClient.Components;

// Code-behind for ConfirmDialog.razor.
// Exposes parameters for controlling visibility, messages and callbacks.
public partial class ConfirmDialog
{
    [Parameter] public bool IsVisible { get; set; }
    [Parameter] public string Message { get; set; } = string.Empty;
    [Parameter] public string SubMessage { get; set; } = string.Empty;
    [Parameter] public EventCallback OnConfirm { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
}
