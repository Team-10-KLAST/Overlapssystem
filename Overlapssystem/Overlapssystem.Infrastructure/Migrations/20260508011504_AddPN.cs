using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Overlapssystem.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddPN : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "PNs",
            columns: table => new
            {
                PNId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                GivenTime = table.Column<TimeOnly>(type: "time", nullable: false),
                ResidentSchemaId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PNs", x => x.PNId);
                table.ForeignKey(
                    name: "FK_PNs_ResidentSchemas_ResidentSchemaId",
                    column: x => x.ResidentSchemaId,
                    principalTable: "ResidentSchemas",
                    principalColumn: "ResidentSchemaId");
            });

        migrationBuilder.CreateIndex(
            name: "IX_PNs_ResidentSchemaId",
            table: "PNs",
            column: "ResidentSchemaId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PNs");
    }
}
