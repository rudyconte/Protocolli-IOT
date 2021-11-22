using Protocolli.IOT.Drone.ClientApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.ClientApp.Models
{
    internal class Velocity : Sensor, ISensor
    {
        private readonly Random _random = new();
        public float Speed { get; set; }

        public string GetJsonMeasure()
        {

            Velocity velocity = new()
            {
                Speed = (float)_random.NextDouble() * 100,
                Timestamp = GetTime()
            };
            return JsonSerializer.Serialize(velocity);
        }
    }
}
