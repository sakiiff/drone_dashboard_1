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

        public DroneController(IDroneService droneService)
        {
            _droneService = droneService;
        }

        //Fetch current all active drones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DroneResponseDTO>>> GetDrones()
        {
            var drones = await _droneService.GetDrones();
            return Ok(drones);
        }

        //Fetch drones by Id
        [HttpGet("{Id}")]
        public async Task<ActionResult<DroneResponseDTO>> GetDroneById(int Id)
        {
            var dto = await _droneService.GetDroneById(Id);
            if (dto == null) 
                return NotFound();

            return Ok(dto);
        }

        //Create new drone
        [HttpPost]
        public async Task<ActionResult<DroneResponseDTO>> CreateDrone(CreateDroneDTO dto)
        {
            var created = await _droneService.CreateDrone(dto);
            return CreatedAtAction(nameof(GetDroneById), new { Id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DroneResponseDTO>> UpdateDrone(int id, UpdateDroneDTO dto)
        {
            var updated = await _droneService.UpdateDrone(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrone(int id)
        {
            var ok = await _droneService.DeleteDrone(id);
            if (!ok) return NotFound();
            return NoContent();
        }

    }   

}
