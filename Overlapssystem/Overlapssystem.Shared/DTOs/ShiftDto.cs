using Overlapssystem.Domain.Enums;
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
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public ShiftType ShiftType { get; set; }
        public DepartmentType Department { get; set; }
    }
}
