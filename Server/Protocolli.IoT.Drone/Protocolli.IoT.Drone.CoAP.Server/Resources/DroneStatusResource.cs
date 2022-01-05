using CoAP;
using CoAP.Server.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolli.IoT.Drone.CoAP.Server.Resources
{
    internal class DroneStatusResource : Resource
    {
        public DroneStatusResource() : base("dronestatus")
        {
            // set a friendly title
            Attributes.Title = "Save drone status";
        }

        // override this method to handle POST requests
        protected override void DoPost(CoapExchange exchange)
        {
            Console.WriteLine(exchange.Request.PayloadString);
            Console.WriteLine("Received post request");
            //send response with status code content
            exchange.Respond(StatusCode.Content, "Received Status");
        }
    }
}
