using System.Net.Http;
using System.Threading.Tasks;
using WebMonitor.Models;

namespace WebMonitor.Clients
{
    public class MonitorHttpClient : IMonitorHttpClient
    {
        private readonly HttpClient httpClient;
        public MonitorHttpClient(HttpClient httpClient) => this.httpClient = httpClient;

        public async Task<WebsiteStatus> GetWebsiteStatus(string address)
        {
            var response = await httpClient.GetAsync(address);
            return new WebsiteStatus
            {
                Status = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode,
                IsSuccessStatusCode = response.IsSuccessStatusCode
            };
        }
        public void Dispose()
            => httpClient.Dispose();
    }
}
