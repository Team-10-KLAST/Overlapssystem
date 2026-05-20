using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services
{
    public interface IPhoneDataService
    {
        
        Task<IEnumerable<PhoneDto>> GetPhonesAsync();

        Task<bool> UpdatePhoneAsync(PhoneDto phoneDto);
    }
}
