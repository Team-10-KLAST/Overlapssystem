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
    public class EmployeeService : IEmployeeService
    {
        private readonly IAppDbContext _context;

        public EmployeeService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .OrderBy(e => e.FirstName)
                .ToListAsync();
        }
    }
}
