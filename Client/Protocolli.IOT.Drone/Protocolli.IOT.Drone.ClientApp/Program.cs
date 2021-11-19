using Protocolli.IOT.Drone.ClientApp.Models;
using Protocolli.IOT.Drone.ClientApp.Protocols;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.ClientApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Battery battery = new();
            Position position = new();
            Velocity velocity = new();

            Http sender = new();

            while (true)
            {
                await sender.SendAsync(battery.GetJsonMeasure(),"/battery");
                await sender.SendAsync(position.GetJsonMeasure(),"/position");
                await sender.SendAsync(velocity.GetJsonMeasure(),"/velocity");

                Thread.Sleep(1000);

            }
            
        }
    }
}
