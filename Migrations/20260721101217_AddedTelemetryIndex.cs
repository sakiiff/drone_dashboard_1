using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace drone_dashboard_1.Migrations
{
    /// <inheritdoc />
    public partial class AddedTelemetryIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Telemetries_FlightId",
                table: "Telemetries");

            migrationBuilder.AlterColumn<int>(
                name: "GpsFixType",
                table: "Telemetries",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.CreateIndex(
                name: "IX_Telemetries_FlightId_Timestamp",
                table: "Telemetries",
                columns: new[] { "FlightId", "Timestamp" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Telemetries_FlightId_Timestamp",
                table: "Telemetries");

            migrationBuilder.AlterColumn<decimal>(
                name: "GpsFixType",
                table: "Telemetries",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Telemetries_FlightId",
                table: "Telemetries",
                column: "FlightId");
        }
    }
}
