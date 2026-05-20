using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Domain.Entities;

public class DepartmentTask
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public Department Department { get; set; } = null!;
}