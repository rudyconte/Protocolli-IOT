using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Services;
using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.Iot.Drone.Subscriber
{
    public class Mqtt
    {
        private readonly IMqttClient _mqttClient;
        //private readonly MqttFactory _factory;
        private readonly IMqttClientOptions _options;

        public Mqtt(IConfiguration configuration)
        {
            // Create a new MQTT client.
            //_factory = new MqttFactory();
            _mqttClient = new MqttFactory().CreateMqttClient();

            // Use TCP connection.
            _options = new MqttClientOptionsBuilder()
                .WithTcpServer(configuration
                .GetSection("MQTT")["brokerAddress"], int.Parse(configuration.GetSection("MQTT")["port"]))
                .Build();
        }

        //connect to broker
        public void Connect()
        {
            //open connection with broker
            _mqttClient.ConnectAsync(_options, CancellationToken.None);

            //try to recconect when disconnected
            _mqttClient.UseDisconnectedHandler(async e =>
            {
                Console.WriteLine("### DISCONNECTED FROM SERVER ###");
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    await _mqttClient.ConnectAsync(_options, CancellationToken.None); // Since 3.0.5 with CancellationToken
                }
                catch
                {
                    Console.WriteLine("### RECONNECTING FAILED ###");
                }
            });
        }

        public void SubscribeToTopic(IConfiguration configuration)
        {
            //when connected subscribe to topic
            _mqttClient.UseConnectedHandler(async e =>
            {
                Console.WriteLine("### CONNECTED WITH SERVER ###");
                await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter(configuration.GetSection("MQTT")["topic"])
                    .Build());

                Console.WriteLine($"### SUBSCRIBED TO TOPIC:  {configuration.GetSection("MQTT")["topic"]}###");
            });
        }

        public void ManageMessages(IServiceProvider services)
        {
            //get dronestatus service via dependency injection
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;
            var droneStatusService = provider.GetRequiredService<IDroneStatusService>();

            //when a message is received save it to influxdb
            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");

                string Payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
               
                DroneStatus? status = JsonSerializer.Deserialize<DroneStatus>(Payload);

                if (status != null)
                {
                    droneStatusService.InsertDroneStatus(status);
                }
                    
            });
        }
    }
}
