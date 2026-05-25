using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Overlapssystem.BlazorClient.Services;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.BlazorClient.Pages;

// Code-behind for the Login.razor.
// Manages the login form, including handling user input, calling the authentication service,
// and navigating to the appropriate page upon successful login.
public partial class Login
{
    [Inject] private IAuthService AuthService { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;
    
    private LoginDto _loginDto = new();
    private string? _errorMessage;
    private bool _isLoading = false;

    // Handles the login process when the user submits the form. It calls the authentication service
    // and updates the UI based on the result.
    private async Task HandleLogin()
    {
        _errorMessage = null;
        _isLoading = true;

        try
        {
            var success = await AuthService.LoginAsync(_loginDto);

            if (success)
                Navigation.NavigateTo("/choose-shift");
            else
                _errorMessage = "Forkert email eller adgangskode. Prøv igen.";
        }
        catch (Exception)
        {
            _errorMessage = "Kunne ikke oprette forbindelse til serveren. Prøv igen senere.";
        }
        finally
        {
            _isLoading = false;
        }
    }

    // Called when the user presses a key in the password field, to allow submitting the form with Enter
    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
            await HandleLogin();
    }
}
