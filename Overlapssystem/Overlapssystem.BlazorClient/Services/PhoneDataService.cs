using Overlapssystem.Shared.DTOs;
using System.Net.Http.Json;

namespace Overlapssystem.BlazorClient.Services
{
    public class PhoneDataService : IPhoneDataService
    {
        private readonly HttpClient _httpClient;

        public PhoneDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<IEnumerable<PhoneDto>> GetPhonesAsync()
        {
            try
            {
                // Foretager et GET kald til api/phones
                var phones = await _httpClient.GetFromJsonAsync<IEnumerable<PhoneDto>>("api/phones");

                return phones ?? Enumerable.Empty<PhoneDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved hentning af telefoner: {ex.Message}");
                return Enumerable.Empty<PhoneDto>();
            }
        }

        public async Task<bool> UpdatePhoneAsync(PhoneDto phoneDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/phones/{phoneDto.Id}", phoneDto);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved opdatering af telefon (Id: {phoneDto.Id}): {ex.Message}");
                return false;
            }
        }
    }
}
