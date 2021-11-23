using Microsoft.AspNetCore.Mvc;
using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.ServerApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VelocityController : ControllerBase
	{
		private readonly DroneService _droneService;

		public VelocityController(DroneService droneService)
		{
			_droneService = droneService;
		}

		// POST api/<BatteryController>
		[HttpPost]
		public ActionResult<Velocity> Create(Velocity velocity)
		{

		}

	}
}

