using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Service interface for managing relative contact data. Defines methods for updating contact notes.
public interface IRelativeContactDataService
{
    Task<Result> UpdateAsync(int residentSchemaId, string contactNote);
}
