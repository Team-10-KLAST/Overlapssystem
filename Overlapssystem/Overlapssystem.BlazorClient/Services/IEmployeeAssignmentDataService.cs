using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for assigning employees to resident schemas through the API.
public interface IEmployeeAssignmentDataService
{
    Task<Result> AssignEmployeeAsync(int residentSchemaId, int employeeId, int position);
}