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
                MedicinTime = new TimeOnly(8, 0),
                PaymentMethod = PaymentMethod.OwnCard,
                DepartmentId = 1
            },
            new Resident
            {
                ResidentId = 2,
                Initials = "CD",
                ShoppingDay = "Onsdag",
                MedicinTime = new TimeOnly(10, 0),
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
                TrafficLight = TrafficLight.Green
            },
            new ResidentSchema
            {
                ResidentSchemaId = 2,
                ResidentId = 2,
                RelativeContact = "Ingen kontakt",
                Status = "Urolig",
                TrafficLight = TrafficLight.Red
            }
        );

        context.SaveChanges();
    }

    [TestMethod]
    // Tests that GetAllAsync returns all schemas from database
    // This verifies basic "happy path" - data exists and is returned. 
    public async Task GetAllAsync_ReturnsAllSchemas()
    {
        // ARRANGE - Set up test database and seed data. 
        var context = CreateContext("GetAllAsync_ReturnsAll");
        SeedTestData(context);
        var service = new ResidentSchemaService(context);

        // ACT - Call GetAllAsync
        var result = await service.GetAllAsync();

        // ASSERT - verify the result. 
        var schemas = result.ToList();
        Assert.AreEqual(2, schemas.Count);
    }

    [TestMethod]
    // Tests that GetAllAsync includes the related Resident data. 
    // This verifies that .Include(s => s.Resident) works correcly. 
    public async Task GetAllAsync_IncludesResidentData()
    {
        // ARRANGE 
        var context = CreateContext("GetAllAsync_IncludesResident");
        SeedTestData(context);
        var service = new ResidentSchemaService(context);

        // ACT 
        var result = await service.GetAllAsync();

        // ASSERT - Check that the correct ResidentIds are present.
        var schemas = result.ToList();
        Assert.AreEqual(2, schemas.Count);
        Assert.IsTrue(schemas.Any(s => s.ResidentId == 1));
        Assert.IsTrue(schemas.Any(s => s.ResidentId == 2));
    }

    [TestMethod]
    //Tests that GetAllAsync returns an empty list when no data exists.
    // This verifies that the service handles "no data " gracefully instead of throwing exception. 
    public async Task GetTaskAsync_EmptyDatabase_ReturnsEmptyList()
    {
        // ARRANGE - create context without seeding any data. 
        var context = CreateContext("GetAllAsync_Empty");
        var service = new ResidentSchemaService(context);

        // ACT
        var result = await service.GetAllAsync();

        // ASSERT
        Assert.AreEqual(0, result.Count());
    }
}
