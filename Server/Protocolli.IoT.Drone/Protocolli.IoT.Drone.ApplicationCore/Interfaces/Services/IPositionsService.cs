using Protocolli.IoT.Drone.ServerApp.Models;

namespace Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services
{
    public interface IPositionsService
    {
        void InsertPosition(Position position);
    }
}
