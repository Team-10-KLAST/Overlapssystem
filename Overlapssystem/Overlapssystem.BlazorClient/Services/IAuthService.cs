using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Interface for the authentication service, defining methods for logging in and out.
public interface IAuthService
{
    Task<bool> LoginAsync(LoginDto loginDto);
    Task LogoutAsync();
}

