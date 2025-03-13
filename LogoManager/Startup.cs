using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using System;
using LogoManager.Interface;
using LogoManager.Service;
using LogoManager.Interfaces;
using LogoManager.Services;
using LogoManager.DAL.Mongo.Repos;
using LogoManager.DAL.Mongo;
using Microsoft.Extensions.Options;

namespace LogoManager
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region |mongo configuration|
            // requires using Microsoft.Extensions.Options
            services.Configure<MultipleDatabaseSettings>(
                Configuration.GetSection(nameof(MultipleDatabaseSettings)));

            services.AddSingleton<IMultipleDatabaseSettings>(sp =>
                (IMultipleDatabaseSettings)sp.GetRequiredService<IOptions<MultipleDatabaseSettings>>().Value);

            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            #endregion |mongo configuration|

            services.AddMvc()
                .AddXmlSerializerFormatters()
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddSingleton<ILogoMessageRepository, LogoMessageRepository>();
            services.AddSingleton<ILogMessageService, LogMessageService>();
            services.AddSingleton<ITCPService, TCPService>();
          
           
            services.AddHostedService<MainService>();
           // services.AddGrpc();
          //  services.AddHttpClient();
          //  services.AddSwaggerGen();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
         //   app.UseRouting();

          //  app.UseAuthorization();

         //   app.UseEndpoints(endpoints =>
         //   {
         //       endpoints.MapControllers();
        //    });

            app.UseStaticFiles();
            app.UseAuthentication();
           

            //   app.UseSwagger();
            //  app.UseSwaggerUI();

            //  applicationLifetime.ApplicationStopping.Register(OnShutdown);
        }

        void OnShutdown()
        {
            
        }
    }
}
