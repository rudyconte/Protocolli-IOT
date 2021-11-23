using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using Microsoft.Extensions.Configuration;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data;
using Protocolli.IoT.Drone.ApplicationCore.Models;

namespace Protocolli.IoT.Drone.Infrastructure.Data
{
    public class PositionsRepository : IPositionsRepository
    {
        private readonly string _url;
        private readonly string _token;
        private readonly string _bucket;
        private readonly string _organization;

        public PositionsRepository(IConfiguration configuration)
        {
            _bucket = configuration.GetSection("InfluxDB")["bucket"];
            _organization = configuration.GetSection("InfluxDB")["organization"];
            _token = configuration.GetSection("InfluxDB")["token"];
            _url = configuration.GetSection("InfluxDB")["url"];
        }

        public void Insert(Position position)
        {
            var influxDBClient = InfluxDBClientFactory.Create(_url, _token.ToCharArray());

            using (var writeApi = influxDBClient.GetWriteApi())
            {
                var point = PointData
                    .Measurement("position")
                    .Field("x", position.X)
                    .Field("y", position.Y)
                    .Field("z", position.Z)
                    .Timestamp(position.Timestamp, WritePrecision.S);

                writeApi.WritePoint(_bucket, _organization, point);
            }
        }
    }
}