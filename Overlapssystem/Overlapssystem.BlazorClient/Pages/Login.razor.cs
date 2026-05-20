using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Pages
{
    // Code-behind for the Login.razor component, which handles user login functionality
    public partial class Login
    {
        // Injected services
        [Inject] private IAuthService AuthService { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        // Holds the login form data bound to the input fields in Login.razor
        private LoginDto _loginDto = new();

        // Holds an error message to display if login fails
        private string? _errorMessage;

        // Controls whether the login button is disabled while the request is in progress
        private bool _isLoading = false;

        // Called when the user clicks the login button
        private async Task HandleLogin()
        {
            _errorMessage = null;
            _isLoading = true;

            var success = await AuthService.LoginAsync(_loginDto);

            if (success)
                Navigation.NavigateTo("/choose-shift");
            else
                _errorMessage = "Forkert email eller adgangskode.";

            _isLoading = false;
        }

        // Called when the user presses a key in the password field, to allow submitting the form with Enter
        private async Task HandleKeyDown(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
                await HandleLogin();
        }
    }
}
