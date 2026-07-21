using drone_dashboard_1.DTOs.Drones;

namespace drone_dashboard_1.Services.Interfaces
{
    public interface IDroneService
    {
        Task<IEnumerable<DroneResponseDTO>> GetDrones();
        Task<DroneResponseDTO?> GetDronesById(int Id);
        Task<DroneResponseDTO> CreateDrone(CreateDroneDTO dto);
        Task<DroneResponseDTO?> UpdateDrone(int Id, UpdateDroneDTO dto);
        Task<bool> DeleteDrone(int Id);
    }
}
