using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xingyi.microservices.Client;

namespace xingyi.jobClient.admin
{
    public class AdminClientSettings : HttpClientSettings
    {
    }

    public class AdminHttpClient
    {
        private readonly HttpClient httpClient;
        private string baseUrl;

        public AdminHttpClient(HttpClient httpClient, IOptions<AdminClientSettings> settings)
        {
            this.baseUrl = settings.Value.BaseUrl + "Admin";
            this.httpClient = httpClient;
        }

        public async Task purge()
        {
            var response = await httpClient.PostAsync($"{baseUrl}/purge", null);
            response.EnsureSuccessStatusCode();
        }
    }

}
