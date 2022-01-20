using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolli.IOT.Drone.ClientApp.Protocols
{
    internal class AmqpEnqueuer
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly int _routingKey;

        //connect and declare queue
        public AmqpEnqueuer(int id)
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _routingKey = id;


            _channel.QueueDeclare(queue: _routingKey.ToString(),
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        //add status to queue
        public void Enqueue(string status)
        {

            //publish status to rabbitMQ
            var body = Encoding.UTF8.GetBytes(status);

            _channel.BasicPublish(exchange: "",
                                 routingKey: _routingKey.ToString(),
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($"Published to rabbit status of drone {_routingKey}");
        }
    }
}
