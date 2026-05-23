using Microsoft.AspNetCore.Components;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Components;

// Code-behind for NoteSection.razor.
// Loads and auto-saves a department-wide note whenever the textarea content changes..
public partial class NoteSection
{
    [Inject] private IDepartmentNoteDataService NoteService { get; set; } = default!;
    [Parameter] public int DepartmentId { get; set; }

    private string Content = string.Empty;
    private bool isSaving = false;
    private int _loadedDepartmentId = 0;
    private string? _errorMessage;

    // Loads the note for the current department when DepartmentId changes.
    protected override async Task OnParametersSetAsync()
    {
        if (DepartmentId > 0 && DepartmentId != _loadedDepartmentId)
        {
            _loadedDepartmentId = DepartmentId;
            var result = await NoteService.GetNoteByDepartmentIdAsync(DepartmentId);
            if (result.IsSuccess)
                Content = result.Data!.FirstOrDefault()?.Content ?? string.Empty;
            else
                _errorMessage = result.Error;
        }
    }
    
    // Saves the current note content for the department.
    private async Task SaveInternal()
    {
        isSaving = true;
        var result = await NoteService.UpdateNoteAsync(new DepartmentNoteDto
        {
            DepartmentId = DepartmentId,
            Content = Content ?? ""
        });

        if (!result.IsSuccess)
            _errorMessage = result.Error;

        await Task.Delay(500);
        isSaving = false;
        StateHasChanged();
    }
}
