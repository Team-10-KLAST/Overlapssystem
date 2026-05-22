using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

// Defines the contract for managing employees
public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<int> CreateEmployeeAsync(string firstName, string lastName);
    Task DeleteEmployeeAsync(int employeeId);
}
