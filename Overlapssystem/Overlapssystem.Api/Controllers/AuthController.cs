using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Infrastructure.Identity;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers;

// This controller handles authentication-related endpoints such as login and registration.
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // Dependencies injected via constructor
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmployeeCreationService _employeeCreationService;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<ApplicationUser> userManager,
        IEmployeeCreationService employeeCreationService, ITokenService tokenService)
    {
        _userManager = userManager;
        _employeeCreationService = employeeCreationService;
        _tokenService = tokenService;
    }

    // Login-endpoint
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            return Unauthorized("Forkert email eller adgangskode.");

        var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!passwordValid)
            return Unauthorized("Forkert email eller adgangskode.");

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? "Employee";

        var token = _tokenService.GenerateToken(user.Id, user.Email!, role);

        return Ok(new AuthResponseDto
        {
            Token = token,
            Email = user.Email!,
            Role = role
        });
    }

    // Register new user endpoint - only for admins
    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
    {
        var employeeId = await _employeeCreationService.CreateEmployeeAsync(
            registerDto.FirstName, registerDto.LastName);

        var user = new ApplicationUser
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            EmployeeId = employeeId
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(errors);
        }

        await _userManager.AddToRoleAsync(user, "Employee");
        return Ok($"Bruger {registerDto.Email} oprettet.");
    }
}
