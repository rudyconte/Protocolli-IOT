using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.ClientApp.Models
{
    internal class Sensor
    {
        public long Timestamp { get; set; }
        public int DroneId { get; set; }

        public long GetTime()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}
