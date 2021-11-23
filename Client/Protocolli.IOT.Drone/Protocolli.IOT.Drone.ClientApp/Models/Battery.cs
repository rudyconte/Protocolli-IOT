using Protocolli.IOT.Drone.ClientApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.ClientApp.Models
{
    internal class Battery : Sensor, ISensor
    {
        private readonly Random _random = new();
        public float Level { get; set; }

        public string GetJsonMeasure()
        {
            Level = _random.Next(0, 100);
            Timestamp = GetTime();
            
            return JsonSerializer.Serialize(this);
        }
    }
}
