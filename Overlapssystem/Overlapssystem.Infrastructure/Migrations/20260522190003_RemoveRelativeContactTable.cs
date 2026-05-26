using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Overlapssystem.Infrastructure.Migrations;

/// <inheritdoc />
public partial class RemoveRelativeContactTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "RelativeContacts");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "RelativeContacts",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ResidentSchemaId = table.Column<int>(type: "int", nullable: false),
                ContactNote = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RelativeContacts", x => x.Id);
                table.ForeignKey(
                    name: "FK_RelativeContacts_ResidentSchemas_ResidentSchemaId",
                    column: x => x.ResidentSchemaId,
                    principalTable: "ResidentSchemas",
                    principalColumn: "ResidentSchemaId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_RelativeContacts_ResidentSchemaId",
            table: "RelativeContacts",
            column: "ResidentSchemaId");
    }
}
