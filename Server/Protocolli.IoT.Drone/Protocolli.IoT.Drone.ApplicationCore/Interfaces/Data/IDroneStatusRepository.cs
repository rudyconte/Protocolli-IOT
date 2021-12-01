using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data
{
    public interface IDroneStatusRepository
    {
        void Insert(DroneStatus status);
        DroneStatus GetLastById(int droneId);
        IEnumerable<DroneStatus> GetAllById(int droneId);
        IEnumerable<DroneStatus> GetAll();
    }
}
