using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public interface IEmployeeDataService
{
    // Henter listen af medarbejdere fra API'en
    Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
}
