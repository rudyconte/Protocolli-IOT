using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;
using Protocolli.IoT.Drone.ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolli.IoT.Drone.Infrastructure.Data
{
    internal class VelocitiesRepository
    {
        private static readonly char[] Token = "".ToCharArray();

        private readonly string bucketName = "protocolli-iot-drone";
        private readonly string orgId = "protocolli-iot";

        public void Insert(Velocity velocity)
        {
            var influxDBClient = InfluxDBClientFactory.Create("http://localhost:8086", Token);

            using (var writeApi = influxDBClient.GetWriteApi())
            {
                var point = PointData.Measurement("velocity")
                    .Field("speed", velocity.Speed)
                    .Timestamp(velocity.Timestamp, WritePrecision.S);

                writeApi.WritePoint(bucketName, orgId, point);
            }
        }
    }
}