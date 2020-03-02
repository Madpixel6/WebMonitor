using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebMonitor.Clients;

namespace WebMonitor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMonitorHttpClient _monitorHttpClient;

        public Worker(ILogger<Worker> logger,
                      IMonitorHttpClient monitorHttpClient)
        {
            _logger = logger;
            _monitorHttpClient = monitorHttpClient;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("=== WebMonitor has started ===");
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("=== WebMonitor is stopping ===");
            _monitorHttpClient.Dispose();
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await _monitorHttpClient.GetWebsiteStatus("https://www.piotrbulka.pl");
                if (result.IsSuccessStatusCode)
                    _logger.LogInformation("Website is up, returned status code: {StatusCode}", result.StatusCode);
                else
                    _logger.LogWarning("Website is down, returned status code: {StatusCode} \n" +
                                       " Full status: {Status}", result.StatusCode, result.Status);

                await Task.Delay(120*1000, stoppingToken);
            }
        }
    }
}
