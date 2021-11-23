using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.ApplicationCore.Services
{
    public class BatteriesService : IBatteriesService
    {
        private readonly IBatteriesRepository _batteriesRepository;

        public BatteriesService(IBatteriesRepository batteriesRepository)
        {
            _batteriesRepository = batteriesRepository;
        }

        public void InsertBattery(Battery battery)
        {
            _batteriesRepository.Insert(battery);
        }
    }
}
