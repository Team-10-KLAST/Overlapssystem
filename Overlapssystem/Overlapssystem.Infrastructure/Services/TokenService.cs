using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Application.Models;

namespace Overlapssystem.Infrastructure.Services;

// Service responsible for generating JWT tokens for authenticated users
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    // Constructor — receives the application configuration via dependency injection to access JWT settings.
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Generates a JWT token containing the user's ID, email, role, and employee ID as claims
    public string GenerateToken(TokenRequest tokenRequest)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));

        var expiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"]
            ?? throw new InvalidOperationException("JwtSettings:ExpiryMinutes er ikke konfigureret."));

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, tokenRequest.UserId),
            new Claim(ClaimTypes.Email, tokenRequest.Email),
            new Claim(ClaimTypes.Role, tokenRequest.Role),
            new Claim("EmployeeId", tokenRequest.EmployeeId)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
