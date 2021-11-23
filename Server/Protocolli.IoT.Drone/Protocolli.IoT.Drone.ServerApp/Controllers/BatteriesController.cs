using Microsoft.AspNetCore.Mvc;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.ServerApp.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BatteriesController : ControllerBase
    {
        private readonly IBatteriesService _batteriesService;

        public BatteriesController(IBatteriesService batteriesService)
        {
            _batteriesService = batteriesService;
        }

        // POST batteries
        [HttpPost]
        public IActionResult Insert(Battery battery)
        {
            if (ModelState.IsValid)
            {
                _batteriesService.InsertBattery(battery);
                return NoContent(); //204
            }
            return BadRequest(ModelState);
        }
    }
}

