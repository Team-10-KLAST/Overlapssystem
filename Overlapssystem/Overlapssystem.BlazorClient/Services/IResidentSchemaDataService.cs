using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Contract for the Blazor-side service that fetches resident schemas. 
public interface IResidentSchemaDataService
{
    // Fetches all resident schemas from the API 
    Task<Result<IEnumerable<ResidentSchemaDto>>> GetByShiftAsync(int shiftId);

}
