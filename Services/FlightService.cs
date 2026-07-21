using drone_dashboard_1.Data;
using drone_dashboard_1.DTOs.Flights;
using drone_dashboard_1.Models;
using drone_dashboard_1.Models.Enums;
using drone_dashboard_1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace drone_dashboard_1.Services
{
    public class FlightService : IFlightService
    {
        private readonly ApplicationDbContext _context;

        public FlightService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FlightResponseDTO>> GetFlights()
        {
            return await _context.Flights
                .Select(f => new FlightResponseDTO
                {
                    Id = f.Id,
                    DroneId = f.DroneId,
                    BatteryStartPercentage = f.BatteryStartPercentage,
                    Status = f.Status,
                    StartTime = f.StartTime,
                    EndTime = f.EndTime
                })
                .ToListAsync();
        }

        public async Task<FlightResponseDTO?> GetFlightById(int id)
        {
            var flight = await _context.Flights
                .Include(f => f.Drone)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (flight == null)
                return null;

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

        public async Task<FlightResponseDTO> CreateFlight(CreateFlightDTO dto)
        {
            var flight = new Flight
            {
                DroneId = dto.DroneId,
                BatteryStartPercentage = dto.BatteryStartPercentage,
                StartTime = DateTime.UtcNow,
                Status = FlightStatus.InProgress,
                CreatedAt = DateTime.UtcNow
            };

            _context.Flights.Add(flight);

            await _context.SaveChangesAsync();

            return new FlightResponseDTO
            {
                Id = flight.Id,
                DroneId = flight.DroneId,
                BatteryStartPercentage = flight.BatteryStartPercentage,
                Status = flight.Status,
                StartTime = flight.StartTime
            };
        }

        public async Task<bool> EndFlight(int id, EndFlightDTO dto)
        {
            var flight = await _context.Flights.FindAsync(id);

            if (flight == null)
                return false;

            flight.EndTime = DateTime.UtcNow;
            flight.BatteryEndPercentage = dto.BatteryEndPercentage;
            flight.DistanceTravelledMeters = dto.DistanceTravelledMeters;
            flight.MaxAltitudeMeters = dto.MaxAltitudeMeters;
            flight.MaxSpeedMetersPerSecond = dto.MaxSpeedMetersPerSecond;

            flight.Status = FlightStatus.Completed;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}