using Overlapssystem.Shared.DTOs;
using System.Net.Http.Json;

namespace Overlapssystem.BlazorClient.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly HttpClient _httpClient;

        public EmployeeDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Henter alle medarbejdere fra API'en og returnerer dem som DTOs.
        /// </summary>
        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            try
            {
                // Foretager et GET kald til api/employees
                var employees = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>("api/employees");

                return employees ?? Enumerable.Empty<EmployeeDto>();
            }
            catch (Exception ex)
            {
                // Her kan du logge fejlen eller håndtere den efter behov
                Console.WriteLine($"Fejl ved hentning af medarbejdere: {ex.Message}");
                return Enumerable.Empty<EmployeeDto>();
            }
        }
    }
}
