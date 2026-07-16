using drone_dashboard_1.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace drone_dashboard_1.DTOs.Drones
{
    public class DroneResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DroneType DroneType { get; set; }

        [Required]
        public string Manufacturer { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;
        public DroneStatus CurrentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
