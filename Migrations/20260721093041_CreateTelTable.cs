using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace drone_dashboard_1.Migrations
{
    /// <inheritdoc />
    public partial class CreateTelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Telemetries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FlightId = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    AltitudeMeters = table.Column<double>(type: "double precision", nullable: false),
                    SpeedMetersPerSecond = table.Column<double>(type: "double precision", nullable: false),
                    VerticalSpeedMetersPerSecond = table.Column<double>(type: "double precision", nullable: false),
                    BatteryPercentage = table.Column<double>(type: "double precision", nullable: false),
                    HeadingDegrees = table.Column<double>(type: "double precision", nullable: false),
                    PitchDegrees = table.Column<double>(type: "double precision", nullable: false),
                    RollDegrees = table.Column<double>(type: "double precision", nullable: false),
                    YawDegrees = table.Column<double>(type: "double precision", nullable: false),
                    CurrentAmps = table.Column<double>(type: "double precision", nullable: false),
                    BatteryVoltage = table.Column<double>(type: "double precision", nullable: false),
                    SatelliteCount = table.Column<int>(type: "integer", nullable: false),
                    GpsFixType = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    FlightMode = table.Column<int>(type: "integer", nullable: false),
                    IsArmed = table.Column<bool>(type: "boolean", nullable: false),
                    DistanceFromHomeMeters = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telemetries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telemetries_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Telemetries_FlightId",
                table: "Telemetries",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telemetries");
        }
    }
}
