using Microsoft.AspNetCore.Mvc;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.ServerApp.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
	[ApiController]
	public class VelocitiesController : ControllerBase
	{
		private readonly IVelocitiesService _velocitiesService;

		public VelocitiesController(IVelocitiesService velocitiesService)
		{
			_velocitiesService = velocitiesService;
		}

        // POST positions
        [HttpPost]
        public IActionResult Insert(Velocity velocity)
        {
            if (ModelState.IsValid)
            {
                _velocitiesService.InsertVelocity(velocity);
                return NoContent(); //204
            }
            return BadRequest(ModelState);
        }
    }
}

