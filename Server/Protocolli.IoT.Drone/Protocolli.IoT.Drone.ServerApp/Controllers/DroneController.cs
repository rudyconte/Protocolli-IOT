using Microsoft.AspNetCore.Mvc;

namespace Protocolli.IoT.Drone.ServerApp.Controllers
{
	public class DroneController
	{
		[Route("api/[controller]")]
		[ApiController]
		public class ProductController : ControllerBase
		{
			private readonly IProductService _productService;

			public ProductController(IProductService productService)
			{
				_productService = productService;
			}

		}
}
