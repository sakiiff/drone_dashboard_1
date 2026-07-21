using drone_dashboard_1.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace drone_dashboard_1.Models
{
    public class Telemetry
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double AltitudeMeters { get; set; }
        public double SpeedMetersPerSecond { get; set; }
        public double VerticalSpeedMetersPerSecond { get; set; }
        public double BatteryPercentage { get; set; }
        public double HeadingDegrees { get; set; }
        public double PitchDegrees { get; set; }
        public double RollDegrees { get; set; }
        public double YawDegrees { get; set; }
        public double CurrentAmps {  get; set; }
        public double BatteryVoltage { get; set; }
        public int SatelliteCount { get; set; }
        public GpsFixType GpsFixType { get; set; } 
        public FlightMode FlightMode { get; set; } = FlightMode.Loiter;
        public bool IsArmed { get; set; }
        public double DistanceFromHomeMeters { get; set; }

    }
}
