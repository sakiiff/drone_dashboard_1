using drone_dashboard_1.Data;
using drone_dashboard_1.DTOs.Flights;
using drone_dashboard_1.Exceptions;
using drone_dashboard_1.Hubs;
using drone_dashboard_1.Models;
using drone_dashboard_1.Models.Enums;
using drone_dashboard_1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using drone_dashboard_1.Mappers.Flight;

namespace drone_dashboard_1.Services
{
    public class FlightService : IFlightService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<FlightHub> _hubContext;

        public FlightService(
            ApplicationDbContext context,
            IHubContext<FlightHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<Flight> ValidateFlightId(int id)
        {
            var flight = await _context.Flights.FindAsync(id);

            if (flight == null)
            {
                throw new BusinessException(
                    ErrorCodes.FlightNotFound,
                    "Flight not found",
                    StatusCodes.Status404NotFound);
            }

            return flight;
        }

        public async Task<Flight> ValidateFlightWithDrone(int id)
        {
            var flight = await _context.Flights
                .Include(f => f.Drone)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (flight == null)
            {
                throw new BusinessException(
                    ErrorCodes.FlightNotFound,
                    "Flight not found",
                    StatusCodes.Status404NotFound);
            }

            return flight;
        }

        public async Task<Drone> ValidateDroneId(int id)
        {
            var drone = await _context.Drones.FindAsync(id);

            if (drone == null)
            {
                throw new BusinessException(
                    ErrorCodes.DroneNotFound,
                    "Drone not found",
                    StatusCodes.Status404NotFound);
            }

            return drone;
        }

        public async Task<Drone> ValidateFlightWithDroneActive(int id)
        {
            var activeFlight = await _context.Flights.AnyAsync(f => 
                f.DroneId == id && 
                f.Status == FlightStatus.InProgress);

            if (activeFlight)
            {
                throw new BusinessException(
                    ErrorCodes.FlightAlreadyInProgress,
                    "Drone already has an active flight",
                    StatusCodes.Status409Conflict);
            }

            var drone = await _context.Drones.FindAsync(id);

            if (drone == null || !drone.IsActive)
            {
                throw new BusinessException(
                    ErrorCodes.DroneNotFound,
                    "Drone not found",
                    StatusCodes.Status404NotFound);
            }

            return drone;
        }

        public async Task<IEnumerable<FlightResponseDTO>> GetFlights()
        {
            return await _context.Flights
                .Select(f => new FlightResponseDTO
                {
                    Id = f.Id,
                    DroneName = f.Drone.Name,
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
            var flight = await ValidateFlightWithDrone(id);
            
            var responseDto = flight.ToResponseDto();

            return responseDto;
        }

        public async Task<FlightResponseDTO> CreateFlight(CreateFlightDTO dto)
        {
            var drone = await ValidateFlightWithDroneActive(dto.DroneId);

            var flight = dto.ToEntity();

            _context.Flights.Add(flight);

            await _context.SaveChangesAsync();

            flight.Drone = drone; 

            return flight.ToResponseDto();
        }

        public async Task<EndFlightDTO> EndFlight(int id, EndFlightDTO dto)
        {
            var flight = await ValidateFlightId(id);

            if (flight.Status == FlightStatus.Completed)
            {
                throw new BusinessException(
                    ErrorCodes.FlightAlreadyCompleted,
                    "Flight already completed",
                    StatusCodes.Status409Conflict);
            }

            flight.EndTime = DateTime.UtcNow;
            flight.BatteryEndPercentage = dto.BatteryEndPercentage;
            flight.MaxAltitudeMeters = dto.MaxAltitudeMeters;
            flight.DistanceTravelledMeters = dto.DistanceTravelledMeters;
            flight.MaxSpeedMetersPerSecond = dto.MaxSpeedMetersPerSecond;
            flight.Status = FlightStatus.Completed;

            await _context.SaveChangesAsync();

            return flight.ToEndFlightDto();
        }
    }
}