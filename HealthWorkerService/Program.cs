using HealthWorkerService;
using Microsoft.EntityFrameworkCore;
using TechMed.BL.Repository.BaseClasses;
using TechMed.BL.Repository.Interfaces;
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
        services.AddSingleton<ISystemHealthRepository, SystemHealthRepository>();
        services.Configure<HostUrl>(Configuration.GetSection("HostUrl"));

    }).UseWindowsService()
    .Build();


await host.RunAsync();
