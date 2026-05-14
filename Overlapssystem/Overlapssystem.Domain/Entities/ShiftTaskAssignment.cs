using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Domain.Entities
{
    public class ShiftTaskAssignment
    {
        public int Id { get; set; }
        public int ShiftId { get; set; }
        public int DepartmentTaskId { get; set; }
        public string AssignedEmployee { get; set; } = string.Empty;

        public Shift Shift { get; set; } = null!;
        public DepartmentTask DepartmentTask { get; set; } = null!;
    }
}
