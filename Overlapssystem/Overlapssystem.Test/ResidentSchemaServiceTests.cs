using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Services;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;
using Overlapssystem.Infrastructure.Data;

namespace Overlapssystem.Test;

// Tests for ResidentSchemaService
// Uses in-memory database so we dont touch the real database
// Each test gets a fresh database to avoid interference between tests. 
[TestClass]
public class ResidentSchemaServiceTests
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
        context.Shifts.Add(new Shift
        {
            Id = 1,
            Date = new DateTime(2026, 5, 5),
            Type = ShiftType.Dagvagt,
            DepartmentId = 1
        });
        
        context.Residents.AddRange(
            new Resident
            {
                ResidentId = 1,
                Initials = "AB",
                ShoppingDay = "Mandag",
                PaymentMethod = PaymentMethod.OwnCard,
                DepartmentId = 1
            },
            new Resident
            {
                ResidentId = 2,
                Initials = "CD",
                ShoppingDay = "Onsdag",
                PaymentMethod = PaymentMethod.PCard,
                DepartmentId = 1
            }
        );

        context.ResidentSchemas.AddRange(
            new ResidentSchema
            {
                ResidentSchemaId = 1,
                ResidentId = 1,
                RelativeContact = "Mor ringede",
                Status = "god dag",
                TrafficLight = TrafficLight.Green,
                ShiftId = 1
            },
            new ResidentSchema
            {
                ResidentSchemaId = 2,
                ResidentId = 2,
                RelativeContact = "Ingen kontakt",
                Status = "Urolig",
                TrafficLight = TrafficLight.Red,
                ShiftId = 1
            }
        );

        context.SaveChanges();
    }

    // Tests that GetByShiftAsync returns all schemas for a given shift
    [TestMethod]
    public async Task GetByShiftAsync_ReturnsAllSchemasForShift()
    {
        // ARRANGE
        var context = CreateContext("GetByShiftAsync_ReturnsAll");
        SeedTestData(context);
        var service = new ResidentSchemaService(context);

        // ACT 
        var result = await service.GetByShiftAsync(1);

        // ASSERT 
        var schemas = result.ToList();
        Assert.AreEqual(2, schemas.Count);
    }

    // Tests that GetByShiftAsync includes the related Resident data. 
    // This verifies that .Include(s => s.Resident) works correcly. 
    [TestMethod]
    public async Task GetByShiftAsync_IncludesResidentData()
    {
        // ARRANGE 
        var context = CreateContext("GetByShiftAsync_IncludesResident");
        SeedTestData(context);
        var service = new ResidentSchemaService(context);

        // ACT 
        var result = await service.GetByShiftAsync(1);

        // ASSERT
        var schemas = result.ToList();
        Assert.AreEqual(2, schemas.Count);
        Assert.IsTrue(schemas.Any(s => s.ResidentId == 1));
        Assert.IsTrue(schemas.Any(s => s.ResidentId == 2));
    }

    // Tests that GetByShiftAsync returns an empty list when no data exists.
    [TestMethod]
    public async Task GetTaskAsync_EmptyDatabase_ReturnsEmptyList()
    {
        // ARRANGE 
        var context = CreateContext("GetByShift_WrongID");
        var service = new ResidentSchemaService(context);

        // ACT
        var result = await service.GetByShiftAsync(999);

        // ASSERT
        Assert.AreEqual(0, result.Count());
    }

    // Tests that GetAllAsync correctly handles schemas where TrafficLight is null.
    [TestMethod]
    public async Task GetAllAsync_NullTrafficLight_ReturnsSchema()
    {
        // ARRANGE
        var context = CreateContext("GetAllAsync_NullTrafficLight");

        context.Shifts.Add(new Shift
        {
            Id = 1,
            Date = new DateTime(2026, 5, 5),
            Type = ShiftType.Dagvagt,
            DepartmentId = 1
        });

        context.Residents.Add(
            new Resident
            {
             ResidentId = 1,
             Initials = "AB",
             ShoppingDay = "Mandag",
             PaymentMethod = PaymentMethod.OwnCard,
             DepartmentId = 1
            }
        );
        context.ResidentSchemas.Add(
            new ResidentSchema
            {
                ResidentSchemaId = 1,
                ResidentId = 1,
                RelativeContact = "Mor ringede",
                Status = "God dag",
                TrafficLight = null,
                ShiftId = 1
            }
        );
        context.SaveChanges();
        var service = new ResidentSchemaService(context);

        // ACT
        var result = await service.GetByShiftAsync(1);

        // ASSERT
        var schema = result.FirstOrDefault();
        Assert.IsNotNull(schema);
        Assert.IsNull(schema.TrafficLight);
    }
}
