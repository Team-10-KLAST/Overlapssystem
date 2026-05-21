using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Overlapssystem.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddShiftIdToResidentSchema : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "ShiftId",
            table: "ResidentSchemas",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 1,
            column: "ShiftId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 2,
            column: "ShiftId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 3,
            column: "ShiftId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 4,
            column: "ShiftId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 5,
            column: "ShiftId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 6,
            column: "ShiftId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 7,
            column: "ShiftId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 8,
            column: "ShiftId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 9,
            column: "ShiftId",
            value: 2);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 10,
            column: "ShiftId",
            value: 2);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 11,
            column: "ShiftId",
            value: 2);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 12,
            column: "ShiftId",
            value: 2);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 13,
            column: "ShiftId",
            value: 2);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 14,
            column: "ShiftId",
            value: 2);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 15,
            column: "ShiftId",
            value: 2);

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 16,
            column: "ShiftId",
            value: 2);

        migrationBuilder.InsertData(
            table: "Shifts",
            columns: new[] { "Id", "Date", "DepartmentId", "Type" },
            values: new object[,]
            {
                { 1, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                { 2, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 }
            });

        migrationBuilder.CreateIndex(
            name: "IX_ResidentSchemas_ShiftId",
            table: "ResidentSchemas",
            column: "ShiftId");

        migrationBuilder.AddForeignKey(
            name: "FK_ResidentSchemas_Shifts_ShiftId",
            table: "ResidentSchemas",
            column: "ShiftId",
            principalTable: "Shifts",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_ResidentSchemas_Shifts_ShiftId",
            table: "ResidentSchemas");

        migrationBuilder.DropIndex(
            name: "IX_ResidentSchemas_ShiftId",
            table: "ResidentSchemas");

        migrationBuilder.DeleteData(
            table: "Shifts",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Shifts",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DropColumn(
            name: "ShiftId",
            table: "ResidentSchemas");
    }
}
