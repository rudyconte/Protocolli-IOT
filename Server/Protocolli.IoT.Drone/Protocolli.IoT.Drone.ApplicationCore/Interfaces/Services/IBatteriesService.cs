using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services
{
    public interface IBatteriesService
    {
        void InsertBattery(Battery battery);
    }
}
