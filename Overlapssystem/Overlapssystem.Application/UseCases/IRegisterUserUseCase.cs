using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapssystem.Application.UseCases;

// Use case interface for registering a new user, which includes creating an associated employee.
public interface IRegisterUserUseCase
{
    Task<bool> ExecuteAsync(string email, string password, string firstName, string lastName);
}
