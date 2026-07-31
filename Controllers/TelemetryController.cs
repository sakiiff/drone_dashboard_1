using drone_dashboard_1.DTOs.Telemetry;
using drone_dashboard_1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace drone_dashboard_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelemetryController : ControllerBase
    {
        private readonly ITelemetryService _telemetryService;

        public TelemetryController(ITelemetryService service)
        {
            _telemetryService = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTelemetry(CreateTelemetryDTO dto)
        {
            var telemetry = await _telemetryService.CreateTelemetry(dto);

            if (telemetry == null)
                return NotFound();

            return Created(string.Empty, telemetry);
        }

        [HttpGet("latest/{droneId}")]
        public async Task<ActionResult<TelemetryResponseDTO>> GetLatestTelemetry(int droneId)
        {
            var telemetry = await _telemetryService.GetLatestTelemetry(droneId);

            if (telemetry == null)
                return NotFound();

            return telemetry;
        }

        [HttpGet("history/{flightId}")]
        public async Task<IEnumerable<TelemetryResponseDTO>> GetTelemetryHistory(int flightId, int pageSize, DateTime start, DateTime end)
        {
            var telemetry = await _telemetryService.GetTelemetryHistory(flightId, pageSize, start, end);

            return telemetry;
        }

    }
}
