using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Protocolli.Iot.Drone.Subscriber;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ApplicationCore.Services;
using Protocolli.IoT.Drone.Infrastructure.Data;

using IHost host = Host.CreateDefaultBuilder(args)
                       .ConfigureServices((_, services) =>
               services.AddScoped<IDroneStatusService, DroneStatusService>()
                       .AddScoped<IDroneStatusRepository, DroneStatusRepository>())
                       .Build();

Mqtt mqtt = new Mqtt(); 

Console.ReadLine(); 

