using Overlapssystem.BlazorClient.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

public interface IPhoneDataService
{
    
    Task<Result<IEnumerable<PhoneDto>>> GetPhonesAsync();

    Task<Result> UpdatePhoneAsync(PhoneDto phoneDto);
}
