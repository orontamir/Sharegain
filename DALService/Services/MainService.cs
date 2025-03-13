
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace DALService.Services
{
    public class MainService : IHostedService
    {
        private readonly IConfiguration _iconfiguration;
        public MainService(IConfiguration configuration) 
        {
            _iconfiguration = configuration;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
