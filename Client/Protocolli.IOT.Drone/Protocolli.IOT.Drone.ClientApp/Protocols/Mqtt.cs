using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Protocolli.IOT.Drone.ClientApp.Interfaces;
using Protocolli.IOT.Drone.ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.ClientApp.Protocols
{
    internal class Mqtt : IProtocol
    {
        private readonly IMqttClient _mqttClient;
        
        public Mqtt()
        {
            // Create a new MQTT client.
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Use TCP connection.
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("127.0.0.1", 1883) // Port is optional
                .Build();

            _mqttClient.ConnectAsync(options, CancellationToken.None);
        }

        public async Task SendAsync(DroneStatus status)
        {
            int id = status.DroneId;
            string data = JsonSerializer.Serialize(status);
            var message = new MqttApplicationMessageBuilder()
            .WithTopic($"protocolliIOT/stato/drone{id}")
            .WithPayload(data)
            .WithExactlyOnceQoS()
            .WithRetainFlag()
            .Build();

            await _mqttClient.PublishAsync(message, CancellationToken.None);
        }

    }
}
