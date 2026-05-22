namespace Overlapssystem.Application.Interfaces;

// Defines the contract for assigning employees to resident schemas
public interface IAssignEmployeeService
{
    Task<bool> AssignEmployeeAsync(int residentSchemaId, int employeeId, int position);
}
