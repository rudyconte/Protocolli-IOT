using Protocolli.IoT.Drone.ServerApp.Models;

namespace Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services
{
    public interface IVelocitiesService
    {
        void InsertVelocity(Velocity velocity);
    }
}
