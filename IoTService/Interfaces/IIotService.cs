using System.Threading.Tasks;

namespace IoTService.Interfaces
{
    public interface IIotService
    {
        Task Start();
        void Stop();
    }
}
