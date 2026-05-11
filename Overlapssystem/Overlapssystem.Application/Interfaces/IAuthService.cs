using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Application.Interfaces;

// Interface for authentication service, responsible for user login and registration
public interface IAuthService
{
    Task<(bool Success, string? Token, string? Email, string? Role)> LoginAsync(string email, string password);
    Task<bool> RegisterAsync(string email, string password, int employeeId);
}

