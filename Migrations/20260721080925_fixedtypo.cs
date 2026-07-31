using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace drone_dashboard_1.Migrations
{
    /// <inheritdoc />
    public partial class fixedTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxAltitude",
                table: "Flights",
                newName: "MaxAltitudeMeters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxAltitudeMeters",
                table: "Flights",
                newName: "MaxAltitude");
        }
    }
}
