using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Overlapssystem.Infrastructure.Migrations;

/// <inheritdoc />
public partial class MedicationTimeFeature : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "MedicinTime",
            table: "Residents");

        migrationBuilder.CreateTable(
            name: "MedicationTimes",
            columns: table => new
            {
                MedicationTimeId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ResidentId = table.Column<int>(type: "int", nullable: false),
                PlannedTime = table.Column<TimeOnly>(type: "time", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MedicationTimes", x => x.MedicationTimeId);
                table.ForeignKey(
                    name: "FK_MedicationTimes_Residents_ResidentId",
                    column: x => x.ResidentId,
                    principalTable: "Residents",
                    principalColumn: "ResidentId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "MedicationRegistrations",
            columns: table => new
            {
                MedicationRegistrationId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                GivenTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                Given = table.Column<bool>(type: "bit", nullable: false),
                ResidentSchemaId = table.Column<int>(type: "int", nullable: false),
                MedicationTimeId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MedicationRegistrations", x => x.MedicationRegistrationId);
                table.ForeignKey(
                    name: "FK_MedicationRegistrations_MedicationTimes_MedicationTimeId",
                    column: x => x.MedicationTimeId,
                    principalTable: "MedicationTimes",
                    principalColumn: "MedicationTimeId");
                table.ForeignKey(
                    name: "FK_MedicationRegistrations_ResidentSchemas_ResidentSchemaId",
                    column: x => x.ResidentSchemaId,
                    principalTable: "ResidentSchemas",
                    principalColumn: "ResidentSchemaId");
            });

        migrationBuilder.InsertData(
            table: "MedicationTimes",
            columns: new[] { "MedicationTimeId", "PlannedTime", "ResidentId" },
            values: new object[,]
            {
                { 1, new TimeOnly(8, 0, 0), 1 },
                { 2, new TimeOnly(8, 0, 0), 2 },
                { 3, new TimeOnly(18, 0, 0), 3 },
                { 4, new TimeOnly(8, 0, 0), 4 },
                { 5, new TimeOnly(10, 0, 0), 5 },
                { 6, new TimeOnly(7, 45, 0), 6 },
                { 7, new TimeOnly(8, 0, 0), 7 },
                { 8, new TimeOnly(8, 0, 0), 8 },
                { 9, new TimeOnly(9, 0, 0), 9 },
                { 10, new TimeOnly(8, 0, 0), 10 },
                { 11, new TimeOnly(12, 0, 0), 11 },
                { 12, new TimeOnly(8, 0, 0), 12 },
                { 13, new TimeOnly(17, 0, 0), 13 },
                { 14, new TimeOnly(8, 0, 0), 14 },
                { 15, new TimeOnly(20, 0, 0), 15 },
                { 16, new TimeOnly(8, 0, 0), 16 }
            });

        migrationBuilder.CreateIndex(
            name: "IX_MedicationRegistrations_MedicationTimeId",
            table: "MedicationRegistrations",
            column: "MedicationTimeId");

        migrationBuilder.CreateIndex(
            name: "IX_MedicationRegistrations_ResidentSchemaId",
            table: "MedicationRegistrations",
            column: "ResidentSchemaId");

        migrationBuilder.CreateIndex(
            name: "IX_MedicationTimes_ResidentId",
            table: "MedicationTimes",
            column: "ResidentId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MedicationRegistrations");

        migrationBuilder.DropTable(
            name: "MedicationTimes");

        migrationBuilder.AddColumn<TimeOnly>(
            name: "MedicinTime",
            table: "Residents",
            type: "time",
            nullable: false,
            defaultValue: new TimeOnly(0, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 1,
            column: "MedicinTime",
            value: new TimeOnly(8, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 2,
            column: "MedicinTime",
            value: new TimeOnly(8, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 3,
            column: "MedicinTime",
            value: new TimeOnly(18, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 4,
            column: "MedicinTime",
            value: new TimeOnly(8, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 5,
            column: "MedicinTime",
            value: new TimeOnly(10, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 6,
            column: "MedicinTime",
            value: new TimeOnly(7, 45, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 7,
            column: "MedicinTime",
            value: new TimeOnly(8, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 8,
            column: "MedicinTime",
            value: new TimeOnly(8, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 9,
            column: "MedicinTime",
            value: new TimeOnly(9, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 10,
            column: "MedicinTime",
            value: new TimeOnly(8, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 11,
            column: "MedicinTime",
            value: new TimeOnly(12, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 12,
            column: "MedicinTime",
            value: new TimeOnly(8, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 13,
            column: "MedicinTime",
            value: new TimeOnly(17, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 14,
            column: "MedicinTime",
            value: new TimeOnly(8, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 15,
            column: "MedicinTime",
            value: new TimeOnly(20, 0, 0));

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 16,
            column: "MedicinTime",
            value: new TimeOnly(8, 0, 0));
    }
}
