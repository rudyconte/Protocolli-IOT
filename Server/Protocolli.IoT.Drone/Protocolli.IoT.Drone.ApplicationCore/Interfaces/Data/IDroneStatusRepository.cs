using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data
{
    public interface IDroneStatusRepository
    {
        void Insert(DroneStatus value);
    }
}
