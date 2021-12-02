using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
        private readonly IDroneStatusService _droneStatusService;
        private readonly IMqttClient _mqttClient;
        public Mqtt(IDroneStatusService droneStatusService)
        {
            _droneStatusService = droneStatusService;
            // Create a new MQTT client.
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Use TCP connection.
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("127.0.0.1", 1883) // Port is optional
                .Build();

            _mqttClient.ConnectAsync(options, CancellationToken.None);
            _mqttClient.UseConnectedHandler(async e =>
            {
                Console.WriteLine("### CONNECTED WITH SERVER ###");

                // Subscribe to a topic
                await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder().WithTopicFilter("protocolliIOT/stato/#").Build());

                Console.WriteLine("### SUBSCRIBED ###");
            });
            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                string Payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                DroneStatus status = JsonSerializer.Deserialize<DroneStatus>(Payload);
            });


        }


    }
}
