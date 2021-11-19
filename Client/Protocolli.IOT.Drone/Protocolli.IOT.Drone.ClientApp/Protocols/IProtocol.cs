using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.ClientApp.Protocols
{
    internal interface IProtocol
    {
        public async Task SendAsync(string data, string path);
    }
}
