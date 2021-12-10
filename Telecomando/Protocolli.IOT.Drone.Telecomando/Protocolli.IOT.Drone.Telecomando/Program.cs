using System;
using System.Threading;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.Telecomando
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //connect to broker
            Mqtt mqtt = new Mqtt();
            mqtt.Connect();

            //array containing commands
            string[] commands = { "Uscire", "Apri nuova corsa", "Chiudi corsa corrente",
            "Accensione", "Spegnimento", "Torna alla base", "Accendi luci"};

            //ask user the commands to send
            int droneId;
            int command = 99;

            while (command != 0){

                Console.WriteLine("Che comando vuoi inviare?");

                for (int i = 0; i < commands.Length; i++)
                {
                    Console.WriteLine($"[{i}] per {commands[i]}");
                }

                command = int.Parse(Console.ReadLine());

                Console.WriteLine("Inserisci Id del drone a cui inviare il comando");
                droneId = int.Parse(Console.ReadLine());

                //send command
                await mqtt.SendAsync(commands[command], droneId);
            }
        }

        

    }
}
