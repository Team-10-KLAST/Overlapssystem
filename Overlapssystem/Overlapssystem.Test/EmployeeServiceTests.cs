using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Services;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Infrastructure.Data;

namespace Overlapssystem.Test;

// Tests for EmployeeService.
// Uses EF Core InMemory database.
// Each test gets a uniquely named database to avoid interference between tests.

[TestClass]
public class EmployeeServiceTests
{
    private AppDbContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        return new AppDbContext(options);
    }

    // Verifies that all employees are returned from the database.
    [TestMethod]
    public async Task GetAllEmployeesAsync_EmployeesExist_ReturnsAllEmployees()
    {
        // ARRANGE
        var context = CreateContext("GetAll_ReturnsAll");
        context.Employees.AddRange(
            new Employee { FirstName = "Anna", LastName = "Nielsen" },
            new Employee { FirstName = "Bo", LastName = "Hansen" }
        );
        await context.SaveChangesAsync();

        var service = new EmployeeService(context);

        // ACT
        var result = await service.GetAllEmployeesAsync();

        // ASSERT
        Assert.AreEqual(2, result.Count());
    }

    // Verifies that employees are returned ordered by first name.
    [TestMethod]
    public async Task GetAllEmployeesAsync_MultipleEmployees_ReturnsOrderedByFirstName()
    {
        // ARRANGE
        var context = CreateContext("GetAll_OrderedByFirstName");
        context.Employees.AddRange(
            new Employee { FirstName = "Zara", LastName = "Jensen" },
            new Employee { FirstName = "Anna", LastName = "Nielsen" },
            new Employee { FirstName = "Mads", LastName = "Hansen" }
        );
        await context.SaveChangesAsync();

        var service = new EmployeeService(context);

        // ACT
        var result = (await service.GetAllEmployeesAsync()).ToList();

        // ASSERT
        Assert.AreEqual("Anna", result[0].FirstName);
        Assert.AreEqual("Mads", result[1].FirstName);
        Assert.AreEqual("Zara", result[2].FirstName);
    }

    // Verifies that an empty list is returned when no employees exist.
    [TestMethod]
    public async Task GetAllEmployeesAsync_NoEmployees_ReturnsEmptyList()
    {
        // ARRANGE
        var context = CreateContext("GetAll_Empty");
        var service = new EmployeeService(context);

        // ACT
        var result = await service.GetAllEmployeesAsync();

        // ASSERT
        Assert.IsFalse(result.Any());
    }

    // Verifies that CreateEmployeeAsync persists a new employee in the database with correct names.
    [TestMethod]
    public async Task CreateEmployeeAsync_ValidNames_PersistsEmployeeInDatabase()
    {
        // ARRANGE
        var context = CreateContext("Create_PersistsEmployee");
        var service = new EmployeeService(context);

        // ACT
        await service.CreateEmployeeAsync("Line", "Pedersen");

        // ASSERT
        var employee = context.Employees.Single();
        Assert.AreEqual("Line", employee.FirstName);
        Assert.AreEqual("Pedersen", employee.LastName);
    }

    // Verifies that CreateEmployeeAsync returns the generated EmployeeId.
    // This ID is critical as it is forwarded to RegisterAsync in RegisterUserUseCase.
    [TestMethod]
    public async Task CreateEmployeeAsync_ValidNames_ReturnsGeneratedEmployeeId()
    {
        // ARRANGE
        var context = CreateContext("Create_ReturnsId");
        var service = new EmployeeService(context);

        // ACT
        var employeeId = await service.CreateEmployeeAsync("Line", "Pedersen");

        // ASSERT
        Assert.IsTrue(employeeId > 0);

        // Verify the returned ID matches what is in the database
        var employee = await context.Employees.FindAsync(employeeId);
        Assert.IsNotNull(employee);
    }

    // Verifies that an existing employee is removed from the database.
    [TestMethod]
    public async Task DeleteEmployeeAsync_EmployeeExists_RemovesEmployeeFromDatabase()
    {
        // ARRANGE
        var context = CreateContext("Delete_RemovesEmployee");
        var employee = new Employee { FirstName = "Søren", LastName = "Larsen" };
        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        var service = new EmployeeService(context);

        // ACT
        await service.DeleteEmployeeAsync(employee.EmployeeId);

        // ASSERT
        var deleted = await context.Employees.FindAsync(employee.EmployeeId);
        Assert.IsNull(deleted);
    }

    // Verifies that DeleteEmployeeAsync does not throw when the ID does not exist.
    // This is important for the cleanup logic in RegisterUserUseCase.
    [TestMethod]
    public async Task DeleteEmployeeAsync_EmployeeDoesNotExist_DoesNotThrow()
    {
        // ARRANGE
        var context = CreateContext("Delete_IdNotFound");
        var service = new EmployeeService(context);

        // ACT & ASSERT
        await service.DeleteEmployeeAsync(999);
        Assert.AreEqual(0, context.Employees.Count());
    }
}

