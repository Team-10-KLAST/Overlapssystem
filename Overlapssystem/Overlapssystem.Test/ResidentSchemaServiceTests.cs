using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Overlapssystem.Infrastructure.Data;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;
using Overlapssystem.Application.Services;
using System.Runtime.CompilerServices;

namespace Overlapssystem.Test;

// Unit tests for ResidentSchemaService
// Uses in-memory database so we dont touch the real database
// Each test gets a fresh database to avoid interference between tests. 

[TestClass]
public class ResidentSchemaServiceTests
{

    // Helper method that creates a fresh in-memory DbContext for each test. 
    // A unique database per test ensures tests dont share data. 
    private AppDbContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        return new AppDbContext(options);
    }

    // Helper method that seeds data into the in-memory database. 
    private void SeedTestData(AppDbContext context)
    {
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

    [TestMethod]
    // Tests that GetByShiftAsync returns all schemas from database
    // This verifies basic "happy path" - data exists and is returned. 
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

    [TestMethod]
    // Tests that GetByShiftAsync includes the related Resident data. 
    // This verifies that .Include(s => s.Resident) works correcly. 
    public async Task GetByShiftAsync_IncludesResidentData()
    {
        // ARRANGE 
        var context = CreateContext("GetByShiftAsync_IncludesResident");
        SeedTestData(context);
        var service = new ResidentSchemaService(context);

        // ACT 
        var result = await service.GetByShiftAsync(1);

        // ASSERT - Check that the correct ResidentIds are present.
        var schemas = result.ToList();
        Assert.AreEqual(2, schemas.Count);
        Assert.IsTrue(schemas.Any(s => s.ResidentId == 1));
        Assert.IsTrue(schemas.Any(s => s.ResidentId == 2));
    }

    [TestMethod]
    //Tests that GetByShiftAsync returns an empty list when no data exists.
    // This verifies that the service handles "no data " gracefully instead of throwing exception. 
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

    [TestMethod]
    // Tests that GetAllAsync correctly handles schemas where TrafficLight is null.
    public async Task GetAllAsync_NullTrafficLight_ReturnsSchema()
    {
        // ARRANGE
        var context = CreateContext("GetAllAsync_NullTrafficLight");
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
