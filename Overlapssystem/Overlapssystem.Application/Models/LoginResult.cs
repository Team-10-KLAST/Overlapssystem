namespace Overlapssystem.Application.Models;

// This record represents the result of a login attempt, including whether it was successful,
// the generated token, and user information.
public record LoginResult(
    bool Success,
    string? Token,
    string? Email,
    string? Role);

