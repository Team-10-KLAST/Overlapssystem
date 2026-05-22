using Overlapssystem.Application.Models;

namespace Overlapssystem.Application.Interfaces;

// This interface defines the contract for a token service that generates JWT tokens for authenticated users.
public interface ITokenService
{
    string GenerateToken(TokenRequest tokenRequest);
}
