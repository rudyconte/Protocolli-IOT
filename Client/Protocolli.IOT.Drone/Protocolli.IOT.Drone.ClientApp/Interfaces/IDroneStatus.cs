﻿using Protocolli.IOT.Drone.ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.ClientApp.Interfaces
{
    internal interface IDroneStatus
    {
        public DroneStatus SimulateDeviceStatus();
    }
}
