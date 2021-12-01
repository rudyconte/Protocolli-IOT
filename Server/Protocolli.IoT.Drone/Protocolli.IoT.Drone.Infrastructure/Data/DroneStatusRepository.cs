using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using Microsoft.Extensions.Configuration;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data;
using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.Infrastructure.Data
{
    public class DroneStatusRepository : IDroneStatusRepository
    {
        private readonly string _url;
        private readonly string _token;
        private readonly string _bucket;
        private readonly string _organization;

        public DroneStatusRepository(IConfiguration configuration)
        {
            _bucket = configuration.GetSection("InfluxDB")["bucket"];
            _organization = configuration.GetSection("InfluxDB")["organization"];
            _token = configuration.GetSection("InfluxDB")["token"];
            _url = configuration.GetSection("InfluxDB")["url"];
        }

        public void Insert(DroneStatus droneStatus)
        {
            var influxDBClient = InfluxDBClientFactory.Create(_url, _token.ToCharArray());

            using (var writeApi = influxDBClient.GetWriteApi())
            {
                var point = PointData
                    .Measurement("drones_status")
                    .Tag("drone_id", $"drone{droneStatus.DroneId}")
                    .Field("battery_level", droneStatus.Battery)
                    .Field("velocity_speed", droneStatus.Velocity)
                    .Field("position_x", droneStatus.Position.X)
                    .Field("position_y", droneStatus.Position.Y)
                    .Field("position_z", droneStatus.Position.Z)
                    .Timestamp(droneStatus.Timestamp, WritePrecision.S);

                writeApi.WritePoint(_bucket, _organization, point);
            }
        }

        public IEnumerable<DroneStatus> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneStatus> GetAllById(int droneId)
        {
            throw new NotImplementedException();
        }

        public DroneStatus GetLastById(int droneId)
        {
            throw new NotImplementedException();
        }
    }
}
