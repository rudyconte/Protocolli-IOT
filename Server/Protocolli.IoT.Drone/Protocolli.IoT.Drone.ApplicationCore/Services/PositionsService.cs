using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.ApplicationCore.Services
{
    public class PositionsService : IPositionsService
    {
        private readonly IPositionsRepository _positionsRepository;

        public PositionsService(IPositionsRepository positionsRepository)
        {
            _positionsRepository = positionsRepository;
        }

        public void InsertPosition(Position position)
        {
            _positionsRepository.Insert(position);
        }
    }
}
