using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Application.Interfaces
{
    // This interface defines the contract for a service that creates new employee records in the database.
    public interface IEmployeeCreationService
    {
        Task<int> CreateEmployeeAsync(string firstName, string lastName);
    }
}
