using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Application.Models;

namespace Overlapssystem.Application.Interfaces;

// Interface for authentication service, responsible for user login and registration
public interface IAuthService
{
    Task<LoginResult> LoginAsync(string email, string password);
    Task<RegisterResult> RegisterAsync(string email, string password, int employeeId);
}

