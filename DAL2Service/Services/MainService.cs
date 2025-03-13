
using DAL2Service.Interfaces;

namespace DAL2Service.Services
{
    public class MainService : IHostedService
    {
        private readonly IQueueService _queueService;

        public MainService(IQueueService queueService)
        {
            this._queueService = queueService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _queueService.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _queueService.Stop();
            return Task.CompletedTask;
        }
    }
}
