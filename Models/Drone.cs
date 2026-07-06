using System.ComponentModel.DataAnnotations;
using drone_dashboard_1.Models.Enums;

namespace drone_dashboard_1.Models
{
    public class Drone
    {
        public int Id { get; set; }

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
        public DroneStatus CurrentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool isActive { get; set; }

    }
}
