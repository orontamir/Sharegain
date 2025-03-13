using IoTService.Extensions;
using Serilog;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
namespace IoTService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("*** Listening on http://0.0.0.0:6000 ***");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .UseSerilog(((context, services) =>
                 {
                     StartupExtension.InternalizeSerilog(context, services, "IoTService.log");
                 }))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://0.0.0.0:6000");
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenAnyIP(5000, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http1;
                        });
                    });

                    var builder = WebApplication.CreateBuilder(args);

                });

    }
}
