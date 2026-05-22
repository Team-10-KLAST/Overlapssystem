using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

// Defines the contract for managing work phones and their employee assignments
public interface IPhoneService
{
    Task<IEnumerable<Phone>> GetAllPhonesAsync();
    Task<int> CreatePhoneAsync(string phoneNumber);
    Task UpdatePhoneAssignmentAsync(int id, int? employeeId, bool isChecked);
    Task DeletePhoneAsync(int id);
}
