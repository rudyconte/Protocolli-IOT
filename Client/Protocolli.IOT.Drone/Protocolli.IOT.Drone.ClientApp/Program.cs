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

            //set here the base url of the API
            string baseUrl = "http://localhost:3333";

            Console.WriteLine("Quanti droni vuoi simulare?");
            int dronesNumber = int.Parse(Console.ReadLine());

            for (int i = 0; i < dronesNumber; i++)
            {
                //sensor index must match with its route index
                sensors.Add(new Battery() { DroneId = i });
                routes.Add(new Http(baseUrl + "/battery"));

                sensors.Add(new Position() { DroneId = i });
                routes.Add(new Http(baseUrl + "/positions"));

                sensors.Add(new Velocity() { DroneId = i });
                routes.Add(new Http(baseUrl + "/velocities"));
            }

           

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
