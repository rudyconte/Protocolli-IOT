using System;
using CoAP.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Protocolli.IoT.Drone.CoAP.Server.Resources;

namespace Protocolli.IoT.Drone.CoAP.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            //register dependency injection
            using IHost host = Host.CreateDefaultBuilder(args)
                                   .ConfigureServices((_, services) =>
                                   services.AddScoped<IDroneStatusService, DroneStatusService>()
                                           .AddScoped<IDroneStatusRepository, DroneStatusRepository>()
                                           .AddSingleton<IConfigurationRoot>(configuration))
                                   .Build();

            var server = new CoapServer(5683);
            server.Add(new DroneStatusResource());

            server.Start();
            Console.WriteLine("Coap server started. Press any key to stop it");
            Console.ReadLine();
            server.Stop();
        }
    }
}
