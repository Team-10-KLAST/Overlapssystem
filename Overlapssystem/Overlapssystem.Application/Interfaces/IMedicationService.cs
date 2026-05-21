using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Application.Interfaces;

// Interface for the MedicationService, that defines the contract for marking a
// planned medication time as given for a specific resident schema.
public interface IMedicationService
{
    Task MarkAsGivenAsync(int residentSchemaId, int medicationTimeId, int employeeId);
}
