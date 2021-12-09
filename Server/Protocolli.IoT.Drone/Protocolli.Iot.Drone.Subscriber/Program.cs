using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Protocolli.Iot.Drone.Subscriber;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ApplicationCore.Services;
using Protocolli.IoT.Drone.Infrastructure.Data;

//use configuration file
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

//connect to mqtt broker and subscribe to topic
Mqtt mqtt = new Mqtt(configuration);
mqtt.ManageMessages(host.Services);
mqtt.Connect();
mqtt.SubscribeToTopic(configuration);


Console.ReadLine(); 

