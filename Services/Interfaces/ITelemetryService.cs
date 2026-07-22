using drone_dashboard_1.DTOs.Telemetry;

namespace drone_dashboard_1.Services.Interfaces
{
    public interface ITelemetryService
    {
        Task<TelemetryResponseDTO> CreateTelemetry(CreateTelemetryDTO dto);
        Task<TelemetryResponseDTO?> GetLatestTelemetry(int droneId);
        Task<IEnumerable<TelemetryResponseDTO>> GetTelemetryHistory(int flightId, int pageSize, DateTime start, DateTime end);

    }
}
