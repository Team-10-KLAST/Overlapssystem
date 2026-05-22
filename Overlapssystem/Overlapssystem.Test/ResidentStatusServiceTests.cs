using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Services;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;
using Overlapssystem.Infrastructure.Data;

namespace Overlapssystem.Test;

// Tests for ResidentStatusService 
// Uses in-memory database so we dont touch the real databas
// Each test gets a fresh database to avoid interference between tests.
[TestClass]
public class ResidentStatusServiceTests
{
    private AppDbContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        return new AppDbContext(options);
    }

    private void SeedTestData(AppDbContext context)
    {
        context.Residents.Add(new Resident
        {
            ResidentId = 1,
            Initials = "AB",
            ShoppingDay = "Mandag",
            PaymentMethod = PaymentMethod.OwnCard,
            DepartmentId = 1
        });

        context.ResidentSchemas.Add(new ResidentSchema
        {
            ResidentSchemaId = 1,
            ResidentId = 1,
            Status = "Gammel status",
            RelativeContact = "Mor ringede.",
            TrafficLight = TrafficLight.Green,
            ShiftId = 1
        });

        context.Employees.Add(new Employee
        {
            EmployeeId = 1,
            FirstName = "Test",
            LastName = "User"
        });

        context.SaveChanges();
    }

    // Tests that UpdateStatusAsync returns true when the schema exists.
    [TestMethod]
    public async Task UpdateStatusAsync_SchemaExists_ReturnsTrue()
    {
        // ARRANGE
        var context = CreateContext("UpdateStatus_ReturnsTrue");
        SeedTestData(context);
        var logService = new ActivityLogService(context);
        var service = new ResidentStatusService(context, logService);

        // ACT
        var result = await service.UpdateStatusAsync(1, "Ny status", 1);

        // ASSERT
        Assert.IsTrue(result);
    }

    // Tests that UpdateStatusAsync actually changes the status in the database.
    [TestMethod]
    public async Task UpdateStatusAsync_SchemaExists_StatusIsUpdated()
    {
        // ARRANGE
        var context = CreateContext("UpdateStatus_StatusChanged");
        SeedTestData(context);
        var logService = new ActivityLogService(context);
        var service = new ResidentStatusService(context, logService);

        // ACT
        await service.UpdateStatusAsync(1, "Ny status", 1);

        // ASSERT 
        var schema = await context.ResidentSchemas.FindAsync(1);
        Assert.AreEqual("Ny status", schema!.Status);
    }

    // Tests that UpdateStatusAsync returns false when schema doesn't exist.
    [TestMethod]
    public async Task UpdateStatusAsync_SchemaNotFound_ReturnsFalse()
    {
        // ARRANGE
        var context = CreateContext("UpdateStatus_NotFound");
        SeedTestData(context);
        var logService = new ActivityLogService(context);
        var service = new ResidentStatusService(context, logService);

        // ACT 
        var result = await service.UpdateStatusAsync(999, "Ny status", 1);

        // ASSERT
        Assert.IsFalse(result);
    }
}