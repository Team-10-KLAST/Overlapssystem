using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs
{
    //dto til at repræsentere en vagt
    public class ShiftDto
    {
        public string Id { get; set; } = string.Empty;
        public string DateTime { get; set; } = string.Empty;
        public string ShiftType { get; set; } = string.Empty;
        public string DepartmentId { get; set; } = string.Empty;
    }
}
