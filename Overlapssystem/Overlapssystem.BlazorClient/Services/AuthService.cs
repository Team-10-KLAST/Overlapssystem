using System.Net.Http.Json;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Services;

// Handles communication with the API's authentication endpoints.
// This service is responsible for sending login requests to the API and managing the authentication state.
public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly JwtAuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient httpClient, JwtAuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
    }

    // Sends login credentials to the API and stores the returned token if successful.
    public async Task<bool> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);
            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            if (result == null)
                return false;

            await _authStateProvider.NotifyUserLoggedIn(result.Token);
            return true;
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }

    // Clears the token and notifies Blazor that the user is logged out
    public async Task LogoutAsync()
    {
        await _authStateProvider.NotifyUserLoggedOut();
    }
}
