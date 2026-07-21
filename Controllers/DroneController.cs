using drone_dashboard_1.DTOs.Drones;
using drone_dashboard_1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace drone_dashboard_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DroneController : ControllerBase
    {
        private readonly IDroneService _droneService;

        public DroneController(IDroneService service)
        {
            _droneService = service;
        }

        //Fetch current all active drones
        [HttpGet]
        public async Task<IActionResult> GetDrones()
        {
            var drones = await _droneService.GetDrones();
            return Ok(drones);
        }

        //Fetch drones by Id
        [HttpGet("{Id}")]
        public async Task<ActionResult<DroneResponseDTO>> GetDronesById(int Id)
        {
            var dto = await _droneService.GetDronesById(Id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        //Create new drone
        [HttpPost]
        public async Task<ActionResult<DroneResponseDTO>> CreateDrone(CreateDroneDTO dto)
        {
            var created = await _droneService.CreateDrone(dto);
            return CreatedAtAction(nameof(GetDronesById), new { Id = created.Id }, created);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<DroneResponseDTO>> UpdateDrone(int Id, UpdateDroneDTO dto)
        {
            var updated = await _droneService.UpdateDrone(Id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteDrone(int Id)
        {
            var ok = await _droneService.DeleteDrone(Id);
            if (!ok) return NotFound();
            return NoContent();
        }

    }   

}
