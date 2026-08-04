using drone_dashboard_1.DTOs.Drones;
using drone_dashboard_1.Models;

namespace drone_dashboard_1.Mappers.Drone
{
    public static class DroneMapper
    {
        public static Models.Drone ToEntity(this CreateDroneDTO dto)
        {
            return new Models.Drone
            {
                Name = dto.Name,
                DroneType = dto.DroneType,
                Manufacturer = dto.Manufacturer,
                Model = dto.Model,
                SerialNumber = dto.SerialNumber,
                FirmwareVersion = dto.FirmwareVersion
            };
        }

        public static DroneResponseDTO ToResponseDTO(this Models.Drone drone)
        {
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

        public static UpdateDroneDTO ToUpdateDTO(this Models.Drone drone)
        {
            return new UpdateDroneDTO
            {
                Name = drone.Name,
                Manufacturer = drone.Manufacturer,
                Model = drone.Model,
                FirmwareVersion = drone.FirmwareVersion,
                UpdatedAt = drone.UpdatedAt
            };
        }
    }
}
