using drone_dashboard_1.Data;
using drone_dashboard_1.DTOs.Drones;
using drone_dashboard_1.Exceptions;
using drone_dashboard_1.Hubs;
using drone_dashboard_1.Mappers.Drone;
using drone_dashboard_1.Models;
using drone_dashboard_1.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace drone_dashboard_1.Services
{
    public class DroneService : IDroneService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<DroneHub> _hubContext;

        public DroneService(ApplicationDbContext context,
            IHubContext<DroneHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<Drone> ValidateDroneID(int id)
        {
            var drone = await _context.Drones.FindAsync(id);

            if(drone == null)
            {
                throw new BusinessException(
                    ErrorCodes.DroneNotFound,
                    "Drone not found",
                    StatusCodes.Status404NotFound);
            }

            return drone;
        }

        public async Task ValidateDroneSN(string serialNumber)
        {
            var exists = await _context.Drones.AnyAsync(d => d.SerialNumber == serialNumber);

            if (exists)
            {
                throw new BusinessException(
                    ErrorCodes.DuplicateSerialNumber,
                    "Serial Number already exist!",
                    StatusCodes.Status409Conflict);
            }
        }

        public async Task<DroneResponseDTO> CreateDrone(CreateDroneDTO dto)
        {
            await ValidateDroneSN(dto.SerialNumber);

            var drone = dto.ToEntity(); 

            _context.Drones.Add(drone);
            await _context.SaveChangesAsync();

            var responseDto = drone.ToResponseDTO();

            await _hubContext.Clients.All.SendAsync(
                "ReceiveDrone",
                responseDto);

            return responseDto;
        }

        public async Task<bool> DeleteDrone(int id)
        {
            var drone = await ValidateDroneID(id);

            if (drone.IsActive == false)
            {
                throw new BusinessException(
                    ErrorCodes.DroneInactive,
                    "Drone already inactive.",
                    StatusCodes.Status409Conflict);
            }

            drone.IsActive = false;
            drone.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DroneResponseDTO>> GetDrones()
        {
            return await _context.Drones
                .Where(p => p.IsActive)
                .Select(p => new DroneResponseDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    DroneType = p.DroneType,
                    Manufacturer = p.Manufacturer,
                    Model = p.Model,
                    CurrentStatus = p.CurrentStatus,
                    CreatedAt = p.CreatedAt
                }).ToListAsync();
        }

        public async Task<DroneResponseDTO?> GetDroneById(int id)
        {
            var drone = await ValidateDroneID(id);

            var responseDto = drone.ToResponseDTO();

            return responseDto;
        }

        public async Task<DroneResponseDTO> UpdateDrone(int id, UpdateDroneDTO dto)
        {
            var drone = await ValidateDroneID(id);  

            drone.Name = dto.Name;
            drone.Manufacturer = dto.Manufacturer;
            drone.Model = dto.Model;
            drone.FirmwareVersion = dto.FirmwareVersion;
            drone.UpdatedAt = dto.UpdatedAt;

            _context.Drones.Update(drone);
            await _context.SaveChangesAsync();

            var responseDto = drone.ToResponseDTO();

            await _hubContext.Clients
                .Group($"drone-{drone.Id}")
                .SendAsync(
                "UpdateDrone",
                responseDto);

            return responseDto;
        }
    }
}
