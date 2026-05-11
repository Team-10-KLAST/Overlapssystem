using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overlapssystem.Application.Interfaces;

namespace Overlapssystem.Application.UseCases;

// Orchestrates the creation of an employee and their associated Identity user account.
// Encapsulates the business rule that a new user always has a corresponding employee entity.
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IEmployeeCreationService _employeeCreationService;
    private readonly IAuthService _authService;

    public RegisterUserUseCase(
        IEmployeeCreationService employeeCreationService,
        IAuthService authService)
    {
        _employeeCreationService = employeeCreationService;
        _authService = authService;
    }

    // Creates an employee entity first, then creates the Identity user with the employee reference.
    public async Task<bool> ExecuteAsync(string email, string password, string firstName, string lastName)
    {
        var employeeId = await _employeeCreationService.CreateEmployeeAsync(firstName, lastName);
        return await _authService.RegisterAsync(email, password, employeeId);
    }
}