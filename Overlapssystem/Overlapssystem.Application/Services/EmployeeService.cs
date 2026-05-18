using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Overlapssystem.Application.Services
{
    // This service provides operations related to employee management, such as retrieving all employees,
    // creating a new employee, and deleting an employee.
    public class EmployeeService : IEmployeeService
    {
        private readonly IAppDbContext _context;

        public EmployeeService(IAppDbContext context)
        {
            _context = context;
        }

        // Retrieves all employees from the database, ordered by their first name.
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .OrderBy(e => e.FirstName)
                .ToListAsync();
        }

        // Creates a new employee and returns the generated EmployeeId
        public async Task<int> CreateEmployeeAsync(string firstName, string lastName)
        {
            var employee = new Employee { FirstName = firstName, LastName = lastName };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.EmployeeId;
        }

        // Deletes an employee by their EmployeeId.
        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
