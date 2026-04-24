using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services
{
    public class EmployeeCreationService : IEmployeeCreationService
    {
        private readonly IAppDbContext _context;

        public EmployeeCreationService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateEmployeeAsync(string firstName, string lastName)
        {
            var employee = new Employee { FirstName = firstName, LastName = lastName };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.EmployeeId;
        }
    }
}
