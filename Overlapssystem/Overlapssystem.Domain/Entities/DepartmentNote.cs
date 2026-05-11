using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Domain.Entities
{
    // Entity for huskeseddel - følger deparmentId
    public class DepartmentNote
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }

}
