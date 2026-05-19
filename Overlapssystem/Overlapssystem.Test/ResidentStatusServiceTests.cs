using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Services;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;
using Overlapssystem.Infrastructure.Data;

namespace Overlapssystem.Test;

[TestClass]
public class ResidentStatusServiceTests
{
    // Creates a fresh in-memory database for each test.
    private AppDbContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        return new AppDbContext(options);
    }

    // Seeds a single resident schema with a known status.
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

    [TestMethod]
    // Tests that UpdateStatusAsync returns true when the schema exists.
    public async Task UpdateStatusAsync_SchemaExists_ReturnsTrue()
    {
        // ARRANGE
        var context = CreateContext("UpdateStatus_ReturnsTrue");
        SeedTestData(context);
        var service = new ResidentStatusService(context);

        // ACT
        var result = await service.UpdateStatusAsync(1, "Ny status", 1);

        // ASSERT
        Assert.IsTrue(result);
    }

    [TestMethod]
    // Tests that UpdateStatusAsync actually changes the status in the database.
    public async Task UpdateStatusAsync_SchemaExists_StatusIsUpdated()
    {
        // ARRANGE
        var context = CreateContext("UpdateStatus_StatusChanged");
        SeedTestData(context);
        var service = new ResidentStatusService(context);

        // ACT
        await service.UpdateStatusAsync(1, "Ny status", 1);

        // ASSERT - Read from database to verify the change was persisted.
        var schema = await context.ResidentSchemas.FindAsync(1);
        Assert.AreEqual("Ny status", schema!.Status);
    }

    [TestMethod]
    // Tests that UpdateStatusAsync returns false when schema doesn't exist.
    public async Task UpdateStatusAsync_SchemaNotFound_ReturnsFalse()
    {
        // ARRANGE
        var context = CreateContext("UpdateStatus_NotFound");
        SeedTestData(context);
        var service = new ResidentStatusService(context);

        // ACT - use an ID that doesn't exist
        var result = await service.UpdateStatusAsync(999, "Ny status", 1);

        // ASSERT
        Assert.IsFalse(result);
    }
}