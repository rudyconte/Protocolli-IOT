using CoAP;
using CoAP.Server.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Protocolli.IoT.Drone.CoAPServer.Resources
{
    internal class DroneStatusResource : Resource
    {
        private readonly IDroneStatusService _droneStatusService;
        public DroneStatusResource(IServiceProvider services) : base("dronestatus")
        {
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;
            _droneStatusService = provider.GetRequiredService<IDroneStatusService>();
            
            // set a friendly title
            Attributes.Title = "Post drone status";
        }

        // override this method to handle POST requests
        protected override void DoPost(CoapExchange exchange)
        {
            string Payload = exchange.Request.PayloadString;
            DroneStatus? status = JsonSerializer.Deserialize<DroneStatus>(Payload);

            if (status != null)
            {
                _droneStatusService.InsertDroneStatus(status);
            }
            Console.WriteLine("received post request");
            //send response with status code content
            exchange.Respond(StatusCode.Content, "Received Status");
        }
    }
}
