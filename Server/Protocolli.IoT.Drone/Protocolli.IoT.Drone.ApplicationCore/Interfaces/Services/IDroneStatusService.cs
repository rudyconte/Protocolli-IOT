using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services
{
    public interface IDroneStatusService
    {
        void InsertDroneStatus(DroneStatus droneStatus);
    }
}
