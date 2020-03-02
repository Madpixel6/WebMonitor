using System;
using System.Threading.Tasks;
using WebMonitor.Models;

namespace WebMonitor.Clients
{
    public interface IMonitorHttpClient : IDisposable
    {
        Task<WebsiteStatus> GetWebsiteStatus(string address);
    }
}
