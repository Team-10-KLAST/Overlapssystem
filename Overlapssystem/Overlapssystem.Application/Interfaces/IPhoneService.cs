using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Domain.Entities;

namespace Overlapssystem.Application.Interfaces;

public interface IPhoneService
{
    Task<IEnumerable<Phone>> GetAllPhonesAsync();
    Task<int> CreatePhoneAsync(string phoneNumber);
    Task UpdatePhoneAssignmentAsync(int id, int? employeeId, bool isChecked);
    Task DeletePhoneAsync(int id);
}
