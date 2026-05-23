using Microsoft.AspNetCore.Components;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Components;

public partial class NoteSection
{
    [Inject] private IDepartmentNoteDataService NoteService { get; set; } = default!;

    [Parameter] public int DepartmentId { get; set; }
    private string Content = string.Empty;
    private bool isSaving = false;
    private int _loadedDepartmentId = 0;
    private string? _errorMessage;

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
