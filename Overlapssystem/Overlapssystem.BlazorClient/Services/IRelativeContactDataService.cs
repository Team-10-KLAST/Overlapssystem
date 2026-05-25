using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for managing relative contact data.
public interface IRelativeContactDataService
{
    Task<Result> UpdateAsync(int residentSchemaId, string contactNote);
}
