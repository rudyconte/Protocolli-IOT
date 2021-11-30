using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using Protocolli.IOT.Drone.ClientApp.Interfaces;

namespace Protocolli.IOT.Drone.ClientApp.Protocols
{
    internal class Http : IProtocol
    {
        //set the URL of the API
        private readonly string _url  = ConfigurationManager.AppSettings["httpAPI"];
        private readonly HttpClient _httpClient = new();

        public async Task SendAsync(string data)
        {
            try
            {
                var response = await _httpClient.PostAsync(_url, new StringContent(data, Encoding.UTF8, "application/json"));
                Console.WriteLine($"{_url} responded with status code: {response.StatusCode}");
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
