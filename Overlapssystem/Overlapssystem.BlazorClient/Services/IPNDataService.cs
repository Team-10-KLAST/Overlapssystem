using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Defines the contract for PN medication data operations in the frontend.
public interface IPNDataService
{
    // Fetches all PN registrations for a specific resident schema from the API.
    Task<IEnumerable<PNDto>> GetPNsBySchemaAsync(int residentSchemaId);

    // Sends a new PN registration to the API and returns the saved result.
    Task<PNDto> RegisterPNAsync(PNDto dto);
}
