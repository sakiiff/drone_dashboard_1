using drone_dashboard_1.Data;
using drone_dashboard_1.DTOs.Drones;
using drone_dashboard_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace drone_dashboard_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DroneController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DroneController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Fetch current all active drones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DroneResponseDTO>>> GetDrones()
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

        //Fetch drones by Id
        [HttpGet("{Id}")]
        public async Task<ActionResult<DroneResponseDTO>> GetDronesById(int Id)
        {
            var drone = await _context.Drones.FindAsync(Id);
            if (drone == null) return NotFound();

            var readDto = new DroneResponseDTO
            {
                Id = drone.Id,
                Name = drone.Name,
                DroneType = drone.DroneType,
                Manufacturer = drone.Manufacturer,
                Model = drone.Model,
                CurrentStatus = drone.CurrentStatus,
                CreatedAt = drone.CreatedAt
            };

            return Ok(readDto);
        }

        //Create new drone
        [HttpPost]
        public async Task<ActionResult<DroneResponseDTO>> CreateDrone(CreateDroneDTO dto)
        {
            var drone = new Drone
            {
                Name = dto.Name,
                DroneType = dto.DroneType,
                Manufacturer = dto.Manufacturer,
                Model = dto.Model,
                SerialNumber = dto.SerialNumber,
                FirmwareVersion = dto.FirmwareVersion
            };

            _context.Drones.Add(drone);
            await _context.SaveChangesAsync();

            var readDrone = new DroneResponseDTO
            {
                Id = drone.Id,
                Name = drone.Name,
                Manufacturer = drone.Manufacturer,
                Model = drone.Model,
                DroneType = drone.DroneType,
                CurrentStatus = drone.CurrentStatus,
                CreatedAt = drone.CreatedAt

            };

            return CreatedAtAction(nameof(GetDronesById), new { Id = drone.Id }, readDrone);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<DroneResponseDTO>> UpdateDrone(int Id, UpdateDroneDTO dto)
        {
            var drone = await _context.Drones.FindAsync(Id);
            if (drone == null) return NotFound();

            drone.Name = dto.Name;
            drone.Manufacturer = dto.Manufacturer;
            drone.Model = dto.Model;
            drone.FirmwareVersion = dto.FirmwareVersion;
            drone.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDrone(int Id)
        {
            var drone = await _context.Drones.FindAsync(Id);
            if (drone == null) return NotFound();

            drone.IsActive = false;

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }   

}
