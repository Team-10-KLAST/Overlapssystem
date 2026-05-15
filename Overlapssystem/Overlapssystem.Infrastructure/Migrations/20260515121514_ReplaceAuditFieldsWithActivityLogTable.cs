using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Overlapssystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceAuditFieldsWithActivityLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationRegistrations_Employees_ModifiedByEmployeeId",
                table: "MedicationRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_ResidentSchemas_Employees_ModifiedByEmployeeId",
                table: "ResidentSchemas");

            migrationBuilder.DropIndex(
                name: "IX_ResidentSchemas_ModifiedByEmployeeId",
                table: "ResidentSchemas");

            migrationBuilder.DropIndex(
                name: "IX_MedicationRegistrations_ModifiedByEmployeeId",
                table: "MedicationRegistrations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ResidentSchemas");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "ResidentSchemas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ResidentSchemas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MedicationRegistrations");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "MedicationRegistrations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MedicationRegistrations");

            migrationBuilder.CreateTable(
                name: "ActivityLogEntries",
                columns: table => new
                {
                    ActivityLogEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ResidentSchemaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogEntries", x => x.ActivityLogEntryId);
                    table.ForeignKey(
                        name: "FK_ActivityLogEntries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_ActivityLogEntries_ResidentSchemas_ResidentSchemaId",
                        column: x => x.ResidentSchemaId,
                        principalTable: "ResidentSchemas",
                        principalColumn: "ResidentSchemaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Residents_DepartmentId",
                table: "Residents",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogEntries_EmployeeId",
                table: "ActivityLogEntries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogEntries_ResidentSchemaId",
                table: "ActivityLogEntries",
                column: "ResidentSchemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Residents_Departments_DepartmentId",
                table: "Residents",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Residents_Departments_DepartmentId",
                table: "Residents");

            migrationBuilder.DropTable(
                name: "ActivityLogEntries");

            migrationBuilder.DropIndex(
                name: "IX_Residents_DepartmentId",
                table: "Residents");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ResidentSchemas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByEmployeeId",
                table: "ResidentSchemas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ResidentSchemas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MedicationRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByEmployeeId",
                table: "MedicationRegistrations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MedicationRegistrations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedByEmployeeId", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.CreateIndex(
                name: "IX_ResidentSchemas_ModifiedByEmployeeId",
                table: "ResidentSchemas",
                column: "ModifiedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationRegistrations_ModifiedByEmployeeId",
                table: "MedicationRegistrations",
                column: "ModifiedByEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationRegistrations_Employees_ModifiedByEmployeeId",
                table: "MedicationRegistrations",
                column: "ModifiedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResidentSchemas_Employees_ModifiedByEmployeeId",
                table: "ResidentSchemas",
                column: "ModifiedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }
    }
}
