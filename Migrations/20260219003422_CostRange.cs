using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class CostRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Variant_Cost",
                table: "Variants");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Variant_Cost",
                table: "Variants",
                sql: "[Cost] > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Variant_Cost",
                table: "Variants");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Variant_Cost",
                table: "Variants",
                sql: "[Cost] BETWEEN 0.001 AND 999999999999");
        }
    }
}
