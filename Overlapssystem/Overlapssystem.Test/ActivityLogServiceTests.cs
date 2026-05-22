using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Services;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;
using Overlapssystem.Infrastructure.Data;

namespace Overlapssystem.Test;

// Tests for ActivityLogService.
// Uses in-memory database so we dont touch the real database.
// Each test gets a fresh database to avoid interference between tests.
[TestClass]
public class ActivityLogServiceTests
{
    private AppDbContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        return new AppDbContext(options);
    }

    private void SeedBaseData(AppDbContext context)
    {
        context.Departments.Add(new Department
        {
            DepartmentId = 1,
            DepartmentName = "Skoven"
        });

        context.Employees.Add(new Employee
        {
            EmployeeId = 1,
            FirstName = "Anna",
            LastName = "Jensen"
        });

        context.Employees.Add(new Employee
        {
            EmployeeId = 2,
            FirstName = "Peter",
            LastName = "Hansen"
        });

        context.Residents.Add(new Resident
        {
            ResidentId = 1,
            Initials = "AB",
            ShoppingDay = "Mandag",
            PaymentMethod = PaymentMethod.OwnCard,
            DepartmentId = 1
        });

        context.Shifts.Add(new Shift
        {
            Id = 1,
            Date = new DateTime(2026, 5, 10),
            Type = ShiftType.Dagvagt,
            DepartmentId = 1
        });

        context.ResidentSchemas.Add(new ResidentSchema
        {
            ResidentSchemaId = 1,
            ResidentId = 1,
            RelativeContact = "Mor ringede",
            Status = "God dag",
            TrafficLight = TrafficLight.Green,
            ShiftId = 1
        });

        context.SaveChanges();
    }

    // Tests that AddLogEntry saves a new entry to the database.
    [TestMethod]
    public async Task LogActionAsync_SavesEntryToDatabase()
    {
        // ARRANGE
        var context = CreateContext("LogAction_SavesEntry");
        SeedBaseData(context);
        var service = new ActivityLogService(context);

        // ACT
        await service.AddLogEntry("Medicin", "Markerede medicin som givet kl 08:00", 1, 1);
        await context.SaveChangesAsync();
        
        // ASSERT
        var saved = context.ActivityLogEntries.FirstOrDefault();
        Assert.IsNotNull(saved);
        Assert.AreEqual("Medicin", saved.ActionType);
        Assert.AreEqual(1, saved.EmployeeId);
    }

    // Tests that GetByEmployeeAsync returns entries for the specified employee.
    [TestMethod]
    public async Task GetByEmployeeAsync_ReturnsEntriesForEmployee()
    {
        // ARRANGE
        var context = CreateContext("GetByEmployee_ReturnsEntries");
        SeedBaseData(context);

        context.ActivityLogEntries.Add(new ActivityLogEntry
        {
            ActivityLogEntryId = 1,
            ActionType = "Trafiklys",
            Description = "Ændrede trafiklys til Rød",
            Timestamp = new DateTime(2026, 5, 10, 10, 30, 0),
            EmployeeId = 1,
            ResidentSchemaId = 1
        });
        context.SaveChanges();

        var service = new ActivityLogService(context);

        // ACT
        var result = await service.GetByEmployeeAsync(
            1, new DateTime(2026, 5, 1), new DateTime(2026, 5, 11));

        // ASSERT
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Trafiklys", result[0].ActionType);
    }

    // Tests that GetByEmployeeAsync only returns entries within the date range.
    [TestMethod]
    public async Task GetByEmployeeAsync_FiltersOnDateRange()
    {
        // ARRANGE
        var context = CreateContext("GetByEmployee_FiltersDateRange");
        SeedBaseData(context);

        context.ActivityLogEntries.Add(new ActivityLogEntry
        {
            ActivityLogEntryId = 1,
            ActionType = "Status",
            Description = "Tilføjede til status: spiste frokost",
            Timestamp = new DateTime(2026, 5, 10, 12, 0, 0),
            EmployeeId = 1,
            ResidentSchemaId = 1
        });

        context.ActivityLogEntries.Add(new ActivityLogEntry
        {
            ActivityLogEntryId = 2,
            ActionType = "Status",
            Description = "Tilføjede til status: sov godt",
            Timestamp = new DateTime(2026, 4, 1, 8, 0, 0),
            EmployeeId = 1,
            ResidentSchemaId = 1
        });
        context.SaveChanges();

        var service = new ActivityLogService(context);

        // ACT
        var result = await service.GetByEmployeeAsync(
            1, new DateTime(2026, 5, 1), new DateTime(2026, 5, 11));

        // ASSERT
        Assert.AreEqual(1, result.Count);
    }

    // Tests that GetByEmployeeAsync only returns entries for the specified employee.
    [TestMethod]
    public async Task GetByEmployeeAsync_FiltersOnEmployeeId()
    {
        // ARRANGE
        var context = CreateContext("GetByEmployee_FiltersEmployeeId");
        SeedBaseData(context);

        context.ActivityLogEntries.Add(new ActivityLogEntry
        {
            ActivityLogEntryId = 1,
            ActionType = "PN",
            Description = "Markerede PN medicin kl. 14:30",
            Timestamp = new DateTime(2026, 5, 10, 14, 30, 0),
            EmployeeId = 1,
            ResidentSchemaId = 1
        });

        context.ActivityLogEntries.Add(new ActivityLogEntry
        {
            ActivityLogEntryId = 2,
            ActionType = "Medicin",
            Description = "Markerede medicin som givet kl. 09:00",
            Timestamp = new DateTime(2026, 5, 10, 9, 0, 0),
            EmployeeId = 2,
            ResidentSchemaId = 1
        });
        context.SaveChanges();

        var service = new ActivityLogService(context);

        // ACT
        var result = await service.GetByEmployeeAsync(
            1, new DateTime(2026, 5, 1), new DateTime(2026, 5, 11));

        // ASSERT
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(1, result[0].EmployeeId);
    }

    // Tests that GetByEmployeeAsync returns empty list when no entries match.
    [TestMethod]
    public async Task GetByEmployeeAsync_NoMatches_ReturnsEmptyList()
    {
        // ARRANGE
        var context = CreateContext("GetByEmployee_NoMatches");
        SeedBaseData(context);
        var service = new ActivityLogService(context);

        // ACT
        var result = await service.GetByEmployeeAsync(
            99, new DateTime(2026, 5, 1), new DateTime(2026, 5, 11));

        // ASSERT
        Assert.AreEqual(0, result.Count);
    }
}