using drone_dashboard_1.DTOs.Drones;
using drone_dashboard_1.DTOs.Flights;

namespace drone_dashboard_1.Mappers.Flight
{
    public static class FlightMapper
    {
        public static Models.Flight ToEntity(this CreateFlightDTO dto)
        {
            return new Models.Flight
            {
                DroneId = dto.DroneId,
                BatteryStartPercentage = dto.BatteryStartPercentage,
                Status = Models.Enums.FlightStatus.InProgress,
                StartTime = DateTime.UtcNow
            };
        }

        public static FlightResponseDTO ToResponseDto(this Models.Flight flight)
        {
            return new FlightResponseDTO
            {
                Id = flight.Id,
                DroneId = flight.DroneId,
                DroneName = flight.Drone.Name,
                StartTime = flight.StartTime,
                EndTime = flight.EndTime,
                BatteryStartPercentage = flight.BatteryStartPercentage,
                BatteryEndPercentage = flight.BatteryEndPercentage,
                DistanceTravelledMeters = flight.DistanceTravelledMeters,
                MaxAltitudeMeters = flight.MaxAltitudeMeters,
                MaxSpeedMetersPerSecond = flight.MaxSpeedMetersPerSecond,
                Status = flight.Status
            };
        }

        public static EndFlightDTO ToEndFlightDto(this Models.Flight flight)
        {
            return new EndFlightDTO
            {
                EndTime = DateTime.UtcNow,
                BatteryEndPercentage = flight.BatteryEndPercentage,
                MaxAltitudeMeters = flight.MaxAltitudeMeters,
                DistanceTravelledMeters = flight.DistanceTravelledMeters,
                MaxSpeedMetersPerSecond = flight.MaxSpeedMetersPerSecond,
                Status = Models.Enums.FlightStatus.Completed
            };
        }
    }
}
