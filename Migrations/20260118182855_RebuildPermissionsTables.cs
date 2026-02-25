using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class RebuildPermissionsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("ApplicationUserPermissions");
            migrationBuilder.DropTable("ApplicationRolePermissions");
            migrationBuilder.DropTable("ApplicationPermissions");
            // ===============================
            // ApplicationPermissions
            // Id MANUAL (NO IDENTITY)
            // ===============================

            migrationBuilder.CreateTable(
                name: "ApplicationPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 200, nullable: false),
                    Module = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPermissions", x => x.Id);
                });

            // ===============================
            // ApplicationRolePermissions
            // ===============================

            migrationBuilder.CreateTable(
                name: "ApplicationRolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    RoleId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRolePermissions", x => x.Id);

                    table.ForeignKey(
                        name: "FK_ApplicationRolePermissions_ApplicationPermissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ApplicationPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_ApplicationRolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // ===============================
            // ApplicationUserPermissions
            // ===============================

            migrationBuilder.CreateTable(
                name: "ApplicationUserPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    UserId = table.Column<string>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserPermissions", x => x.Id);

                    table.ForeignKey(
                        name: "FK_ApplicationUserPermissions_ApplicationPermissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ApplicationPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);

                    table.ForeignKey(
                        name: "FK_ApplicationUserPermissions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            // ===============================
            // INDEXES
            // ===============================

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRolePermissions_RoleId",
                table: "ApplicationRolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRolePermissions_PermissionId",
                table: "ApplicationRolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPermissions_UserId",
                table: "ApplicationUserPermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPermissions_PermissionId",
                table: "ApplicationUserPermissions",
                column: "PermissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ApplicationUserPermissions");
            migrationBuilder.DropTable(name: "ApplicationRolePermissions");
            migrationBuilder.DropTable(name: "ApplicationPermissions");
        }
    }
}
