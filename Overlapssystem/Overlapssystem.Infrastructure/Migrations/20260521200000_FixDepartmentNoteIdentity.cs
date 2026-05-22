using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Overlapssystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDepartmentNoteIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE [DepartmentNotes] DROP CONSTRAINT [PK_DepartmentNotes];");
            migrationBuilder.Sql("ALTER TABLE [DepartmentNotes] ADD [DepartmentId_New] int NOT NULL DEFAULT 0;");
            migrationBuilder.Sql("UPDATE [DepartmentNotes] SET [DepartmentId_New] = [DepartmentId];");
            migrationBuilder.Sql("ALTER TABLE [DepartmentNotes] DROP COLUMN [DepartmentId];");
            migrationBuilder.Sql("EXEC sp_rename 'DepartmentNotes.DepartmentId_New', 'DepartmentId', 'COLUMN';");
            migrationBuilder.Sql("ALTER TABLE [DepartmentNotes] ADD CONSTRAINT [PK_DepartmentNotes] PRIMARY KEY ([DepartmentId]);");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
