using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs;

// DTO for updating the status text on a resident schema.
// The schema ID is provided via the URL — this DTO contains only the new status value.
public class UpdateStatusDto
{
    public string Status { get; set; } = string.Empty;
}
