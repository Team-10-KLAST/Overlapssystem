using Microsoft.AspNetCore.Components;

namespace Overlapssystem.BlazorClient.Components;

public partial class RelativeContact
{
    [Parameter] public string ContactNote { get; set; } = string.Empty;

    [Parameter] public EventCallback<string> OnContactSaved { get; set; }

    private string currentMessage = string.Empty;
    private string savedMessage = string.Empty;
    private bool isEditing = false;
    private bool hasSavedMessage = false;
    private bool HasChanges => currentMessage != savedMessage;

    private MarkupString SavedMessageHtml =>
        new(System.Net.WebUtility.HtmlEncode(currentMessage).Replace("\n", "<br>"));

    protected override void OnParametersSet()
    {
        if (!isEditing)
        {
            savedMessage = ContactNote;
            currentMessage = savedMessage;
            hasSavedMessage = !string.IsNullOrWhiteSpace(savedMessage);
        }
    }

    private void OnFocus() => isEditing = true;

    private void OnBlur()
    {
        if (!HasChanges)
            isEditing = false;
    }

    private void StartEditing() => isEditing = true;

    private async Task SaveMessage()
    {
        await OnContactSaved.InvokeAsync(currentMessage);
        savedMessage = currentMessage;
        hasSavedMessage = !string.IsNullOrWhiteSpace(currentMessage);
        isEditing = false;
    }

    private void Cancel()
    {
        currentMessage = savedMessage;
        isEditing = false;
    }
}
