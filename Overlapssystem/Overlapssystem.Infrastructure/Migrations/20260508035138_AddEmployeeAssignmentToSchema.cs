using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Overlapssystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeAssignmentToSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrimaryEmployeeId",
                table: "ResidentSchemas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondaryEmployeeId",
                table: "ResidentSchemas",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 1,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 2,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 3,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 4,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 5,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 6,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 7,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 8,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 9,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 10,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 11,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 12,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 13,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 14,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 15,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ResidentSchemas",
                keyColumn: "ResidentSchemaId",
                keyValue: 16,
                columns: new[] { "PrimaryEmployeeId", "SecondaryEmployeeId" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_ResidentSchemas_PrimaryEmployeeId",
                table: "ResidentSchemas",
                column: "PrimaryEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentSchemas_SecondaryEmployeeId",
                table: "ResidentSchemas",
                column: "SecondaryEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResidentSchemas_Employees_PrimaryEmployeeId",
                table: "ResidentSchemas",
                column: "PrimaryEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResidentSchemas_Employees_SecondaryEmployeeId",
                table: "ResidentSchemas",
                column: "SecondaryEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResidentSchemas_Employees_PrimaryEmployeeId",
                table: "ResidentSchemas");

            migrationBuilder.DropForeignKey(
                name: "FK_ResidentSchemas_Employees_SecondaryEmployeeId",
                table: "ResidentSchemas");

            migrationBuilder.DropIndex(
                name: "IX_ResidentSchemas_PrimaryEmployeeId",
                table: "ResidentSchemas");

            migrationBuilder.DropIndex(
                name: "IX_ResidentSchemas_SecondaryEmployeeId",
                table: "ResidentSchemas");

            migrationBuilder.DropColumn(
                name: "PrimaryEmployeeId",
                table: "ResidentSchemas");

            migrationBuilder.DropColumn(
                name: "SecondaryEmployeeId",
                table: "ResidentSchemas");
        }
    }
}
