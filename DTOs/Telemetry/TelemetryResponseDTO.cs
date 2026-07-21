using drone_dashboard_1.Models.Enums;

namespace drone_dashboard_1.DTOs.Telemetry
{
    public class TelemetryResponseDTO
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public DateTime Timestamp { get; set; }

        // GPS
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double AltitudeMeters { get; set; }

        // Movement
        public double SpeedMetersPerSecond { get; set; }
        public double VerticalSpeedMetersPerSecond { get; set; }

        // Battery
        public double BatteryPercentage { get; set; }
        public double BatteryVoltage { get; set; }
        public double CurrentAmps { get; set; }

        // Attitude
        public double HeadingDegrees { get; set; }
        public double PitchDegrees { get; set; }
        public double RollDegrees { get; set; }
        public double YawDegrees { get; set; }

        // GPS quality
        public int SatelliteCount { get; set; }
        public GpsFixType GpsFixType { get; set; }

        // Drone state
        public bool IsArmed { get; set; }
        public FlightMode FlightMode { get; set; }
    }
}
