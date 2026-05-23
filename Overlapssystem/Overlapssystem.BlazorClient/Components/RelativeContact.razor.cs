using Microsoft.AspNetCore.Components;

namespace Overlapssystem.BlazorClient.Components;

// Code-behind for RelativeContact.razor.
// Displays a note about a resident's relative contact, allowing editing and saving of the note.
public partial class RelativeContact
{
    [Parameter] public string ContactNote { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> OnContactSaved { get; set; }

    private string currentMessage = string.Empty;
    private string savedMessage = string.Empty;
    private bool isEditing = false;
    private bool hasSavedMessage = false;

    // True if the user has made changes to the message that haven't been saved yet.
    private bool HasChanges => currentMessage != savedMessage;

    // Returns the current message formatted as HTML for display when not editing.
    private MarkupString SavedMessageHtml =>
        new(System.Net.WebUtility.HtmlEncode(currentMessage).Replace("\n", "<br>"));

    // Initializes the current message from the ContactNote parameter when it changes,
    // but only if not currently editing.
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
