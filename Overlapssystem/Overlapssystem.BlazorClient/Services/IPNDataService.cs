using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Defines the contract for PN medication data operations in the frontend.
public interface IPNDataService
{
    // Fetches all PN registrations for a specific resident schema from the API.
    Task<Result<IEnumerable<PNDto>>> GetPNsBySchemaAsync(int residentSchemaId);

    // Sends a new PN registration to the API and returns the saved result.
    Task<Result<PNDto>> RegisterPNAsync(PNDto dto);

    // Sends a DELETE request to permanently remove a PN registration by ID.
    Task<Result> DeletePNAsync(int pnId);

    // Sends a PUT request to update the GivenTime of an existing PN registration.
    Task<Result> UpdatePNAsync(PNDto dto);
}
