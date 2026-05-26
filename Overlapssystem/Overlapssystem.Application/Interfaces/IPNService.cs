using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

// Defines the contract for PN medication registration operations.
public interface IPNService
{
    Task<int> RegisterPNAsync(int residentSchemaId, TimeOnly givenTime, int employeeId);

    Task<IEnumerable<PN>> GetPNsBySchemaAsync(int residentSchemaId);

    Task<bool> DeletePNAsync(int pnId, int employeeId);

    Task<bool> UpdatePNAsync(int pnId, TimeOnly givenTime);
}
