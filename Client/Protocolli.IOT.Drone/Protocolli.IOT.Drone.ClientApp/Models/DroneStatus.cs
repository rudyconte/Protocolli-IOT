using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using Protocolli.IOT.Drone.ClientApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.ClientApp.Models
{
    internal class DroneStatus : IDroneStatus
    {
        private readonly Random _random = new();
        private readonly Position _position = new();
        private readonly IMqttClient _mqttClient;
        private readonly string _url = ConfigurationManager.AppSettings["brokerMQTT"];
        private readonly int _port = int.Parse(ConfigurationManager.AppSettings["portMQTT"]);

        public int DroneId { get; set; }
        public Position Position { get; set; }
        public float Velocity { get; set; }
        public float Battery { get; set; }
        public long Timestamp { get; set; }

        public DroneStatus SimulateDeviceStatus()
        {
            Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

            Velocity = (float)_random.NextDouble() * 200;
            Battery = _random.Next(0, 100);
            Position = _position.SimulatePosition();

            return this;
        }

        public DroneStatus()
        {
            // Create a new MQTT client.
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Use TCP connection.
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(_url, _port) // Port is optional
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

        //subscribe to topic for receiving commands
        public void SubscribeToTopic()
        {
            int id = this.DroneId;
            //when connected subscribe to topic
            _mqttClient.UseConnectedHandler(async e =>
            {
                Console.WriteLine("### CONNECTED WITH SERVER ###");
                await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter($"protocolliIOT/comando/drone{id}")
                    .Build());

                Console.WriteLine($"### SUBSCRIBED TO TOPIC:  protocolliIOT/comando/drone{id}###");
            });
        }

        //Manage received commands
        public void ManageMessages()
        {


            //when a message is received save it to influxdb
            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                

                string Payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                Console.WriteLine($"### RECEIVED COMMAND FOR DRONE {this.DroneId}: {Payload} ###");


            });
        }
    }
}
