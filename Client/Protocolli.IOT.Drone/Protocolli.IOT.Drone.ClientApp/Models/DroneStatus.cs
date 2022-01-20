using Protocolli.IOT.Drone.ClientApp.Interfaces;
using Protocolli.IOT.Drone.ClientApp.Protocols;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace Protocolli.IOT.Drone.ClientApp.Models
{
    internal class DroneStatus : IDroneStatus
    {
        private readonly Random _random = new();
        private readonly Position _position = new();
        private readonly AmqpEnqueuer _enqueuer;

        public int DroneId { get; set; }
        public Position Position { get; set; }
        public float Velocity { get; set; }
        public float Battery { get; set; }
        public long Timestamp { get; set; }

        public DroneStatus()
        {

        }

        //connect to rabbit and create queue with name = DroneId
        public DroneStatus(int id)
        {
            DroneId = id;
            _enqueuer = new(id);

        }

        public int GetDroneId()
        {
            return DroneId;
        }

        public string SimulateDeviceStatus()
        {
            Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

            Velocity = (float)_random.NextDouble() * 200;
            Battery = _random.Next(0, 100);
            Position = _position.SimulatePosition();

            string status = JsonSerializer.Serialize(this);

            return status;
        }

        //simulate status and add it to the queue
        public void EnqueueStatus()
        {
            string status = this.SimulateDeviceStatus();
            _enqueuer.Enqueue(status);
        }
    }
}
