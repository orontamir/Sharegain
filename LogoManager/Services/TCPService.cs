using System;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using Serilog;
using System.Threading;
using Microsoft.Extensions.Configuration;
using LogoManager.Interface;
using System.Text;
using LogoManager.Models;
using LogoManager.Interfaces;
using LogoManager.Extensions;

namespace LogoManager.Service
{
    public class TCPService : ITCPService
    {
        TcpListener listener = null;
        TcpClient client = new TcpClient();
        readonly ILogMessageService _LogMessageService;

        public TCPService(ILogMessageService logMessageService)
        {
            _LogMessageService = logMessageService;
        }

       

        public  async Task Start(int port, string ipAddress)
        {
            listener = new TcpListener(IPAddress.Parse(ipAddress), port);
            listener.Start();
            LogInformation($"Server listening on port {port}");

            try
            {
                while (true)
                {
                     client = await listener.AcceptTcpClientAsync();
                    _ = HandleClientAsync(client);
                }
            }
            finally
            {
                listener.Stop();
            }
        }

        private  async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                string clientInfo = client.Client.RemoteEndPoint.ToString();
                LogInformation($"Client connected: {clientInfo}");

                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    LogoMessageModel logMessage = new LogoMessageModel {message = dataReceived, clientInfo = clientInfo, time = DateTime.Now};
                    LogInformation($"Received: {logMessage}");
                    var newEntity = await _LogMessageService.InsertLogMessage(logMessage.ToEntity());

                    await stream.WriteAsync(buffer, 0, bytesRead);
                }

                LogInformation($"Client disconnected: {client.Client.RemoteEndPoint}");
            }
            catch (Exception ex)
            {
                LogError($"An error occurred with a client: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }

        public void Stop()
        {
            client.Close();
        }

        public static void LogInformation(string message)
        {
            Log.Logger.Information(message);
        }

        public static void LogError(string message)
        {
            Log.Logger.Error(message);
        }

    }
}
