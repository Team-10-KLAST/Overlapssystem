using Overlapssystem.Application.Models;

namespace Overlapssystem.Application.UseCases;

// Use case interface for registering a new user, which includes creating an associated employee.
public interface IRegisterUserUseCase
{
    Task<RegisterResult> ExecuteAsync(string email, string password, string firstName, string lastName);
}
