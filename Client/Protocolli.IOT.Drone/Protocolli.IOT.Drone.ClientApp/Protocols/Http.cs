using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Protocolli.IOT.Drone.ClientApp.Protocols
{
    internal class Http : IProtocol
    {
        private readonly HttpClient _httpClient = new();
        private readonly string _url = "";
        private string _complete_url;
        public async Task SendAsync(string data, string path)
        {
            _complete_url = _url + path;
            await _httpClient.PostAsync(_complete_url, new StringContent(data, Encoding.UTF8, "application/json"));
        }
    }
}
