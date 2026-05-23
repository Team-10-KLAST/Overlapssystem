using Microsoft.AspNetCore.Components;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Components;

// Code-behind for ResidentCard.razor
// Displays and manages all information related to a resident, including traffic light, status, medications,
// PN times, assigned employees, and relative contact info. ResidentCard is responsible for all API
// interactions related to the resident's data and holds the main state for the card.
public partial class ResidentCard
{
    [Inject] private ITrafficLightDataService TrafficLightService { get; set; } = default!;
    [Inject] private IResidentStatusDataService StatusService { get; set; } = default!;
    [Inject] private IMedicationDataService MedicationService { get; set; } = default!;
    [Inject] private IEmployeeDataService EmployeeDataService { get; set; } = default!;
    [Inject] private IPNDataService PNDataService { get; set; } = default!;
    [Inject] private IRelativeContactDataService RelativeContactService { get; set; } = default!;
    [Inject] private IEmployeeAssignmentDataService AssignmentService { get; set; } = default!;

    [Parameter] public ResidentSchemaDto Schema { get; set; } = default!;
    
    private List<EmployeeDto> allEmployees = new();
    private bool loadError = false;
    private string? _errorMessage;
    private List<PNDto> pnTimes = new();

    // Loads employees and PN times on first render.
    protected override async Task OnInitializedAsync()
    {
        var employeesTask = EmployeeDataService.GetEmployeesAsync();
        var pnsTask = PNDataService.GetPNsBySchemaAsync(Schema.ResidentSchemaId);

        var employeesResult = await employeesTask;
        if (employeesResult.IsSuccess)
            allEmployees = employeesResult.Data!.ToList();
        else
            loadError = true;

        var pnsResult = await pnsTask;
        if (pnsResult.IsSuccess)
            pnTimes = pnsResult.Data!.ToList();
        else
            loadError = true;
    }

    // Persists the new traffic light color via the API and updates the local schema on success.
    private async Task HandleTrafficLightChanged(string newColor)
    {
        var result = await TrafficLightService.UpdateTrafficLightAsync(Schema.ResidentSchemaId, newColor);

        if (result.IsSuccess)
            Schema.TrafficLight = newColor;

        else
            _errorMessage = result.Error;
    }

    // Persists the new status text via the API and updates the local schema on success.
    private async Task HandleStatusSaved(string newStatus)
    {
        var result = await StatusService.UpdateStatusAsync(Schema.ResidentSchemaId, newStatus);

        if (result.IsSuccess)
            Schema.Status = newStatus;
        else
            _errorMessage = result.Error;
    }

    // Persists the medication as given via the API and updates the local state on success.
    private async Task HandleMedicationGiven(MedicationTimeDto med)
    {
        var result = await MedicationService.MarkAsGivenAsync(Schema.ResidentSchemaId, med.MedicationTimeId);

        if (result.IsSuccess)
            med.IsGiven = true;
        else
            _errorMessage = result.Error;
    }

    // Registers a new PN time via the API and adds the saved entry to the local list.
    private async Task HandlePNAdded(PNDto dto)
    {
        var result = await PNDataService.RegisterPNAsync(dto);

        if (result.IsSuccess)
            pnTimes.Add(result.Data!);
        else
            _errorMessage = result.Error;
    }

    // Deletes a PN time via the API and removes it from the local list.
    private async Task HandlePNDeleted(PNDto dto)
    {
        var result = await PNDataService.DeletePNAsync(dto.PNId);

        if (result.IsSuccess)
            pnTimes.Remove(dto);
        else
            _errorMessage = result.Error;
    }

    // Saves the relative contact note via the API.
    private async Task HandleContactSaved(string contactNote)
    {
        var result = await RelativeContactService.UpdateAsync(Schema.ResidentSchemaId, contactNote);

        if (result.IsSuccess)
            Schema.RelativeContact = contactNote;
        else
            _errorMessage = result.Error;
    }

    // Assigns the selected employee via the API and updates the local schema on success.
    private async Task HandleEmployeeChanged((int position, int? employeeId) args)
    {
        if (args.employeeId.HasValue)
        {
            var result = await AssignmentService.AssignEmployeeAsync(
                Schema.ResidentSchemaId, args.employeeId.Value, args.position);

            if (result.IsSuccess)
            {
                if (args.position == 1) Schema.SelectedPrimaryId = args.employeeId;
                else Schema.SelectedSecondaryId = args.employeeId;
            }
            else
            {
                _errorMessage = result.Error;
            }
        }
        else
        {
            if (args.position == 1) Schema.SelectedPrimaryId = null;
            else Schema.SelectedSecondaryId = null;
        }
    }
}
