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


    }).UseWindowsService()
    .Build();

await host.RunAsync();
