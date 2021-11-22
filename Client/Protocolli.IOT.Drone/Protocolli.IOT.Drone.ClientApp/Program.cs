using Protocolli.IOT.Drone.ClientApp.Interfaces;
using Protocolli.IOT.Drone.ClientApp.Models;
using Protocolli.IOT.Drone.ClientApp.Protocols;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.ClientApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            List<ISensor> sensors = new();
            List<IProtocol> routes = new();

            //sensor index must match with its route index
            sensors.Add(new Battery());
            routes.Add(new Http("http://localhost:3333/battery"));

            sensors.Add(new Position());
            routes.Add(new Http("http://localhost:3333/positions"));

            sensors.Add(new Velocity());
            routes.Add(new Http("http://localhost:3333/velocities"));

            while (true)
            {
                for (int i = 0; i < sensors.Count; i++)
                {
                    await routes[i].SendAsync(sensors[i].GetJsonMeasure());
                }

                Thread.Sleep(2000);

            }
            
        }
    }
}
