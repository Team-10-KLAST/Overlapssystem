using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;

namespace Overlapssystem.Application.UseCases;

// Orchestrates the creation of a shift and initialization of resident schemas.
// Encapsulates the business rule that a new shift always has resident schemas.
public class CreateShiftUseCase : ICreateShiftUseCase
{
    private readonly IShiftService _shiftService;
    private readonly IResidentSchemaService _residentSchemaService;

    public CreateShiftUseCase(IShiftService shiftService, IResidentSchemaService residentSchemaService)
    {
        _shiftService = shiftService;
        _residentSchemaService = residentSchemaService;
    }

    // Creates a shift and initializes resident schemas for all residents in the department.
    public async Task<Shift> ExecuteAsync(DateTime date, ShiftType shiftType, int departmentId)
    {
        var shift = await _shiftService.CreateShiftAsync(date, shiftType, departmentId);
        await _residentSchemaService.InitializeSchemaForShiftAsync(shift.Id, departmentId);
        return shift;
    }
}