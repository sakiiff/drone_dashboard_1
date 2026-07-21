using drone_dashboard_1.Data;
using drone_dashboard_1.DTOs.Flights;
using drone_dashboard_1.Models;
using drone_dashboard_1.Models.Enums;
using drone_dashboard_1.Services;
using drone_dashboard_1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace drone_dashboard_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;
        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlights()
        {
            var flights = await _flightService.GetFlights();

            return Ok(flights);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlightById(int id)
        {
            var flight = await _flightService.GetFlightById(id);

            if (flight == null)
                return NotFound();

            return Ok(flight);
        }

        [HttpPost("start")]
        public async Task<IActionResult> CreateFlight(CreateFlightDTO dto)
        {
            var flight = await _flightService.CreateFlight(dto);

            if (flight == null)
                return NotFound();

            return Ok(flight);
        }

        [HttpPost("{id}/end")]
        public async Task<IActionResult> EndFlight(int id, EndFlightDTO dto)
        {
            var result = await _flightService.EndFlight(id, dto);

            if (!result)
                return NotFound();

            return Ok(result);
        }
    }
}
