using drone_dashboard_1.Data;
using drone_dashboard_1.DTOs.Drones;
using drone_dashboard_1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace drone_dashboard_1.Services
{
    public class DroneService : IDroneService
    {
        private readonly ApplicationDbContext _context;

        public DroneService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DroneResponseDTO> CreateDrone(CreateDroneDTO dto)
        {
            var drone = new Models.Drone
            {
                Name = dto.Name,
                DroneType = dto.DroneType,
                Manufacturer = dto.Manufacturer,
                Model = dto.Model,
                SerialNumber = dto.SerialNumber,
                FirmwareVersion = dto.FirmwareVersion,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Drones.Add(drone);
            await _context.SaveChangesAsync();

            return new DroneResponseDTO
            {
                Id = drone.Id,
                Name = drone.Name,
                Manufacturer = drone.Manufacturer,
                Model = drone.Model,
                DroneType = drone.DroneType,
                CurrentStatus = drone.CurrentStatus,
                CreatedAt = drone.CreatedAt
            };
        }

        public async Task<bool> DeleteDrone(int Id)
        {
            var drone = await _context.Drones.FindAsync(Id);
            if (drone == null) return false;

            drone.IsActive = false;
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

        public async Task<DroneResponseDTO?> GetDronesById(int Id)
        {
            var drone = await _context.Drones.FindAsync(Id);
            if (drone == null) return null;

            return new DroneResponseDTO
            {
                Id = drone.Id,
                Name = drone.Name,
                DroneType = drone.DroneType,
                Manufacturer = drone.Manufacturer,
                Model = drone.Model,
                CurrentStatus = drone.CurrentStatus,
                CreatedAt = drone.CreatedAt
            };
        }

        public async Task<DroneResponseDTO?> UpdateDrone(int Id, UpdateDroneDTO dto)
        {
            var drone = await _context.Drones.FindAsync(Id);
            if (drone == null) return null;

            drone.Name = dto.Name;
            drone.Manufacturer = dto.Manufacturer;
            drone.Model = dto.Model;
            drone.FirmwareVersion = dto.FirmwareVersion;
            drone.UpdatedAt = DateTime.UtcNow;

            _context.Drones.Update(drone);
            await _context.SaveChangesAsync();

            return new DroneResponseDTO
            {
                Id = drone.Id,
                Name = drone.Name,
                DroneType = drone.DroneType,
                Manufacturer = drone.Manufacturer,
                Model = drone.Model,
                CurrentStatus = drone.CurrentStatus,
                CreatedAt = drone.CreatedAt
            };
        }
    }
}
