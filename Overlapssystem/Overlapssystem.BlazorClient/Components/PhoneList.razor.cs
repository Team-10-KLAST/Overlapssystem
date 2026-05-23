using Microsoft.AspNetCore.Components;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Components;

// Code-behind for PhoneList.razor.
// Displays a list of phones for the current shift, allowing assignment to employees and marking as tested.
public partial class PhoneList
{
    [Inject] private IPhoneDataService PhoneService { get; set; } = default!;
    [Inject] private IEmployeeDataService EmployeeService { get; set; } = default!;
    [Parameter] public int ShiftId { get; set; }
    [Parameter] public List<EmployeeDto> AllEmployees { get; set; } = new();

    private List<PhoneDto>? phones;
    private string? _errorMessage;

    // Loads the list of phones and all employees when ShiftId changes.
    protected override async Task OnParametersSetAsync()
    {
        if (ShiftId > 0)
        {
            var errors = new List<string>();

            var phonesResult = await PhoneService.GetPhonesAsync();
            if (phonesResult.IsSuccess)
                phones = phonesResult.Data!.OrderBy(p => p.Number).ToList();
            else
                errors.Add(phonesResult.Error!);

            var employeesResult = await EmployeeService.GetEmployeesAsync();
            if (employeesResult.IsSuccess)
                AllEmployees = employeesResult.Data!.ToList();
            else
                errors.Add(employeesResult.Error!);

            _errorMessage = errors.Any() ? string.Join(" | ", errors) : null;
        }
    }

    // Called when the user changes the assigned employee for a phone.
    private async Task OnEmployeeChanged(ChangeEventArgs e, PhoneDto phone)
    {
        var previousEmployeeId = phone.EmployeeId;

        if (int.TryParse(e.Value?.ToString(), out var employeeId))
            phone.EmployeeId = employeeId;
        else
        {
            phone.EmployeeId = null;
            phone.IsTested = false;
        }

        StateHasChanged();

        var result = await PhoneService.UpdatePhoneAsync(phone);
        if (!result.IsSuccess)
        {
            phone.EmployeeId = previousEmployeeId;
            _errorMessage = result.Error;
            StateHasChanged();
        }
    }

    // Called when the user toggles the "tested" status of a phone.
    private async Task OnTestChanged(ChangeEventArgs e, PhoneDto phone)
    {
        if (phone.EmployeeId == null) return;

        if (e.Value is bool isTested)
        {
            phone.IsTested = isTested;
            var result = await PhoneService.UpdatePhoneAsync(phone);
            if (!result.IsSuccess)
            {
                phone.IsTested = !isTested;
                _errorMessage = result.Error;
            }
        }
    }

    // Resets all phones by unassigning them from employees and marking them as not tested.
    private async Task ResetAllPhones()
    {
        if (phones == null || !phones.Any()) return;

        foreach (var phone in phones)
        {
            if (phone.EmployeeId != null || phone.IsTested)
            {
                phone.EmployeeId = null;
                phone.IsTested = false;
                await PhoneService.UpdatePhoneAsync(phone);
            }
        }
        StateHasChanged();
    }
}
