using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Overlapssystem.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddDepartmentTasksAssignments : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "DepartmentTasks",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                DepartmentId = table.Column<int>(type: "int", nullable: false),
                TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DepartmentTasks", x => x.Id);
                table.ForeignKey(
                    name: "FK_DepartmentTasks_Departments_DepartmentId",
                    column: x => x.DepartmentId,
                    principalTable: "Departments",
                    principalColumn: "DepartmentId",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "ShiftTaskAssignments",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ShiftId = table.Column<int>(type: "int", nullable: false),
                DepartmentTaskId = table.Column<int>(type: "int", nullable: false),
                AssignedEmployee = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ShiftTaskAssignments", x => x.Id);
                table.ForeignKey(
                    name: "FK_ShiftTaskAssignments_DepartmentTasks_DepartmentTaskId",
                    column: x => x.DepartmentTaskId,
                    principalTable: "DepartmentTasks",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ShiftTaskAssignments_Shifts_ShiftId",
                    column: x => x.ShiftId,
                    principalTable: "Shifts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "DepartmentTasks",
            columns: new[] { "Id", "DepartmentId", "TaskName" },
            values: new object[,]
            {
                { 1, 1, "Medicinansvar" },
                { 2, 1, "Hygiejne" },
                { 3, 1, "Aftensmad" },
                { 4, 2, "Medicinansvar" },
                { 5, 2, "Hygiejne" },
                { 6, 2, "Aftensmad" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_DepartmentTasks_DepartmentId",
            table: "DepartmentTasks",
            column: "DepartmentId");

        migrationBuilder.CreateIndex(
            name: "IX_ShiftTaskAssignments_DepartmentTaskId",
            table: "ShiftTaskAssignments",
            column: "DepartmentTaskId");

        migrationBuilder.CreateIndex(
            name: "IX_ShiftTaskAssignments_ShiftId",
            table: "ShiftTaskAssignments",
            column: "ShiftId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ShiftTaskAssignments");

        migrationBuilder.DropTable(
            name: "DepartmentTasks");
    }
}
