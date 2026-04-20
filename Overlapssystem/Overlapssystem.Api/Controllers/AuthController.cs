using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Infrastructure.Identity;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Controllers
{
    // This controller handles authentication-related endpoints such as login and registration.
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Dependencies injected via constructor
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmployeeCreationService _employeeCreationService;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager,
            IEmployeeCreationService employeeCreationService, IConfiguration configuration)
        {
            _userManager = userManager;
            _employeeCreationService = employeeCreationService;
            _configuration = configuration;
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

            var token = GenerateJwtToken(user, role);

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

        // Helper method to generate JWT token for authenticated users
        private string GenerateJwtToken(ApplicationUser user, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
