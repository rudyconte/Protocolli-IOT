using CoAP.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ApplicationCore.Services;
using Protocolli.IoT.Drone.CoAPServer.Resources;
using Protocolli.IoT.Drone.Infrastructure.Data;

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

var server = new CoapServer(int.Parse(configuration.GetSection("CoAP")["port"]));
server.Add(new DroneStatusResource(host.Services));

server.Start();
Console.WriteLine("Coap server started. Press any key to stop it");
Console.ReadLine();
server.Stop();
