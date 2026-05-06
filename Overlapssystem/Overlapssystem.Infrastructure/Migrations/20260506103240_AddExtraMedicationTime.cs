using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Overlapssystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExtraMedicationTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MedicationTimes",
                columns: new[] { "MedicationTimeId", "PlannedTime", "ResidentId" },
                values: new object[] { 17, new TimeOnly(20, 0, 0), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MedicationTimes",
                keyColumn: "MedicationTimeId",
                keyValue: 17);
        }
    }
}
