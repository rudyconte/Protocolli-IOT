using Protocolli.IoT.Drone.ServerApp.Models;

namespace Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data
{
    public interface IRepository<TEntity>
        where TEntity : Sensor
    {
        void Insert(TEntity value);
    }
}
