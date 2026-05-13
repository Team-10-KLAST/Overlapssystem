using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs
{
    /// <summary>
    /// DTO til overførsel af data ved tildeling af en medarbejder til et skema.
    /// </summary>
    public class AssignEmployeeDto
    {
        public int ResidentSchemaId { get; set; }
        public int EmployeeId { get; set; }

        // Bruges til at skelne mellem primær (1) og sekundær (2) position
        public int Position { get; set; }
    }
}
