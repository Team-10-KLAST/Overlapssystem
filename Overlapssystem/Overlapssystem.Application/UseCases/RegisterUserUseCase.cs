using Overlapssystem.Application.Interfaces;
using Overlapssystem.Application.Models;

namespace Overlapssystem.Application.UseCases;

// Orchestrates the creation of an employee and their associated Identity user account.
// Encapsulates the business rule that a new user always has a corresponding employee entity.
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IEmployeeService _employeeService;
    private readonly IAuthService _authService;

    public RegisterUserUseCase(
        IEmployeeService employeeService,
        IAuthService authService)
    {
        _employeeService = employeeService;
        _authService = authService;
    }

    // Creates an employee and registers an Identity user. Rolls back the employee if registration fails.
    public async Task<RegisterResult> ExecuteAsync(string email, string password, string firstName, string lastName)
    {
        var employeeId = await _employeeService.CreateEmployeeAsync(firstName, lastName);

        var result = await _authService.RegisterAsync(email, password, employeeId);

        if (!result.Success)
            await _employeeService.DeleteEmployeeAsync(employeeId);

        return result;
    }
}