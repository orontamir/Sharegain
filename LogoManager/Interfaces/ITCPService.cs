using System;
using System.Threading.Tasks;

namespace LogoManager.Interface
{
    public interface ITCPService
    {
        Task Start(int port, string ipAddress);
        void Stop();
    }
}
