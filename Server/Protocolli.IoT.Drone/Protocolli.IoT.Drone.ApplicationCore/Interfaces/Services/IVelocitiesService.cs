using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services
{
    public interface IVelocitiesService
    {
        void InsertVelocity(Velocity velocity);
    }
}
