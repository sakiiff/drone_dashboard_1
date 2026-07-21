using drone_dashboard_1.DTOs.Flights;

namespace drone_dashboard_1.Services.Interfaces
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightResponseDTO>> GetFlights();
        Task<FlightResponseDTO?> GetFlightById(int id);
        Task<FlightResponseDTO> CreateFlight(CreateFlightDTO dto);
        Task<bool> EndFlight(int id, EndFlightDTO dto);
    }
}