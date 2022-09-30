using HealthWorkerService;
using Microsoft.EntityFrameworkCore;
using TechMed.BL.Repository.BaseClasses;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ZoomAPI.Model;
using TechMed.BL.ZoomAPI.Service;
using TechMed.DL.Models;


var builder = new ConfigurationBuilder()
          .SetBasePath(Path.Combine(AppContext.BaseDirectory))
          .AddJsonFile("appsettings.json", optional: true);

var Configuration = builder.Build();
var connectionString =Configuration.GetConnectionString("TeliMedConn");
string str= Configuration.GetValue<string>("Host:APIHost");
var strv=Configuration.GetSection("MyConfig");
var strvs=Configuration.GetSection("ConnectionStrings");
var t=Configuration.GetSection("HostUrl").Get<HostUrl>();
//var services = new ServiceCollection();
//services.AddDbContext<TeleMedecineContext>(o => o.UseSqlServer(connectionString), ServiceLifetime.Transient);
//services.AddScoped<IUserRepository, UserRepository>();
//services.AddScoped<ISystemHealthRepository, SystemHealthRepository>();

//var serviceProvider = services.BuildServiceProvider();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<TeleMedecineContext>(o => o.UseSqlServer(connectionString), ServiceLifetime.Singleton);
        services.AddHostedService<Worker>();
        services.AddHostedService<TokenWorker>();
        services.AddSingleton<ISystemHealthRepository, SystemHealthRepository>();
        services.Configure<HostUrl>(Configuration.GetSection("HostUrl"));
        services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
        services.Configure<ZoomSettings>(
        settings =>
        {
            settings.ZoomSSAccountId = Configuration.GetValue<string>("Zoom:ZoomSSAccountId");
            settings.ZoomSSClientID = Configuration.GetValue<string>("Zoom:ZoomSSClientID");
            settings.ZoomSSClientSecret = Configuration.GetValue<string>("Zoom:ZoomSSClientSecret");
        }).AddSingleton<IZoomAccountService, ZoomAccountService>();

    }).UseWindowsService()
    .Build();


await host.RunAsync();
