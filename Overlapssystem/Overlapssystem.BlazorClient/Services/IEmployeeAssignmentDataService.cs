using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public interface IEmployeeAssignmentDataService
{
    /// <summary>
    /// Sender en anmodning til API'en om at tildele en medarbejder til et skema.
    /// </summary>
    /// <param name="residentSchemaId">ID på det specifikke beboerskema.</param>
    /// <param name="employeeId">ID på den valgte medarbejder.</param>
    /// <param name="position">1 for primær, 2 for sekundær.</param>
    Task<bool> AssignEmployeeAsync(int residentSchemaId, int employeeId, int position);
}