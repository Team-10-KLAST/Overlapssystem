using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

public interface IResidentSchemaService
{
    // Returns all resident schemas, including their related Resident. 
    // Async because database calls should never block the calling threead. 
    Task<IEnumerable<ResidentSchema>> GetAllAsync();
}
