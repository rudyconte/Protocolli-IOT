using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.ApplicationCore.Services
{
    public class DroneStatusService : IDroneStatusService
    {
        private readonly IDroneStatusRepository _droneStatusRepository;

        public DroneStatusService(IDroneStatusRepository droneStatusRepository)
        {
            _droneStatusRepository = droneStatusRepository;
        }

        public void InsertDroneStatus(DroneStatus droneStatus)
        {
            _droneStatusRepository.Insert(droneStatus);
        }
    }
}
