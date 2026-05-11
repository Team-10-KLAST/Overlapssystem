using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Application.Interfaces;

// This interface defines the contract for a token service that generates JWT tokens for authenticated users.
public interface ITokenService
{
    string GenerateToken(string userId, string email, string role);
}
