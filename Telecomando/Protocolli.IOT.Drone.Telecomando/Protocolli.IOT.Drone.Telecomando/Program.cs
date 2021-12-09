using System;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.Telecomando
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Mqtt mqtt = new Mqtt();
            mqtt.Connect();
            while (true){
                await mqtt.SendAsync("Ti puzza il culo drone 0", 0);
            }
        }

        

    }
}
