using Microsoft.JSInterop;

namespace Overlapssystem.BlazorClient.Services
{
    // A DelegatingHandler that automatically attaches the JWT token to every HTTP request
    // sent to the API, so the user does not have to manually include it each time
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IJSRuntime _js;

        public AuthorizationMessageHandler(IJSRuntime js)
        {
            _js = js;
        }

        // Intercepts every HTTP request and adds the Authorization header if a token exists
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _js.InvokeAsync<string?>("sessionStorage.getItem", "authToken");

            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
