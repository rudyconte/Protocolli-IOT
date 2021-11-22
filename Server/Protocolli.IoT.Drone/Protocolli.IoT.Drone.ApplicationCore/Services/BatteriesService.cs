using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolli.IoT.Drone.ApplicationCore.Services
{
    public class BatteriesService : IBatteriesService
    {
        private readonly IBatteriesRepository _batteriesRepository;

        public BatteriesService(IBatteriesRepository batteriesRepository)
        {
            _batteriesRepository = batteriesRepository;
        }

        public void InsertBattery(Battery battery)
        {
            _batteriesRepository.Insert(battery);
        }
    }
}
