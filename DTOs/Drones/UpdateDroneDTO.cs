using System.ComponentModel.DataAnnotations;

namespace drone_dashboard_1.DTOs.Drones
{
    public class UpdateDroneDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string FirmwareVersion { get; set; } = string.Empty;
    }
}
