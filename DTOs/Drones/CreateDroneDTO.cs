using drone_dashboard_1.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace drone_dashboard_1.DTOs.Drones
{
    public class CreateDroneDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public DroneType DroneType { get; set; }
        [Required]
        public string Manufacturer { get; set; } = string.Empty;
        [Required]
        public string Model { get; set; } = string.Empty;
        [Required]
        public string SerialNumber { get; set; } = string.Empty;
        [Required]
        public string FirmwareVersion { get; set; } = string.Empty;

    }
}
