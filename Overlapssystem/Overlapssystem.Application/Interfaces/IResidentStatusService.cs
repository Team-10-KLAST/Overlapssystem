using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Application.Interfaces;

// contract for updating a residents status text 
public interface IResidentStatusService
{
    Task<bool> UpdateStatusAsync(int residentSchemaId, string newStatus);
}
