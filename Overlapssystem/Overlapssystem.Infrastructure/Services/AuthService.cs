using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Infrastructure.Identity;

namespace Overlapssystem.Infrastructure.Services;

// Implementation of the authentication service, handling user login and registration
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
    public async Task<(bool Success, string? Token, string? Email, string? Role)> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return (false, null, null, null);

        var result = await _signInManager.CheckPasswordSignInAsync(
            user, password, lockoutOnFailure: true);

        if (!result.Succeeded)
            return (false, null, null, null);

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? "Employee";

        var token = _tokenService.GenerateToken(user.Id, user.Email!, role);

        return (true, token, user.Email, role);
    }

    // Registers a new user with the specified email, password, and employee ID
    public async Task<bool> RegisterAsync(string email, string password, int employeeId)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmployeeId = employeeId
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return false;

        await _userManager.AddToRoleAsync(user, "Employee");
        return true;
    }
}