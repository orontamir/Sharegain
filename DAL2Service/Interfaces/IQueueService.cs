using Base.Models;

namespace DAL2Service.Interfaces
{
    public interface IQueueService
    {
        public Task ProduceAsync(Signal signal);

        public Task ConsumAsync();

        public Task Start();
        public void Stop();
    }
}
