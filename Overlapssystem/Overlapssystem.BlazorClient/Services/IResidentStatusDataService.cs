using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Contract for the Blazor-side service that updates resident status.
public interface IResidentStatusDataService
{
    // Updates the status text for a specific resident schema.
    Task<bool> UpdateStatusAsync(int residentSchemaId, string newStatus);
}