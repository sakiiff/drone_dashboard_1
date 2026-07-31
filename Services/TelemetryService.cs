using drone_dashboard_1.Data;
using drone_dashboard_1.DTOs.Telemetry;
using drone_dashboard_1.Models;
using drone_dashboard_1.Models.Enums;
using drone_dashboard_1.Services.Interfaces;
using drone_dashboard_1.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace drone_dashboard_1.Services
{
    
    public class TelemetryService : ITelemetryService
    {
        private readonly ApplicationDbContext _context;
        public TelemetryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TelemetryResponseDTO> CreateTelemetry(CreateTelemetryDTO dto)
        {
            // Check if the flight exists
            var flight = await _context.Flights.FindAsync(dto.FlightId);

            if (flight == null)
            {
                throw new BusinessException(
                    ErrorCodes.FlightNotFound,
                    "Flight not found.",
                    StatusCodes.Status404NotFound);
            }

            // Check if the flight has already ended
            if (flight.Status == FlightStatus.Completed)
            {
                throw new BusinessException(
                    ErrorCodes.TelemetryFlightCompleted,
                    "Cannot add telemetry to a completed flight.",
                    StatusCodes.Status409Conflict);
            }

            var telemetry = new Telemetry
            {
                FlightId = dto.FlightId,
                Timestamp = dto.Timestamp == default
                    ? DateTime.UtcNow
                    : dto.Timestamp,

                Longitude = dto.Longitude,
                Latitude = dto.Latitude,
                AltitudeMeters = dto.AltitudeMeters,
                SpeedMetersPerSecond = dto.SpeedMetersPerSecond,
                VerticalSpeedMetersPerSecond = dto.VerticalSpeedMetersPerSecond,

                HeadingDegrees = dto.HeadingDegrees,
                PitchDegrees = dto.PitchDegrees,
                RollDegrees = dto.RollDegrees,
                YawDegrees = dto.YawDegrees,

                BatteryPercentage = dto.BatteryPercentage,
                BatteryVoltage = dto.BatteryVoltage,
                CurrentAmps = dto.CurrentAmps,

                SatelliteCount = dto.SatelliteCount,
                GpsFixType = dto.GpsFixType,
                IsArmed = dto.IsArmed,
                FlightMode = dto.FlightMode
            };

            _context.Telemetries.Add(telemetry);
            await _context.SaveChangesAsync();

            return new TelemetryResponseDTO
            {
                Id = telemetry.Id,
                FlightId = telemetry.FlightId,
                Timestamp = telemetry.Timestamp,
                Longitude = telemetry.Longitude,
                Latitude = telemetry.Latitude,
                AltitudeMeters = telemetry.AltitudeMeters,
                SpeedMetersPerSecond = telemetry.SpeedMetersPerSecond,
                VerticalSpeedMetersPerSecond = telemetry.VerticalSpeedMetersPerSecond,
                BatteryPercentage = telemetry.BatteryPercentage,
                BatteryVoltage = telemetry.BatteryVoltage,
                CurrentAmps = telemetry.CurrentAmps,
                SatelliteCount = telemetry.SatelliteCount,
                GpsFixType = telemetry.GpsFixType,
                IsArmed = telemetry.IsArmed,
                FlightMode = telemetry.FlightMode
            };
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

            var telemetries = await _context.Telemetries
                .Include(t => t.Flight)
                .ThenInclude(f => f.Drone)
                .AsNoTracking()
                .Where(t => t.Flight.DroneId == droneId)
                .OrderByDescending(t => t.Timestamp)
                .FirstOrDefaultAsync();

            if (telemetries == null)
                return null;

            return new TelemetryResponseDTO
            {
                Id = telemetries.Id,
                DroneName = telemetries.Flight.Drone.Name,
                FlightId = telemetries.FlightId,
                Timestamp = telemetries.Timestamp,
                Latitude = telemetries.Latitude,
                Longitude = telemetries.Longitude,
                AltitudeMeters = telemetries.AltitudeMeters,
                SpeedMetersPerSecond = telemetries.SpeedMetersPerSecond,
                VerticalSpeedMetersPerSecond = telemetries.VerticalSpeedMetersPerSecond,
                BatteryPercentage = telemetries.BatteryPercentage,
                BatteryVoltage = telemetries.BatteryVoltage,
                CurrentAmps = telemetries.CurrentAmps,
                SatelliteCount = telemetries.SatelliteCount,
                GpsFixType = telemetries.GpsFixType,
                IsArmed = telemetries.IsArmed,
                FlightMode = telemetries.FlightMode
            };
        }

        public async Task<IEnumerable<TelemetryResponseDTO>> GetTelemetryHistory(int flightId, int pageSize, DateTime start, DateTime end)
        {
            var flightExist = await _context.Flights.AnyAsync(d => d.Id == flightId); 

            if (!flightExist)
            {
                throw new BusinessException(
                    ErrorCodes.TelemetryFlightNotFound,
                    "Flight does not exist",
                    StatusCodes.Status404NotFound);
            }

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
