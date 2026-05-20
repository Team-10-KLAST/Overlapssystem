using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Overlapssystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreMedicationTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 4,
                column: "PlannedTime",
                value: new TimeOnly(7, 0, 0));

            migrationBuilder.UpdateData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 5,
                column: "PlannedTime",
                value: new TimeOnly(8, 0, 0));

            migrationBuilder.UpdateData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 10,
                column: "PlannedTime",
                value: new TimeOnly(7, 0, 0));

            migrationBuilder.UpdateData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 15,
                column: "PlannedTime",
                value: new TimeOnly(9, 0, 0));

            migrationBuilder.UpdateData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 17,
                column: "PlannedTime",
                value: new TimeOnly(12, 0, 0));

            migrationBuilder.InsertData(
                table: "MedicationTimes",
                columns: new[] { "MedicationTimeId", "PlannedTime", "ResidentId" },
                values: new object[,]
                {
                    { 18, new TimeOnly(20, 0, 0), 1 },
                    { 19, new TimeOnly(18, 0, 0), 2 },
                    { 20, new TimeOnly(12, 0, 0), 4 },
                    { 21, new TimeOnly(17, 0, 0), 4 },
                    { 22, new TimeOnly(22, 0, 0), 4 },
                    { 23, new TimeOnly(14, 0, 0), 5 },
                    { 24, new TimeOnly(20, 0, 0), 5 },
                    { 25, new TimeOnly(19, 30, 0), 6 },
                    { 26, new TimeOnly(20, 0, 0), 7 },
                    { 27, new TimeOnly(13, 0, 0), 8 },
                    { 28, new TimeOnly(21, 0, 0), 8 },
                    { 29, new TimeOnly(21, 0, 0), 9 },
                    { 30, new TimeOnly(12, 0, 0), 10 },
                    { 31, new TimeOnly(17, 0, 0), 10 },
                    { 32, new TimeOnly(22, 0, 0), 10 },
                    { 33, new TimeOnly(20, 0, 0), 11 },
                    { 34, new TimeOnly(14, 0, 0), 12 },
                    { 35, new TimeOnly(21, 0, 0), 12 },
                    { 36, new TimeOnly(15, 0, 0), 14 },
                    { 37, new TimeOnly(22, 0, 0), 14 },
                    { 38, new TimeOnly(20, 0, 0), 15 },
                    { 39, new TimeOnly(13, 0, 0), 16 },
                    { 40, new TimeOnly(21, 0, 0), 16 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 40);

            migrationBuilder.UpdateData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 4,
                column: "PlannedTime",
                value: new TimeOnly(8, 0, 0));

            migrationBuilder.UpdateData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 5,
                column: "PlannedTime",
                value: new TimeOnly(10, 0, 0));

            migrationBuilder.UpdateData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 10,
                column: "PlannedTime",
                value: new TimeOnly(8, 0, 0));

            migrationBuilder.UpdateData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 15,
                column: "PlannedTime",
                value: new TimeOnly(20, 0, 0));

            migrationBuilder.UpdateData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 17,
                column: "PlannedTime",
                value: new TimeOnly(20, 0, 0));
        }
    }
}
