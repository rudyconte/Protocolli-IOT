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
    public class VelocitiesService : IVelocitiesService
    {

        private readonly IVelocitiesRepository _velocitiesRepository;

        public VelocitiesService(IVelocitiesRepository velocitiesRepository)
        {
            _velocitiesRepository = velocitiesRepository;
        }

        public void InsertVelocity(Velocity velocity)
        {
            _velocitiesRepository.Insert(velocity);
        }
    }
}
