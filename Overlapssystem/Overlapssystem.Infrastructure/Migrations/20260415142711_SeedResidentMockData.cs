using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Overlapssystem.Infrastructure.Migrations;

/// <inheritdoc />
public partial class SeedResidentMockData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Residents",
            columns: new[] { "ResidentId", "Initials", "MedicinTime", "PaymentMethod", "ShoppingDay" },
            values: new object[,]
            {
                { 1, "L", new TimeOnly(8, 0, 0), 0, "Tirsdag" },
                { 2, "T", new TimeOnly(8, 0, 0), 1, "Onsdag" },
                { 3, "S", new TimeOnly(18, 0, 0), 0, "Torsdag" },
                { 4, "SH", new TimeOnly(8, 0, 0), 1, "Mandag" },
                { 5, "A", new TimeOnly(10, 0, 0), 2, "Fredag" },
                { 6, "B", new TimeOnly(7, 45, 0), 1, "Tirsdag" },
                { 7, "J", new TimeOnly(8, 0, 0), 2, "Onsdag" },
                { 8, "D", new TimeOnly(8, 0, 0), 0, "Torsdag" }
            });

        migrationBuilder.InsertData(
            table: "ResidentSchemas",
            columns: new[] { "ResidentSchemaId", "RelativeContact", "ResidentId", "Status", "TrafficLight" },
            values: new object[,]
            {
                { 1, "Mor: 12 34 56 78", 1, "Stabil dag, ingen bemærkninger.", 0 },
                { 2, "Far: 23 45 67 89", 2, "Lidt urolig om morgenen, faldt til ro efter morgenmad.", 1 },
                { 3, "Søster: 34 56 78 90", 3, "God dag, deltog i fællesaktiviteter.", 0 },
                { 4, "Mor: 45 67 89 01", 4, "Skal have ekstra opsyn ved badning.", 1 },
                { 5, "Bror: 56 78 90 12", 5, "Anspændt, har brug for ro.", 2 },
                { 6, "Mor: 67 89 01 23", 6, "Glad og energisk hele dagen.", 0 },
                { 7, "Far: 78 90 12 34", 7, "Lidt træt, sov middagslur.", 0 },
                { 8, "Plejefar: 89 01 23 45", 8, "Konflikt med medbeboer, kræver opfølgning.", 2 }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 5);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 6);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 7);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 8);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 5);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 6);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 7);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 8);
    }
}
