using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using Protocolli.IOT.Drone.ClientApp.Interfaces;
using Protocolli.IOT.Drone.ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private readonly string _url = ConfigurationManager.AppSettings["brokerMQTT"];
        private readonly int _port = int.Parse(ConfigurationManager.AppSettings["portMQTT"]);
        private readonly string _topic = ConfigurationManager.AppSettings["topicMQTT"];

        public Mqtt()
        {
            // Create a new MQTT client.
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Use TCP connection.
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(_url, _port) // Port is optional
                .WithCleanSession(true) //per non riempire broker di messaggi 
                .Build();

            //connect to broker
            _mqttClient.ConnectAsync(options, CancellationToken.None);

            //try to riconnect if disconnected
            _mqttClient.UseDisconnectedHandler(async e =>
            {
                Console.WriteLine("### DISCONNECTED FROM SERVER ###");
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    await _mqttClient.ConnectAsync(options, CancellationToken.None); // Since 3.0.5 with CancellationToken
                }
                catch
                {
                    Console.WriteLine("### RECONNECTING FAILED ###");
                }
            });
        }

        public async Task SendAsync(IDroneStatus status)
        {
            int id = status.GetDroneId();
            string data = status.SimulateDeviceStatus();

            //create message to publish
            var message = new MqttApplicationMessageBuilder()
            .WithTopic($"{_topic}{id}")
            .WithPayload(data)
            .WithRetainFlag(true) //voglio mantenere ultimi stati per popolare dashboard
            .WithAtMostOnceQoS() //QOS 0 
            .Build();

            //publish message
            try 
            {
                await _mqttClient.PublishAsync(message, CancellationToken.None);

                Console.WriteLine($"Published Drone Status to topic: {_topic}{id}");
            }
            catch (MQTTnet.Exceptions.MqttCommunicationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
       
    }
}
