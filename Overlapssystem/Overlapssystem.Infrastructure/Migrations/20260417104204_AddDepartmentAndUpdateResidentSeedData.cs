using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Overlapssystem.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddDepartmentAndUpdateResidentSeedData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "DepartmentId",
            table: "Residents",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "Departments",
            columns: table => new
            {
                DepartmentId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Departments", x => x.DepartmentId);
            });

        migrationBuilder.InsertData(
            table: "Departments",
            columns: new[] { "DepartmentId", "DepartmentName" },
            values: new object[,]
            {
                { 1, "Skoven" },
                { 2, "Slottet" }
            });

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 1,
            column: "RelativeContact",
            value: "Mor ringede, kommer på besøg torsdag eftermiddag.");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 2,
            column: "RelativeContact",
            value: "Ingen kontakt i dag.");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 3,
            column: "RelativeContact",
            value: "Søster hentede til frokost, aftale om næste besøg fredag.");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 4,
            column: "RelativeContact",
            value: "Far besøgte kl. 14, gik godt.");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 5,
            column: "RelativeContact",
            value: "Mor ringede, beboer ville ikke tale i telefon.");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 6,
            column: "RelativeContact",
            value: "Ingen kontakt i dag.");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 7,
            column: "RelativeContact",
            value: "Plejefar afleverede tøj, kort besøg.");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 8,
            column: "RelativeContact",
            value: "Aftalt med mor at hun ringer i morgen kl. 10.");

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 1,
            column: "DepartmentId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 2,
            column: "DepartmentId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 3,
            column: "DepartmentId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 4,
            column: "DepartmentId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 5,
            column: "DepartmentId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 6,
            column: "DepartmentId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 7,
            column: "DepartmentId",
            value: 1);

        migrationBuilder.UpdateData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 8,
            column: "DepartmentId",
            value: 1);

        migrationBuilder.InsertData(
            table: "Residents",
            columns: new[] { "ResidentId", "DepartmentId", "Initials", "MedicinTime", "PaymentMethod", "ShoppingDay" },
            values: new object[,]
            {
                { 9, 2, "M", new TimeOnly(9, 0, 0), 0, "Mandag" },
                { 10, 2, "K", new TimeOnly(8, 0, 0), 1, "Tirsdag" },
                { 11, 2, "P", new TimeOnly(12, 0, 0), 2, "Onsdag" },
                { 12, 2, "R", new TimeOnly(8, 0, 0), 0, "Torsdag" },
                { 13, 2, "N", new TimeOnly(17, 0, 0), 1, "Fredag" },
                { 14, 2, "F", new TimeOnly(8, 0, 0), 2, "Mandag" },
                { 15, 2, "E", new TimeOnly(20, 0, 0), 0, "Onsdag" },
                { 16, 2, "H", new TimeOnly(8, 0, 0), 1, "Fredag" }
            });

        migrationBuilder.InsertData(
            table: "ResidentSchemas",
            columns: new[] { "ResidentSchemaId", "RelativeContact", "ResidentId", "Status", "TrafficLight" },
            values: new object[,]
            {
                { 9, "Mor kom forbi med kage, hyggeligt besøg.", 9, "Rolig dag, god stemning.", 0 },
                { 10, "Ingen kontakt i dag.", 10, "Nægtede morgenmad, spiste lidt til frokost.", 1 },
                { 11, "Bror ringede, vil besøge i weekenden.", 11, "Deltog i musik-aktivitet, sang med.", 0 },
                { 12, "Plejemor hentede til tandlæge kl. 10.", 12, "Træt efter tandlægebesøg, hvilede sig.", 1 },
                { 13, "Far ringede, beboer glad for samtalen.", 13, "God dag, hjalp med at dække bord.", 0 },
                { 14, "Ingen kontakt i dag.", 14, "Urolig, havde svært ved at falde til ro.", 2 },
                { 15, "Mor og søster besøgte, gik en tur sammen.", 15, "Glad efter besøg, snakkede om det hele aftenen.", 0 },
                { 16, "Aftalt med far at han kommer lørdag.", 16, "Stille dag, så film om eftermiddagen.", 0 }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Departments");

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 9);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 10);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 11);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 12);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 13);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 14);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 15);

        migrationBuilder.DeleteData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 16);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 9);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 10);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 11);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 12);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 13);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 14);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 15);

        migrationBuilder.DeleteData(
            table: "Residents",
            keyColumn: "ResidentId",
            keyValue: 16);

        migrationBuilder.DropColumn(
            name: "DepartmentId",
            table: "Residents");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 1,
            column: "RelativeContact",
            value: "Mor: 12 34 56 78");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 2,
            column: "RelativeContact",
            value: "Far: 23 45 67 89");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 3,
            column: "RelativeContact",
            value: "Søster: 34 56 78 90");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 4,
            column: "RelativeContact",
            value: "Mor: 45 67 89 01");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 5,
            column: "RelativeContact",
            value: "Bror: 56 78 90 12");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 6,
            column: "RelativeContact",
            value: "Mor: 67 89 01 23");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 7,
            column: "RelativeContact",
            value: "Far: 78 90 12 34");

        migrationBuilder.UpdateData(
            table: "ResidentSchemas",
            keyColumn: "ResidentSchemaId",
            keyValue: 8,
            column: "RelativeContact",
            value: "Plejefar: 89 01 23 45");
    }
}
