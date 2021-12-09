using System;
using System.Threading;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.Telecomando
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string[] commands = { "Uscire", "Apri nuova corsa", "Chiusdi corsa corrente",
            "Accensione", "Spegnimento", "Torna alla base", "Accendi luci"};
            Mqtt mqtt = new Mqtt();
            mqtt.Connect();
            int droneId;
            int command = 99;
            while (command != 0){
                Console.WriteLine("Che comando vuoi inviare?");
                Console.WriteLine("[0] per uscire");
                Console.WriteLine("[1] per apertura nuova corsa");
                Console.WriteLine("[2] per chiusura corsa corrente");
                Console.WriteLine("[3] per accensione");
                Console.WriteLine("[4] per spegnimento");
                Console.WriteLine("[5] per rientro alla base");
                Console.WriteLine("[6] per accensione luci di natale");
                command = int.Parse(Console.ReadLine());

                Console.WriteLine("Inserisci Id del drone a cui inviare il comando");
                droneId = int.Parse(Console.ReadLine());

                await mqtt.SendAsync(commands[command], droneId);
                Thread.Sleep(5000);
            }
        }

        

    }
}
