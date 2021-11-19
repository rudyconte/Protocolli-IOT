using Microsoft.AspNetCore.Mvc;
using Protocolli.IoT.Drone.ServerApp.Models;

namespace Protocolli.IoT.Drone.ServerApp.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class BatteryController:ControllerBase
	{
		private readonly DroneService _droneService;

			public BatteryController(DroneService droneService)
			{
				_droneService = droneService;
			}

			// POST api/<BatteryController>
			[HttpPost]
			public ActionResult<Battery> Create(Battery battery)
			{

			}

		}
	}

