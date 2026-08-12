using drone_dashboard_1.Data;
using drone_dashboard_1.DTOs.Telemetry;
using drone_dashboard_1.Exceptions;
using drone_dashboard_1.Hubs;
using drone_dashboard_1.Mappers.Telemetry;
using drone_dashboard_1.Models;
using drone_dashboard_1.Models.Enums;
using drone_dashboard_1.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace drone_dashboard_1.Services
{
    
    public class TelemetryService : ITelemetryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<TelemetryHub> _hubContext;
        public TelemetryService(
            ApplicationDbContext context,
            IHubContext<TelemetryHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        private async Task<Flight> ValidateFlight(int flightId)
        {
            var flight = await _context.Flights.FindAsync(flightId);

            if (flight == null)
            {
                throw new BusinessException(
                    ErrorCodes.FlightNotFound,
                    "Flight not found.",
                    StatusCodes.Status404NotFound);
            }

            if (flight.Status == FlightStatus.Completed)
            {
                throw new BusinessException(
                    ErrorCodes.TelemetryFlightCompleted,
                    "Cannot add telemetry to a completed flight.",
                    StatusCodes.Status409Conflict);
            }

            return flight;
        }

        public async Task<TelemetryResponseDTO> CreateTelemetry(CreateTelemetryDTO dto)
        {
            var flight = await ValidateFlight(dto.FlightId);

            var telemetry = dto.ToEntity();

            _context.Telemetries.Add(telemetry);
            await _context.SaveChangesAsync();

            var responseDto = telemetry.ToResponseDTO(flight.Drone?.Name);

            Console.WriteLine($"[Server] Broadcasting TelemetryUpdated to group: flight-{flight.Id}");

            await _hubContext.Clients
                .Group($"flight-{dto.FlightId}")
                .SendAsync(
                "TelemetryUpdated",
                responseDto);

            return responseDto;
        }

        public async Task<TelemetryResponseDTO?> GetLatestTelemetry(int droneId)
        {
            var droneExists = await _context.Drones.AnyAsync(d => d.Id == droneId);
            if (!droneExists)
            {
                throw new BusinessException(
                    ErrorCodes.TelemetryDroneReferencedNotFound,
                    "Referenced drone not found",
                    StatusCodes.Status404NotFound);
            }

            var result = await _context.Telemetries
                .AsNoTracking()
                .Where(t => t.Flight.DroneId == droneId)
                .OrderByDescending(t => t.Timestamp)
                .Select(t => new TelemetryResponseDTO
                {
                    Id = t.Id,
                    DroneName = t.Flight.Drone.Name,
                    FlightId = t.FlightId,
                    Timestamp = t.Timestamp,
                    Latitude = t.Latitude,
                    Longitude = t.Longitude,
                    AltitudeMeters = t.AltitudeMeters,
                    SpeedMetersPerSecond = t.SpeedMetersPerSecond,
                    VerticalSpeedMetersPerSecond = t.VerticalSpeedMetersPerSecond,
                    BatteryPercentage = t.BatteryPercentage,
                    BatteryVoltage = t.BatteryVoltage,
                    CurrentAmps = t.CurrentAmps,
                    SatelliteCount = t.SatelliteCount,
                    GpsFixType = t.GpsFixType,
                    IsArmed = t.IsArmed,
                    FlightMode = t.FlightMode
                })
                .FirstOrDefaultAsync();

            await _hubContext.Clients
                .Group($"drone-{droneId}")
                .SendAsync(
                "LatestTelemetry",
                result);

            return result;
        }

        public async Task<IEnumerable<TelemetryResponseDTO>> GetTelemetryHistory(int flightId, int pageSize, DateTime start, DateTime end)
        {
            var flight = await ValidateFlight(flightId);

            if (end <= start)
            {
                throw new BusinessException(
                    ErrorCodes.TelemetryTimestampError,
                    "End time must be after Start time!",
                    StatusCodes.Status409Conflict);
            }

            return await _context.Telemetries
                .Where(t => t.FlightId == flightId
                    && t.Timestamp >= start
                    && t.Timestamp < end)
                .AsNoTracking()
                .OrderBy(t => t.Timestamp)
                .Take(pageSize)
                .Select(t => new TelemetryResponseDTO
                {
                    Id = t.Id,
                    DroneName = t.Flight.Drone.Name,
                    FlightId = t.FlightId,
                    Timestamp = t.Timestamp,
                    Latitude = t.Latitude,
                    Longitude = t.Longitude,
                    AltitudeMeters = t.AltitudeMeters,
                    SpeedMetersPerSecond = t.SpeedMetersPerSecond,
                    VerticalSpeedMetersPerSecond = t.VerticalSpeedMetersPerSecond,
                    BatteryPercentage = t.BatteryPercentage,
                    BatteryVoltage = t.BatteryVoltage,
                    CurrentAmps = t.CurrentAmps,
                    SatelliteCount = t.SatelliteCount,
                    GpsFixType = t.GpsFixType,
                    IsArmed = t.IsArmed,
                    FlightMode = t.FlightMode
                })
                .ToListAsync();
        }
    }
}
