using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using Microsoft.Extensions.Configuration;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data;
using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.Infrastructure.Data
{
    public class VelocitiesRepository : IVelocitiesRepository
    {
        private readonly string _url;
        private readonly string _token;
        private readonly string _bucket;
        private readonly string _organization;

        public VelocitiesRepository(IConfiguration configuration)
        {
            _bucket = configuration.GetSection("InfluxDB")["bucket"];
            _organization = configuration.GetSection("InfluxDB")["organization"];
            _token = configuration.GetSection("InfluxDB")["token"];
            _url = configuration.GetSection("InfluxDB")["url"];
        }

        public void Insert(Velocity velocity)
        {
            var influxDBClient = InfluxDBClientFactory.Create(_url, _token.ToCharArray());

            using (var writeApi = influxDBClient.GetWriteApi())
            {
                var point = PointData
                    .Measurement("velocity")
                    .Field("speed", velocity.Speed)
                    .Timestamp(velocity.Timestamp, WritePrecision.S);

                writeApi.WritePoint(_bucket, _organization, point);
            }
        }
    }
}