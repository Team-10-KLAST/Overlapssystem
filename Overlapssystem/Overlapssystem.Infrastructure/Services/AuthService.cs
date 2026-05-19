using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Application.Models;
using Overlapssystem.Infrastructure.Identity;

namespace Overlapssystem.Infrastructure.Services;

// Implementation of the authentication service, handling user login and registration 
// using ASP.NET Core Identity for user management and a custom token service for JWT generation
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    // Validates user credentials and generates a JWT token if successful
    public async Task<LoginResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return new LoginResult(false, null, null, null);

        var result = await _signInManager.CheckPasswordSignInAsync(
            user, password, lockoutOnFailure: true);

        if (!result.Succeeded)
            return new LoginResult(false, null, null, null);

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? "Employee";

        var token = _tokenService.GenerateToken(new TokenRequest(
            UserId: user.Id,
            Email: user.Email!,
            Role: role,
            EmployeeId: user.EmployeeId?.ToString() ?? "0"
        ));

        return new LoginResult(true, token, user.Email!, role);
    }

    // Registers a new user with the specified email, password, and employee ID
    public async Task<RegisterResult> RegisterAsync(string email, string password, int employeeId)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmployeeId = employeeId
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return new RegisterResult(false, result.Errors.Select(error => error.Description));

        await _userManager.AddToRoleAsync(user, "Employee");
        return new RegisterResult(true, Enumerable.Empty<string>());
    }
}