using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Application.UseCases;
using Overlapssystem.Shared.DTOs;
using Overlapssystem.Api.Mappers;

namespace Overlapssystem.Api.Controllers;

// API controller responsible for authentication-related endpoints such as login and registration.
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IRegisterUserUseCase _registerUserUseCase;

    // Constructor — receives the authentication service and user registration use case via dependency injection.
    public AuthController(IAuthService authService, IRegisterUserUseCase registerUserUseCase)
    {
        _authService = authService;
        _registerUserUseCase = registerUserUseCase;
    }

    // POST api/auth/login
    // Authenticates a user and returns a JWT token if successful.
    // This endpoint is rate-limited to prevent brute-force attacks.
    [HttpPost("login")]
    [AllowAnonymous]
    [EnableRateLimiting("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto.Email, loginDto.Password);

        if (!result.Success)
            return Unauthorized("Forkert email eller adgangskode.");

        return Ok(AuthMapper.ToDto(result));
    }

    // POST api/auth/register
    // Registers a new user. Only accessible by users with the "Admin" role.
    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
    {
        var result = await _registerUserUseCase.ExecuteAsync(
            registerDto.Email,
            registerDto.Password,
            registerDto.FirstName,
            registerDto.LastName);

        if (!result.Success)
            return BadRequest(result.Errors);

        return Ok($"Bruger {registerDto.Email} oprettet.");
    }
}
