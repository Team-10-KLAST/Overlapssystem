using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Services;

// Handles CRUD operations for work phones and their employee assignments. 
public class PhoneService : IPhoneService
{
    private readonly IAppDbContext _context;

    public PhoneService(IAppDbContext context)
    {
        _context = context;
    }

    // Retrieves all phone entries from the database, including the assigned employee if one exists.
    public async Task<IEnumerable<Phone>> GetAllPhonesAsync()
    {
        return await _context.Phones
            .Include(p => p.Employee)
            .OrderBy(p => p.Number)
            .ToListAsync();
    }

    // Creates a new phone entry with the specified phone number and returns the generated Id.
    public async Task<int> CreatePhoneAsync(string phoneNumber)
    {
        var phone = new Phone
        {
            Number = phoneNumber,
            IsTested = false,
            EmployeeId = null
        };

        _context.Phones.Add(phone);
        await _context.SaveChangesAsync();
        return phone.Id;
    }

    // Updates the employee assignment and the checked status for a specific phone entry.
    public async Task UpdatePhoneAssignmentAsync(int id, int? employeeId, bool isTested)
    {
        var phone = await _context.Phones.FindAsync(id);
        if (phone != null)
        {
            phone.EmployeeId = employeeId;
            phone.IsTested = employeeId == null ? false : isTested;
            await _context.SaveChangesAsync();
        }
    }

    // Deletes a phone entry by its Id.
    public async Task DeletePhoneAsync(int id)
    {
        var phone = await _context.Phones.FindAsync(id);
        if (phone != null)
        {
            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();
        }
    }
}
