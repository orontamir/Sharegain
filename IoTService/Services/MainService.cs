
using IoTService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace IoTService.Services
{
    public class MainService : IHostedService
    {
        private readonly IConfiguration _Configuration;

       
        private readonly IIotService  _IotService;
        public MainService(IConfiguration configuration, IIotService iotService) 
        {
            _Configuration = configuration;
            _IotService = iotService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _IotService.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _IotService.Stop();
            return Task.CompletedTask;
        }
    }
}
