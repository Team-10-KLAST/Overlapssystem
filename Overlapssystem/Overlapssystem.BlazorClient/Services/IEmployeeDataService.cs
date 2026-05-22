using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public interface IEmployeeDataService
{
    // Henter listen af medarbejdere fra API'en
    Task<Result<IEnumerable<EmployeeDto>>> GetEmployeesAsync();
}
