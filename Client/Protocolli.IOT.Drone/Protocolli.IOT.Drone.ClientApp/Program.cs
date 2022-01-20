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
        static void Main(string[] args)
        {
            List<IDroneStatus> devices = new();
            List<AmqpConsumer> senders = new();

            Console.WriteLine("Quanti droni vuoi simulare?");
            int dronesNumber = int.Parse(Console.ReadLine());

           
            for (int i = 0; i < dronesNumber; i++)
            {
                devices.Add(new DroneStatus(i));
                senders.Add(new AmqpConsumer(i)); //consume queues for each drone
            }

      
            while (true)
            {
                for (int i = 0; i < devices.Count; i++)
                {
                    devices[i].EnqueueStatus();
                }

                Thread.Sleep(2000);

            }
            
        }
    }
}
