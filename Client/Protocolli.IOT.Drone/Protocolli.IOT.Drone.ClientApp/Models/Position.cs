using Protocolli.IOT.Drone.ClientApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.ClientApp.Models
{
    internal class Position : Sensor, ISensor
    {
        private readonly Random _random = new();
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public string GetJsonMeasure()
        {
            Position position = new()
            {
                X = _random.NextDouble() * 10000,
                Y = _random.NextDouble() * 10000,
                Z = _random.NextDouble() * 800,
                Timestamp = GetTime()
            };
            return JsonSerializer.Serialize(position);
        }
    }
}
