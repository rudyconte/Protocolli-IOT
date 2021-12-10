using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.ClientApp.Models
{
    internal class DroneStatusMqtt: DroneStatus
    {
        private readonly IMqttClient _mqttClient;
        private readonly string _url = ConfigurationManager.AppSettings["brokerMQTT"];
        private readonly int _port = int.Parse(ConfigurationManager.AppSettings["portMQTT"]);
        private readonly string _topic = ConfigurationManager.AppSettings["topicMQTTcommands"];

        //enable drone status to receive commands via mqtt
        public DroneStatusMqtt()
        {
            // Create a new MQTT client.
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Use TCP connection.
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(_url, _port) // Port is optional
                .WithCleanSession(false) //voglio che drone riceva comandi inviati mentre era offline
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

            //when a message is received save it to influxdb
            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                string Payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                Console.WriteLine($"### RECEIVED COMMAND FOR DRONE {DroneId}: {Payload} ###");
            });

            //when connected subscribe to topic
            _mqttClient.UseConnectedHandler(async e =>
            {
                Console.WriteLine("### CONNECTED WITH SERVER ###");
                await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter($"{_topic}{DroneId}")
                    .Build());

                Console.WriteLine($"### SUBSCRIBED TO TOPIC:  {_topic}{DroneId}###");
            });

            
        }
    }
}
