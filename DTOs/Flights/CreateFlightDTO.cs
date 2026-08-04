using drone_dashboard_1.Models.Enums;

namespace drone_dashboard_1.DTOs.Flights
{
    public class CreateFlightDTO
    {
        public int DroneId { get; set; }
        public double BatteryStartPercentage { get; set; }
        public FlightStatus Status { get; set; } = FlightStatus.Idle;
        public DateTime StartTime { get; set; }

    }
}
