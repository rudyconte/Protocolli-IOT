using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ApplicationCore.Models;

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
