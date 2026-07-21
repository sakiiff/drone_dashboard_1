using drone_dashboard_1.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace drone_dashboard_1.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public int DroneId { get; set; }
        public Drone Drone { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double DistanceTravelledMeters { get; set; }
        public double MaxAltitudeMeters { get; set; }
        public double MaxSpeedMetersPerSecond { get; set; }
        public double BatteryStartPercentage { get; set; }
        public double BatteryEndPercentage { get; set; }
        public FlightStatus Status { get; set; } = FlightStatus.InProgress;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
