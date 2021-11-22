using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;
using Microsoft.Extensions.Configuration;
using Protocolli.IoT.Drone.ApplicationCore.Interfaces.Data;
using Protocolli.IoT.Drone.ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolli.IoT.Drone.Infrastructure.Data
{
    public class BatteriesRepository : IBatteriesRepository
    {
        private readonly string _url;
        private readonly char[] _token;
        private readonly string _bucket;
        private readonly string _organization;

        public BatteriesRepository(IConfiguration configuration)
        {
            _bucket = configuration.GetSection("InfluxDB")["bucket"];
            _organization = configuration.GetSection("InfluxDB")["organization"];
            _token = configuration.GetSection("InfluxDB")["organization"].ToCharArray();
            _url = configuration.GetSection("InfluxDB")["url"];
        }

        public void Insert(Battery battery)
        {
            var influxDBClient = InfluxDBClientFactory.Create(_url, _token);

            using (var writeApi = influxDBClient.GetWriteApi())
            {
                var point = PointData.Measurement("battery")
                    .Field("level", battery.Level)
                    .Timestamp(battery.Timestamp, WritePrecision.S);

                writeApi.WritePoint(_bucket, _organization, point);
            }
        }
    }
}