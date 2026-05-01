using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

public interface IResidentSchemaService
{
    // Returns all resident schemas for a specific shift
    Task<IEnumerable<ResidentSchema>> GetByShiftAsync(int shiftId);

    // Creates new ResidentSchemas for all resixents in a department when a new shift starts 
    Task<IEnumerable<ResidentSchema>> InitializeSchemaForShiftAsync(int shiftId, int departmentId);
}
