using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for updating resident status through the API.
public interface IResidentStatusDataService
{
    Task<Result> UpdateStatusAsync(int residentSchemaId, string newStatus);
}