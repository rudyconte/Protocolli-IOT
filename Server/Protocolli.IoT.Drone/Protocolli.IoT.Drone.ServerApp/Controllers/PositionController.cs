using Microsoft.AspNetCore.Mvc;
using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.ServerApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PositionController : ControllerBase
	{
		private readonly DroneService _droneService;

		public PositionController(DroneService droneService)
		{
			_droneService = droneService;
		}

		// POST api/<BatteryController>
		[HttpPost]
		public ActionResult<Position> Create(Position position)
		{

		}

	}
}

