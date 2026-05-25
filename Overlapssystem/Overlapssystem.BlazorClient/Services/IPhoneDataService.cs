using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for fetching and updating phone data through the API.
public interface IPhoneDataService
{   
    Task<Result<IEnumerable<PhoneDto>>> GetPhonesAsync();
    Task<Result> UpdatePhoneAsync(PhoneDto phoneDto);
}
