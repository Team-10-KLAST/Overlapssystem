using Microsoft.EntityFrameworkCore;
using Overlapssystem.Application.Services;
using Overlapssystem.Domain.Entities;
using Overlapssystem.Domain.Enums;
using Overlapssystem.Infrastructure.Data;

namespace Overlapssystem.Test
{
    // Unit tests for ActivityLogService.
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

        private Employee SeedBaseData(AppDbContext context)
        {
            var department = new Department
            {
                DepartmentId = 1,
                DepartmentName = "Skoven"
            };
            context.Departments.Add(department);

            var employee = new Employee
            {
                EmployeeId = 1,
                FirstName = "Anna",
                LastName = "Jensen"
            };
            context.Employees.Add(employee);

            var resident = new Resident
            {
                ResidentId = 1,
                Initials = "AB",
                ShoppingDay = "Mandag",
                PaymentMethod = PaymentMethod.OwnCard,
                DepartmentId = 1
            };
            context.Residents.Add(resident);

            context.Shifts.Add(new Shift
            {
                Id = 1,
                Date = new DateTime(2026, 5, 10),
                Type = ShiftType.Dagvagt,
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

            context.SaveChanges();
            return employee;
        }

        [TestMethod]
        // Tests that GetMedicationLogAsync returns medication registrations
        // for the specified employee within the date range.
        public async Task GetMedicationLogAsync_ReturnsMedicationEntries()
        {
            // ARRANGE
            var context = CreateContext("MedLog_ReturnsMedEntries");
            var employee = SeedBaseData(context);

            context.MedicationRegistrations.Add(new MedicationRegistration
            {
                MedicationRegistrationId = 1,
                ResidentSchemaId = 1,
                MedicationTimeId = 1,
                Given = true,
                GivenTime = new DateTime(2026, 5, 10, 8, 15, 0),
                CreatedAt = new DateTime(2026, 5, 10, 8, 15, 0),
                ModifiedByEmployeeId = 1
            });
            context.SaveChanges();

            var service = new ActivityLogService(context);

            // ACT
            var result = await service.GetMedicationLogAsync(
                1, new DateTime(2026, 5, 1), new DateTime(2026, 5, 11));

            // ASSERT
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result[0].Given);
            Assert.AreEqual(1, result[0].ModifiedByEmployeeId);
        }

        [TestMethod]
        // Tests that GetResidentSchemaLogAsync returns schema changes
        // for the specified employee within the date range.
        public async Task GetResidentSchemaLogAsync_ReturnsSchemaEntries()
        {
            // ARRANGE
            var context = CreateContext("SchemaLog_ReturnsSchemaEntries");
            var employee = SeedBaseData(context);

            var schema = context.ResidentSchemas.First();
            schema.ModifiedByEmployeeId = 1;
            schema.UpdatedAt = new DateTime(2026, 5, 10, 10, 30, 0);
            context.SaveChanges();

            var service = new ActivityLogService(context);

            // ACT
            var result = await service.GetResidentSchemaLogAsync(
                1, new DateTime(2026, 5, 1), new DateTime(2026, 5, 11));

            // ASSERT
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].ModifiedByEmployeeId);
        }

        [TestMethod]
        // Tests that GetMedicationLogAsync only returns entries within the specified date range.
        public async Task GetMedicationLogAsync_FiltersOnDateRange()
        {
            // ARRANGE
            var context = CreateContext("MedLog_FiltersDateRange");
            var employee = SeedBaseData(context);

            // Entry WITHIN range
            context.MedicationRegistrations.Add(new MedicationRegistration
            {
                MedicationRegistrationId = 1,
                ResidentSchemaId = 1,
                MedicationTimeId = 1,
                Given = true,
                GivenTime = new DateTime(2026, 5, 10, 8, 0, 0),
                CreatedAt = new DateTime(2026, 5, 10, 8, 0, 0),
                ModifiedByEmployeeId = 1
            });

            // Entry OUTSIDE range
            context.MedicationRegistrations.Add(new MedicationRegistration
            {
                MedicationRegistrationId = 2,
                ResidentSchemaId = 1,
                MedicationTimeId = 1,
                Given = true,
                GivenTime = new DateTime(2026, 4, 1, 8, 0, 0),
                CreatedAt = new DateTime(2026, 4, 1, 8, 0, 0),
                ModifiedByEmployeeId = 1
            });
            context.SaveChanges();

            var service = new ActivityLogService(context);

            // ACT
            var result = await service.GetMedicationLogAsync(
                1, new DateTime(2026, 5, 1), new DateTime(2026, 5, 11));

            // ASSERT
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        // Tests that GetMedicationLogAsync only returns entries for the specified employee.
        public async Task GetMedicationLogAsync_FiltersOnEmployeeId()
        {
            // ARRANGE
            var context = CreateContext("MedLog_FiltersEmployeeId");
            var employee = SeedBaseData(context);

            // Second employee
            context.Employees.Add(new Employee
            {
                EmployeeId = 2,
                FirstName = "Peter",
                LastName = "Hansen"
            });

            // Entry for employee 1
            context.MedicationRegistrations.Add(new MedicationRegistration
            {
                MedicationRegistrationId = 1,
                ResidentSchemaId = 1,
                MedicationTimeId = 1,
                Given = true,
                GivenTime = new DateTime(2026, 5, 10, 8, 0, 0),
                CreatedAt = new DateTime(2026, 5, 10, 8, 0, 0),
                ModifiedByEmployeeId = 1
            });

            // Entry for employee 2
            context.MedicationRegistrations.Add(new MedicationRegistration
            {
                MedicationRegistrationId = 2,
                ResidentSchemaId = 1,
                MedicationTimeId = 1,
                Given = true,
                GivenTime = new DateTime(2026, 5, 10, 9, 0, 0),
                CreatedAt = new DateTime(2026, 5, 10, 9, 0, 0),
                ModifiedByEmployeeId = 2
            });
            context.SaveChanges();

            var service = new ActivityLogService(context);

            // ACT
            var result = await service.GetMedicationLogAsync(
                1, new DateTime(2026, 5, 1), new DateTime(2026, 5, 11));

            // ASSERT
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].ModifiedByEmployeeId);
        }

        [TestMethod]
        // Tests that GetMedicationLogAsync returns an empty list
        // when no entries match the employee and date range.
        public async Task GetMedicationLogAsync_NoMatches_ReturnsEmptyList()
        {
            // ARRANGE
            var context = CreateContext("MedLog_NoMatches");
            SeedBaseData(context);
            var service = new ActivityLogService(context);

            // ACT
            var result = await service.GetMedicationLogAsync(
                99, new DateTime(2026, 5, 1), new DateTime(2026, 5, 11));

            // ASSERT
            Assert.AreEqual(0, result.Count);
        }
    }
}