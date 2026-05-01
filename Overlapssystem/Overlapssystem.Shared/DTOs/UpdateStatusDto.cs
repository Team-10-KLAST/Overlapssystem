using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs;

// DTO for updating a resudebt schemas status terxt 
// only containts new status  - chema ID comes from the URL. 
public class UpdateStatusDto
{
    public string Status { get; set; } = string.Empty;
}
