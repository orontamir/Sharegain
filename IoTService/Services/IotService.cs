using Base.Models;
using Base.Services;
using IoTService.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IoTService.Services
{
    public class IotService : IIotService
    {
        public static bool IsRunning = false;
        private readonly IConfiguration _configuration;
        private readonly HttpService _httpService;

        public IotService(IConfiguration configuration) 
        {
            _configuration = configuration;
            _httpService = new HttpService(new HttpClient());
            _httpService.BaseAddress = "https://localhost:7036";
        }
        public async Task Start()
        {
            IsRunning = true;
            Signal signal = new Signal();
            LogService.LogInformation("Start Running IoT Servic");
            int counter = 0;
            LoginModel loginModel = new LoginModel {
                Name = "Oron",
                Password = "qwe123",
            };
            string token = await _httpService.PostAsync<string>($"api/User/Login", new System.Collections.Generic.Dictionary<string, object>(), loginModel);
            _httpService.Bearer = token;


            while (IsRunning)
            {
                try
                {
                    DateTime dateTime = DateTime.Now;
                    var sin = signal.Sin.GetGoodSignal(counter);
                    var state = signal.State.GetGoodSignal(counter);
                    LogService.LogInformation($"Sin = {sin}, State = {state}, Counter = {counter}, dateTime =  {dateTime}");
                    signal.Sin.Value = sin;
                    signal.State.Value = state;
                    signal.DateTime = dateTime;
                    await _httpService.PostAsync<string>($"api/IoT/Queue", new System.Collections.Generic.Dictionary<string, object>(), signal);
                    Thread.Sleep(2000);

                }
                catch (Exception ex)
                {
                    LogService.LogError($"Error Message: {ex.Message}");
                }
                finally
                {
                    counter++;
                    if (counter > 6000) { counter = 0; }
                }
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
