using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces
{
    // This interface defines the contract for a service that manages employee-related operations,
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<int> CreateEmployeeAsync(string firstName, string lastName);
        Task DeleteEmployeeAsync(int employeeId);
    }
}
