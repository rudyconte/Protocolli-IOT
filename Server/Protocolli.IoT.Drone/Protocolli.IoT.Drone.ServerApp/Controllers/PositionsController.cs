using Microsoft.AspNetCore.Mvc;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.ServerApp.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionsService _positionsService;

        public PositionsController(IPositionsService positionsService)
        {
            _positionsService = positionsService;
        }

        // POST positions
        [HttpPost]
        public IActionResult Insert(Position position)
        {
            if (ModelState.IsValid)
            {
                _positionsService.InsertPosition(position);
                return NoContent(); //204
            }
            return BadRequest(ModelState);
        }
    }
}

