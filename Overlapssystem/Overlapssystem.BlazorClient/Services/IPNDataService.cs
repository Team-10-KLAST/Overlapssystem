using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for managing PN data through the API.
public interface IPNDataService
{
    Task<Result<IEnumerable<PNDto>>> GetPNsBySchemaAsync(int residentSchemaId);
    Task<Result<PNDto>> RegisterPNAsync(PNDto dto);
    Task<Result> DeletePNAsync(int pnId);

    // Sends a PUT request to update the GivenTime of an existing PN registration.
    Task<Result> UpdatePNAsync(PNDto dto);
}
