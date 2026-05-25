using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for fetching resident schemas for a specific shift from the API. 
public interface IResidentSchemaDataService
{
    Task<Result<IEnumerable<ResidentSchemaDto>>> GetByShiftAsync(int shiftId);
}
