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
    internal class BatteriesRepository
    {
        private static readonly char[] Token = "".ToCharArray();

        private readonly string bucketName = "protocolli-iot-drone";
        private readonly string orgId = "protocolli-iot";

        public void Insert(Battery battery)
        {
            var influxDBClient = InfluxDBClientFactory.Create("http://localhost:8086", Token);

            //
            // Write Data
            //
            using (var writeApi = influxDBClient.GetWriteApi())
            {
                //
                // Write by Point
                //
                var point = PointData.Measurement("battery")
                    .Field("level", battery.Level)
                    .Timestamp(battery.Timestamp, WritePrecision.S);

                writeApi.WritePoint(bucketName, orgId, point);
            }
        }
    }
}