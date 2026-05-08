using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Application.Interfaces
{
    public interface IAssignEmployeeService
    {
        /// Tildeler en medarbejder til et beboerskema på en specifik position.
        /// <param name="residentSchemaId">ID på det aktuelle skema.</param>
        /// <param name="employeeId">ID på den medarbejder der skal tildeles.</param>
        /// <param name="position">1 for primær, 2 for sekundær.</param>
        /// <returns>True hvis opdateringen lykkedes.</returns>
        Task<bool> AssignEmployeeAsync(int residentSchemaId, int employeeId, int position);
    }
}
