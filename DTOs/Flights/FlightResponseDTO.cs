using drone_dashboard_1.Models.Enums;

namespace drone_dashboard_1.DTOs.Flights
{
    public class FlightResponseDTO
    {
        public int Id { get; set; }
        public int DroneId { get; set; }
        public string DroneName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double BatteryStartPercentage { get; set; }
        public double BatteryEndPercentage { get; set; }
        public double DistanceTravelledMeters { get; set; }
        public double MaxAltitudeMeters { get; set; }
        public double MaxSpeedMetersPerSecond { get; set; }
        public FlightStatus Status { get; set; }
    }
}
