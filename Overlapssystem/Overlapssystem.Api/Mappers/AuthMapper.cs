using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Overlapssystem.Application.Models;
using Overlapssystem.Shared.DTOs;

namespace Overlapssystem.Api.Mappers;

public static class AuthMapper
{
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
