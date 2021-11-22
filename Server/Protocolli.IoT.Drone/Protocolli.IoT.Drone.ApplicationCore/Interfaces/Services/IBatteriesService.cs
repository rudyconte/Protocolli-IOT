using Protocolli.IoT.Drone.ServerApp.Models;

namespace Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services
{
    public interface IBatteriesService
    {
        void InsertBattery(Battery battery);
    }
}
