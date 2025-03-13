using DAL2Service;
using DAL2Service.DAL.Mongo.Interfaces;
using DAL2Service.DAL.Mongo.Repos;
using DAL2Service.DAL.Mongo.Settings;
using DAL2Service.DAL.Sql;
using DAL2Service.Interfaces;
using DAL2Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Text;
using XAct;
Console.WriteLine("*** Listening on https://localhost:7036 ***");
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(((context, services) =>
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
 }));
builder.Services.Configure<MultipleDataBaseSettings>(
    builder.Configuration.GetSection(nameof(MultipleDataBaseSettings))
    );
builder.Services.AddSingleton<IMultipleDataBaseSettings>(sp =>
    (IMultipleDataBaseSettings)sp.GetRequiredService<IOptions<MultipleDataBaseSettings>>().Value
    );
builder.Services.AddSingleton<IDataBaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DataBaseSettings>>().Value
);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
JwtService.secret = "SharegainTokenSecretSharegainTokenSecret";
JwtService.expDate ="15";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "YourIssuer",
            ValidAudience = "YourAudience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SharegainTokenSecretSharegainTokenSecret"))
        };
    });
builder.Services.AddHostedService<MainService>();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddSingleton<RepositoryBase, Repository>();
builder.Services.AddSingleton<ISignalService, SignalService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IQueueService, QueueService>();
builder.Services.AddSingleton<ISignalRepository, SignalRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
