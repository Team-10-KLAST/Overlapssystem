using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

// Defines the contract for PN medication registration operations.
public interface IPNService
{
    // Registers a new PN medication with a given time for a resident schema.
    Task<int> RegisterPNAsync(int residentSchemaId, TimeOnly givenTime, int employeeId);

    // Returns all PN registrations for a specific resident schema.
    Task<IEnumerable<PN>> GetPNsBySchemaAsync(int residentSchemaId);
}
