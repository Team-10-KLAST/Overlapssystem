using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

public interface IResidentSchemaService
{
    // Returns all resident schemas, including their related Resident. 
    Task<IEnumerable<ResidentSchema>> GetAllAsync();
}
