using LogoManager.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace LogoManager.Service
{
    public class MainService : IHostedService
    {
        private readonly IConfiguration _Configuration;
        private readonly ITCPService _TCPService;
        public MainService(ITCPService tcpService, IConfiguration configuration)
        {
            _TCPService = tcpService;
            _Configuration = configuration;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Configuration IpAddress and Port
            int port = _Configuration.GetSection("TcpPort") != null ? int.Parse(_Configuration.GetSection("TcpPort").Value) : 1313;
            string ipAddress = _Configuration.GetSection("TcpIp") != null ? (_Configuration.GetSection("TcpIp").Value) : "0.0.0.0";
            _TCPService.Start(port, ipAddress);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _TCPService.Stop();
            return Task.CompletedTask;
        }
    }
}
