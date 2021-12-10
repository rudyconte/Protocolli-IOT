using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.Telecomando
{
    public class Mqtt
    {
        private readonly IMqttClient _mqttClient;
        private readonly IMqttClientOptions _options;
        private readonly string _url = ConfigurationManager.AppSettings["brokerMQTT"];
        private readonly int _port = int.Parse(ConfigurationManager.AppSettings["portMQTT"]);
        private readonly string _topic = ConfigurationManager.AppSettings["topicMQTTcommands"];

        public Mqtt()
        {
            // Create a new MQTT client.
            //_factory = new MqttFactory();
            _mqttClient = new MqttFactory().CreateMqttClient();

            // Use TCP connection.
            _options = new MqttClientOptionsBuilder()
                .WithTcpServer(_url,_port)
                .WithCleanSession(false) //voglio che drone riceva comandi inviati mentre era offline
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
            .WithTopic($"{_topic}{droneId}")
            .WithPayload(command)
            .WithExactlyOnceQoS() //QOS2 --> voglio che comando venga inviato esattamente 1 volta sola
            .WithRetainFlag(false) //non mi interessa al subscribe ricevere l'ultimo comando inviato
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
