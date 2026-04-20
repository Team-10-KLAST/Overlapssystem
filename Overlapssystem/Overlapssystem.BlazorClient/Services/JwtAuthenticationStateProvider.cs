using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Overlapssystem.BlazorClient.Services
{
    // Custom AuthenticationStateProvider that uses JWT tokens stored in sessionStorage to manage
    // authentication state in Blazor
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;

        public JwtAuthenticationStateProvider(IJSRuntime js)
        {
            _js = js;
        }

        // Called by Blazor to find out who is currently logged in
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _js.InvokeAsync<string?>("sessionStorage.getItem", "authToken");

            if (string.IsNullOrEmpty(token))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        // Call this after a successful login to update Blazor's authentication state
        public async Task NotifyUserLoggedIn(string token)
        {
            await _js.InvokeVoidAsync("sessionStorage.setItem", "authToken", token);

            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        // Call this when the user logs out to clear the authentication state
        public async Task NotifyUserLoggedOut()
        {
            await _js.InvokeVoidAsync("sessionStorage.removeItem", "authToken");

            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }

        // Extracts claims (user info) from the JWT token
        private IEnumerable<Claim> ParseClaimsFromJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.Claims;
        }
    }
}
