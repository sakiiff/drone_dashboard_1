using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace drone_dashboard_1.Migrations
{
    /// <inheritdoc />
    public partial class SecondInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Drones",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Drones",
                newName: "isActive");
        }
    }
}
