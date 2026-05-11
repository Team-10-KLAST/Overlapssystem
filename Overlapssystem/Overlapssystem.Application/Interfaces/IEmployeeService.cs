using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces
{
    public interface IEmployeeService
    {
        // Henter alle medarbejdere fra databasen som domæne-entiteter
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    }
}
