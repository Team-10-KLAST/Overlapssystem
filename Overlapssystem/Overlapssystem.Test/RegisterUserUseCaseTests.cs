using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Overlapssystem.Application.Interfaces;
using Overlapssystem.Application.Models;
using Overlapssystem.Application.UseCases;

namespace Overlapssystem.Test;

// Unit tests for the RegisterUserUseCase, which handles user registration
// by creating an employee and an Identity user account.
// Uses Moq to mock the dependencies (IEmployeeService and IAuthService) and
// verify the behavior of the use case under different scenarios.
[TestClass]
public class RegisterUserUseCaseTests
{
    // Builds a RegisterUserUseCase instance with mocked dependencies for testing.
    private static RegisterUserUseCase Build(
        Mock<IEmployeeService> employeeServiceMock,
        Mock<IAuthService> authServiceMock)
    {
        return new RegisterUserUseCase(
            employeeServiceMock.Object,
            authServiceMock.Object);
    }

    // Tests the ExecuteAsync method of RegisterUserUseCase when both employee creation
    // and user registration succeed.
    [TestMethod]
    public async Task ExecuteAsync_BothServicesSucceed_ReturnsSuccess()
    {
        // ARRANGE
        var employeeServiceMock = new Mock<IEmployeeService>();
        var authServiceMock = new Mock<IAuthService>();

        employeeServiceMock
            .Setup(m => m.CreateEmployeeAsync("Anna", "Nielsen"))
            .ReturnsAsync(7);

        authServiceMock
            .Setup(m => m.RegisterAsync("anna@bosted.dk", "Kode1234!", 7))
            .ReturnsAsync(new RegisterResult(true, Enumerable.Empty<string>()));

        var useCase = Build(employeeServiceMock, authServiceMock);

        // ACT
        var result = await useCase.ExecuteAsync("anna@bosted.dk", "Kode1234!", "Anna", "Nielsen");

        // ASSERT
        Assert.IsTrue(result.Success);
        Assert.IsFalse(result.Errors.Any());
        authServiceMock.Verify(m => m.RegisterAsync("anna@bosted.dk", "Kode1234!", 7), Times.Once);
    }

    // Tests that if RegisterAsync fails after CreateEmployeeAsync succeeds,
    // the created employee is deleted by calling DeleteEmployeeAsync.
    [TestMethod]
    public async Task ExecuteAsync_RegisterAsyncFails_CallsDeleteEmployeeAsync()
    {
        // ARRANGE
        var employeeServiceMock = new Mock<IEmployeeService>();
        var authServiceMock = new Mock<IAuthService>();

        employeeServiceMock
            .Setup(m => m.CreateEmployeeAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(3);

        authServiceMock
            .Setup(m => m.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(new RegisterResult(false, new[] { "Email er allerede i brug." }));

        var useCase = Build(employeeServiceMock, authServiceMock);

        // ACT
        await useCase.ExecuteAsync("fejl@bosted.dk", "Kode1234!", "Eva", "Larsen");

        // ASSERT
        employeeServiceMock.Verify(m => m.DeleteEmployeeAsync(3), Times.Once);
    }

    // Tests that if RegisterAsync fails, the ExecuteAsync method returns a failure result
    // with the appropriate error messages.
    [TestMethod]
    public async Task ExecuteAsync_RegisterAsyncFails_ReturnsFailureWithErrors()
    {
        // ARRANGE
        var employeeServiceMock = new Mock<IEmployeeService>();
        var authServiceMock = new Mock<IAuthService>();

        var errors = new[] { "Email is already taken." };

        employeeServiceMock
            .Setup(m => m.CreateEmployeeAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(1);

        authServiceMock
            .Setup(m => m.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(new RegisterResult(false, errors));

        var useCase = Build(employeeServiceMock, authServiceMock);

        // ACT
        var result = await useCase.ExecuteAsync("fail@bosted.dk", "Password1!", "Eva", "Larsen");

        // ASSERT
        Assert.IsFalse(result.Success);
        CollectionAssert.Contains(result.Errors.ToList(), "Email is already taken.");
    }

    // Tests that if RegisterAsync succeeds, DeleteEmployeeAsync is never called,
    // ensuring that the employee is not deleted.
    [TestMethod]
    public async Task ExecuteAsync_RegisterAsyncSucceeds_DoesNotCallDeleteEmployeeAsync()
    {
        // ARRANGE
        var employeeServiceMock = new Mock<IEmployeeService>();
        var authServiceMock = new Mock<IAuthService>();

        employeeServiceMock
            .Setup(m => m.CreateEmployeeAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(1);

        authServiceMock
            .Setup(m => m.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(new RegisterResult(true, Enumerable.Empty<string>()));

        var useCase = Build(employeeServiceMock, authServiceMock);

        // ACT
        await useCase.ExecuteAsync("ok@bosted.dk", "Kode1234!", "Ole", "Olsen");

        // ASSERT
        employeeServiceMock.Verify(
            m => m.DeleteEmployeeAsync(It.IsAny<int>()),
            Times.Never);
    }
}
