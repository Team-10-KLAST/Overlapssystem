using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Services;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;
using Overlapssystem.Infrastructure.Data;

namespace Overlapssystem.Test;

[TestClass]
public class TrafficLightServiceTests
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
        context.ResidentSchemas.AddRange(
            new ResidentSchema
            {
                ResidentSchemaId = 1,
                ResidentId = 1,
                RelativeContact = "Mor ringede",
                Status = "God dag",
                TrafficLight = TrafficLight.Green
            },
            new ResidentSchema
            {
                ResidentSchemaId = 2,
                ResidentId = 2,
                RelativeContact = "Ingen kontakt",
                Status = "Urolig",
                TrafficLight = TrafficLight.Green
            }
        );

        context.Employees.Add(new Employee
        {
            EmployeeId = 1,
            FirstName = "Test",
            LastName = "User"
        });

        context.SaveChanges();
    }

    [TestMethod]
    // Tests that UpdateTrafficLightAsync correctly updates the traffic light status.
    public async Task UpdateTrafficLightAsync_ValidId_UpdatesTrafficLight()
    {
        // ARRANGE
        var context = CreateContext("UpdateTrafficLight_ValidId");
        SeedTestData(context);
        var service = new TrafficLightService(context);

        // ACT
        var result = await service.UpdateTrafficLightAsync(1, "Red", 1);

        // ASSERT
        Assert.IsTrue(result);
        var schema = await context.ResidentSchemas.FindAsync(1);
        Assert.AreEqual(TrafficLight.Red, schema!.TrafficLight);

        var otherSchema = await context.ResidentSchemas.FindAsync(2);
        Assert.AreEqual(TrafficLight.Green, otherSchema!.TrafficLight);
    }

    [TestMethod]
    // Tests that UpdateTrafficLightAsync returns false when schema does not exist.
    public async Task UpdateTrafficLightAsync_InvalidId_ReturnsFalse()
    {
        // ARRANGE
        var context = CreateContext("UpdateTrafficLight_InvalidId");
        var service = new TrafficLightService(context);

        // ACT
        var result = await service.UpdateTrafficLightAsync(999, "Red", 1);

        // ASSERT
        Assert.IsFalse(result);
    }

    [TestMethod]
    // Tests that UpdateTrafficLightAsync can set the traffic light to null.
    public async Task UpdateTrafficLightAsync_NullValue_SetsTrafficLightToNull()
    {
        // ARRANGE
        var context = CreateContext("UpdateTrafficLight_NullValue");
        SeedTestData(context);
        var service = new TrafficLightService(context);

        // ACT
        var result = await service.UpdateTrafficLightAsync(1, null, 1);

        // ASSERT
        Assert.IsTrue(result);
        var schema = await context.ResidentSchemas.FindAsync(1);
        Assert.IsNull(schema!.TrafficLight);
    }
}
