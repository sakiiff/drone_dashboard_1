using drone_dashboard_1.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace drone_dashboard_1.DTOs.Telemetry
{
    public class CreateTelemetryDTO
    {
        public int FlightId { get; set; }
        public DateTime Timestamp { get; set; }

        [Range (-180, 180)]
        public double Longitude { get; set; }

        [Range(-90, 90)]
        public double Latitude { get; set; }
        public double AltitudeMeters { get; set; }
        public double SpeedMetersPerSecond { get; set; }
        public double VerticalSpeedMetersPerSecond { get; set; }

        [Range(0, 100)]
        public double BatteryPercentage { get; set; }
        public double HeadingDegrees { get; set; }
        public double PitchDegrees { get; set; }
        public double RollDegrees { get; set; }
        public double YawDegrees { get; set; }
        public double CurrentAmps { get; set; }
        public double BatteryVoltage { get; set; }
        public int SatelliteCount { get; set; }
        public bool IsArmed { get; set; }
        public GpsFixType GpsFixType { get; set; }
        public FlightMode FlightMode { get; set; } = FlightMode.Loiter;

    }
}
