using Protocolli.IoT.Drone.ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data
{
    public interface IRepository<TEntity>
        where TEntity : Sensor
    {
        void Insert(TEntity value);
    }
}
