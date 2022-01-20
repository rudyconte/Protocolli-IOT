using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocolli.IOT.Drone.ClientApp.Interfaces;
using Protocolli.IOT.Drone.ClientApp.Models;
using System.Text.Json;

namespace Protocolli.IOT.Drone.ClientApp.Protocols
{
    public class AmqpConsumer
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly int _routingKey;
        private readonly Interfaces.IProtocol _sender;
        public AmqpConsumer(int id)
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _routingKey = id;
            _sender = new Mqtt();

            _channel.QueueDeclare(queue: _routingKey.ToString(),
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                IDroneStatus status = JsonSerializer.Deserialize<DroneStatus>(message);
                await _sender.SendAsync(status);
                Console.WriteLine($"Sent message for drone {status.GetDroneId()}");
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            
            _channel.BasicConsume(queue: _routingKey.ToString(),
                                 autoAck: false,
                                 consumer: consumer);
        }
    }
}
