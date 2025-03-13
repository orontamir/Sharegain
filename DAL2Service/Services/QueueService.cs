using DAL2Service.Interfaces;
using System.Collections.Concurrent;
using Base.Models;
using Base.Services;
using DAL2Service.Models;

namespace DAL2Service.Services
{
    public class QueueService : IQueueService
    {
        private readonly ConcurrentQueue<Signal> _messageQueue = new ConcurrentQueue<Signal>();

        private readonly ISignalService _signalService;
        private readonly IConfiguration _configuration;
        private bool _IsRunning = false;
        public QueueService(IConfiguration configuration, ISignalService signalService) 
        {
            _signalService = signalService;
            _configuration = configuration;
            _IsRunning = false;
        }
        public async Task ConsumAsync()
        {
            await Parallel.ForEachAsync(
                GetSignalEnumerable(),
                new ParallelOptions { MaxDegreeOfParallelism = 1},
                async (signal, _) =>
                {
                    try
                    {
                        await CreateData(signal);
                    }
                    catch (Exception ex)
                    {
                        LogService.LogError($"Error Message: {ex.Message}");
                    }
                }
                );
        }

        private IEnumerable<Signal> GetSignalEnumerable()
        {
            while (!_messageQueue.IsEmpty)
            {
                if (_messageQueue.TryDequeue(out Signal signal))
                {
                    yield return signal;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        private async Task CreateData(Signal signal)
        {
            try
            {
                double amplitude = 32.0;
                double max_range = 4095.0;
                double min_range = 256.0;
                Sin _sin = new Sin();
                SignalDbModel signalDbModel = new SignalDbModel
                {
                    Type = "Sin",
                    TimeStemp = signal.DateTime,
                    Time = signal.DateTime.TimeOfDay.ToString(),
                    Date = signal.DateTime.Date.ToString(),
                    Value = signal.Sin.Value,
                    Error = _sin.Validation(signal.Sin.Value, 0.0, amplitude)
                };
                await  _signalService.AddSignal(signalDbModel);
                State _state = new State();
                signalDbModel = new SignalDbModel
                {
                    Type = "State",
                    TimeStemp = signal.DateTime,
                    Time = signal.DateTime.TimeOfDay.ToString(),
                    Date = signal.DateTime.Date.ToString(),
                    Value = signal.State.Value,
                    Error = _state.Validation(signal.State.Value, 0.0, amplitude)
                };
                await _signalService.AddSignal(signalDbModel);

            }
            catch (Exception ex)
            {
                LogService.LogError($"Exception when insert signal into data base , error message: {ex.Message}");
            }
        }

        public async Task ProduceAsync(Signal signal)
        {
            _messageQueue.Enqueue(signal);
        }

        public async Task Start()
        {
            _IsRunning = true;
            while (_IsRunning) 
            {
                await ConsumAsync();
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            _IsRunning = false;
        }
    }
}
