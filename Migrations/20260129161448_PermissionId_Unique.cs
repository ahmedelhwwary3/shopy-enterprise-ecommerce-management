using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class PermissionId_Unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ApplicationPermissions_Id",
                table: "ApplicationPermissions",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationPermissions_Id",
                table: "ApplicationPermissions");
        }
    }
}
