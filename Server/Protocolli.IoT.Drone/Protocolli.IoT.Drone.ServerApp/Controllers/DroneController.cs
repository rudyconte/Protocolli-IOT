using Microsoft.AspNetCore.Mvc;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ServerApp.Models;

namespace Protocolli.IoT.Drone.ServerApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BatteriesController : ControllerBase
    {
        private readonly IBatteriesService _batteriesService;

        public BatteriesController(IBatteriesService batteriesService)
        {
            _batteriesService = batteriesService;
        }

        [HttpPost]
        public IActionResult Insert(Battery battery)
        {
            _batteriesService.InsertBattery(battery);
            return NoContent(); //204
        }
    }
}
