using Microsoft.AspNetCore.Components;

namespace Overlapssystem.BlazorClient.Components;

public partial class ConfirmDialog
{
    [Parameter] public bool IsVisible { get; set; }
    [Parameter] public string Message { get; set; } = string.Empty;
    [Parameter] public string SubMessage { get; set; } = string.Empty;
    [Parameter] public EventCallback OnConfirm { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
}
