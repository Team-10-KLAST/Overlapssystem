using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Application.Interfaces
{
    public interface IEmployeeCreationService
    {
        Task<int> CreateEmployeeAsync(string firstName, string lastName);
    }
}
