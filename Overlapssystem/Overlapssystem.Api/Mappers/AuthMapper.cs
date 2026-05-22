using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Overlapssystem.Application.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

// Mapper class responsible for converting authentication results into data transfer objects (DTOs) for API responses.
public static class AuthMapper
{
    // Maps a LoginResult object to an AuthResponseDto.
    public static AuthResponseDto ToDto(LoginResult result)
    {
        return new AuthResponseDto
        {
            Token = result.Token!,
            Email = result.Email!,
            Role = result.Role!
        };
    }
}
