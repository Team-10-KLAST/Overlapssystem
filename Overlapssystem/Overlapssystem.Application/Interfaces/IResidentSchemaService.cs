using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

public interface IResidentSchemaService
{
    // Returns all resident schemas, including their related Resident. 
    Task<IEnumerable<ResidentSchema>> GetAllAsync();

    // Creates new ResidentSchemas for all resixents in a department when a new shift starts 
    Task<IEnumerable<ResidentSchema>> InitializeSchemaForShiftAsync(int shiftId, int departmentId);
}
