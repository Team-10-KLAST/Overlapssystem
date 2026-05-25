using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for fetching employee data from the API.
public interface IEmployeeDataService
{
    Task<Result<IEnumerable<EmployeeDto>>> GetEmployeesAsync();
}
