using Microsoft.AspNetCore.Components;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Components;

// Code-behind for FixedMedication.razor.
// Handles checkbox interaction and confirmation flow for fixed medication times.
public partial class FixedMedication
{
    [Parameter] public List<MedicationTimeDto> MedicationTimes { get; set; } = new();
    [Parameter] public string? Initials { get; set; }
    [Parameter] public EventCallback<MedicationTimeDto> OnMedicationGiven { get; set; }

    private bool showConfirm = false;
    private MedicationTimeDto? pendingMed;

    // When a medication time is clicked, we set the pending medication and show the confirmation dialog.
    private void HandleMedicationClicked(MedicationTimeDto med)
    {
        if (med.IsGiven)
            return;
        pendingMed = med;
        showConfirm = true;
    }

    private async Task ConfirmChange()
    {
        await OnMedicationGiven.InvokeAsync(pendingMed);
        showConfirm = false;
        pendingMed = null;
    }

    private void CancelChange()
    {
        showConfirm = false;
        pendingMed = null;
    }
}
