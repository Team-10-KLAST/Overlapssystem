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

namespace Overlapssystem.Test
{
    // Unit tests for MedicationService
    // Uses in-memory database so we dont touch the real database
    // Each test gets a fresh database to avoid interference between tests.
    [TestClass]
    public class MedicationServiceTests
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
            context.Departments.Add(new Department
            {
                DepartmentId = 1,
                DepartmentName = "Skoven"
            });

            context.Shifts.Add(new Shift
            {
                Id = 1,
                Date = new DateTime(2026, 4, 28),
                Type = ShiftType.Dagvagt,
                DepartmentId = 1
            });

            context.Residents.Add(new Resident
            {
                ResidentId = 1,
                Initials = "AB",
                ShoppingDay = "Mandag",
                PaymentMethod = PaymentMethod.OwnCard,
                DepartmentId = 1
            });

            context.MedicationTimes.Add(new MedicationTime
            {
                MedicationTimeId = 1,
                ResidentId = 1,
                PlannedTime = new TimeOnly(8, 0)
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

            context.Employees.Add(new Employee
            {
                EmployeeId = 1,
                FirstName = "Test",
                LastName = "User"
            });

            context.SaveChanges();
        }

        [TestMethod]
        // Tests that MarkAsGivenAsync creates a new MedicationRegistration in the database.
        public async Task MarkAsGivenAsync_CreatesRegistration()
        {
            // ARRANGE
            var context = CreateContext("MarkAsGiven_CreatesRegistration");
            SeedTestData(context);
            var service = new MedicationService(context);

            // ACT
            await service.MarkAsGivenAsync(1, 1, 1);

            // ASSERT
            var registration = context.MedicationRegistrations.FirstOrDefault();
            Assert.IsNotNull(registration);
            Assert.AreEqual(1, registration.ResidentSchemaId);
            Assert.AreEqual(1, registration.MedicationTimeId);
            Assert.IsTrue(registration.Given);
        }

        [TestMethod]
        // Tests that MarkAsGivenAsync sets GivenTime to a recent UTC timestamp.
        public async Task MarkAsGivenAsync_SetsGivenTimeToUtcNow()
        {
            // ARRANGE
            var context = CreateContext("MarkAsGiven_SetsGivenTime");
            SeedTestData(context);
            var service = new MedicationService(context);
            var beforeCall = DateTime.UtcNow;

            // ACT
            await service.MarkAsGivenAsync(1, 1, 1);

            // ASSERT
            var registration = context.MedicationRegistrations.FirstOrDefault();
            Assert.IsNotNull(registration);
            Assert.IsTrue(registration.GivenTime >= beforeCall);
            Assert.IsTrue(registration.GivenTime <= DateTime.UtcNow);
        }

        [TestMethod]
        // Tests that MarkAsGivenAsync creates separate registrations for multiple medication times.
        public async Task MarkAsGivenAsync_MultipleTimes_CreatesMultipleRegistrations()
        {
            // ARRANGE
            var context = CreateContext("MarkAsGiven_MultipleRegistrations");
            SeedTestData(context);
            context.MedicationTimes.Add(new MedicationTime
            {
                MedicationTimeId = 2,
                ResidentId = 1,
                PlannedTime = new TimeOnly(20, 0)
            });
            context.SaveChanges();
            var service = new MedicationService(context);

            // ACT
            await service.MarkAsGivenAsync(1, 1, 1);
            await service.MarkAsGivenAsync(1, 2, 1);

            // ASSERT
            var registrations = context.MedicationRegistrations.ToList();
            Assert.AreEqual(2, registrations.Count);
            Assert.IsTrue(registrations.Any(r => r.MedicationTimeId == 1));
            Assert.IsTrue(registrations.Any(r => r.MedicationTimeId == 2));
        }

        [TestMethod]
        // Tests that MarkAsGivenAsync only creates one registration per medication time per shift,
        // even if called multiple times with the same arguments.
        public async Task MarkAsGivenAsync_CalledTwice_DoesNotCreateDuplicateRegistration()
        {
            // ARRANGE
            var context = CreateContext("MarkAsGiven_NoDuplicates");
            SeedTestData(context);
            var service = new MedicationService(context);

            // ACT
            await service.MarkAsGivenAsync(1, 1, 1);
            await service.MarkAsGivenAsync(1, 1, 1);

            // ASSERT
            var registrations = context.MedicationRegistrations.ToList();
            Assert.AreEqual(1, registrations.Count);
        }
    }
}
