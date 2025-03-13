using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace DALService
{
    public class Program
    {


        public static void Main(string[] args)
        {
            Console.WriteLine("*** Listening on http://localhost:7000 ***");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .UseSerilog(((context, services) =>
                {
                    var config = new ConfigurationBuilder()
                       .AddEnvironmentVariables()
                       .Build();
                    var template = "{Timestamp:yyyy-MM-dd HH:mm:ss}  {Level:u4}  {Message:lj}{NewLine}{Exception}";
                    services
                        .Enrich.FromLogContext()
                        .WriteTo.Console(outputTemplate: template)
                        .WriteTo.Debug(outputTemplate: template)
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .ReadFrom.Configuration(context.Configuration);

                    var logFolder = context.Configuration["LOG_FOLDER"] ?? "Log";
                    var logFile = Path.Combine(logFolder, "DALService.log");
                    services.WriteTo.File(logFile, outputTemplate: template, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, rollOnFileSizeLimit: true, fileSizeLimitBytes: 10000000);
                }
                )
                )
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:7000");

                })
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    // Add other providers for JSON, etc.

                    if (hostContext.HostingEnvironment.IsDevelopment())
                    {
                        builder.AddUserSecrets<Program>();
                    }
                });
    }
}
