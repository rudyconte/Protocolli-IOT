using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.Telecomando
{
    public class Mqtt
    {
        private readonly IMqttClient _mqttClient;
        //private readonly MqttFactory _factory;
        private readonly IMqttClientOptions _options;

        public Mqtt()
        {
            // Create a new MQTT client.
            //_factory = new MqttFactory();
            _mqttClient = new MqttFactory().CreateMqttClient();

            // Use TCP connection.
            _options = new MqttClientOptionsBuilder()
                .WithTcpServer("127.0.0.1",1883)
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

        public async Task SendAsync (string command, int droneId)
        {

            //create message to publish
            var message = new MqttApplicationMessageBuilder()
            .WithTopic($"protocolliIOT/comando/drone{droneId}")
            .WithPayload(command)
            .WithExactlyOnceQoS()
            //.WithRetainFlag()
            .Build();

            //publish message
            try
            {
                await _mqttClient.PublishAsync(message, CancellationToken.None);

                Console.WriteLine($"Sent command");
            }
            catch (MQTTnet.Exceptions.MqttCommunicationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
