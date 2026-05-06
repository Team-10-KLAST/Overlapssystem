using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Shared.DTOs;

// DTO for marking a planned medication as given for a specific resident schema and medication time.
public class MarkMedicationGivenDto
{
    public int ResidentSchemaId { get; set; }
    public int MedicationTimeId { get; set; }
}
