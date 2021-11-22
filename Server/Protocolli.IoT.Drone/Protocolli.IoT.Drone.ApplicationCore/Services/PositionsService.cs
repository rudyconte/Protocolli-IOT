using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
