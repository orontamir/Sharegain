using DALService.DAL.Sql;
using DALService.Interfaces;
using DALService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;

namespace DALService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //#region |mongo configuration|
            //// requires using Microsoft.Extensions.Options
            //services.Configure<MultipleDatabaseSettings>(
            //    Configuration.GetSection(nameof(MultipleDatabaseSettings)));

            //services.AddSingleton<IMultipleDatabaseSettings>(sp =>
            //    (IMultipleDatabaseSettings)sp.GetRequiredService<IOptions<MultipleDatabaseSettings>>().Value);

            //services.AddSingleton<IDatabaseSettings>(sp =>
            //    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            //#endregion |mongo configuration|

            services.AddControllers().AddNewtonsoftJson();

           // JwtService.secret = Configuration.GetValue<string>("secret") ?? "Default_TokenSecret";
          //  JwtService.expDate = Configuration.GetValue<string>("expirationInMinutes") ?? "15";
           // services.AddTokenAuthentication(Configuration.GetSection("secret").Value);
           // services.AddSingleton<JwtService>();

            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sharegain",
                    Version = "v1",
                    Description = "DAL Service - REST API",
                    Contact = new OpenApiContact
                    {
                        Name = "Sharegain",
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            services.AddMemoryCache();
            services.AddHostedService<MainService>();
            services.AddSingleton<RepositoryBase, Repository>();
            services.AddSingleton<ISignalService, SignalService>();

            

            //AddMongoDb(services);

            //services.AddSingleton<FilesService>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost", "ionic://localhost", "capacitor://localhost", "https://localhost").AllowAnyMethod().AllowAnyHeader();
                    });
            });
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IServiceProvider serviceProvider,
            IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/health");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dev");
                c.RoutePrefix = string.Empty;
            });  
        }
    }
}
