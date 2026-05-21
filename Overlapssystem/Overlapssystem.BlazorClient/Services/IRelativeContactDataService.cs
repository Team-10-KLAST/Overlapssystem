using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Defines the contract for relative contact data operations in the frontend.
public interface IRelativeContactDataService
{
    // Fetches all relative contact registrations for a specific resident schema.
    Task<Result<IEnumerable<RelativeContactDto>>> GetBySchemaAsync(int residentSchemaId);

    // Sends a new relative contact registration to the API and returns the saved result.
    Task<Result<RelativeContactDto>> CreateAsync(RelativeContactDto dto);

    // Sends an updated relative contact registration to the API.
    Task<Result> UpdateAsync(RelativeContactDto dto);

    // Deletes a relative contact registration by ID.
    Task<Result> DeleteAsync(int id);
}
