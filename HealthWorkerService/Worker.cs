using Microsoft.Extensions.Options;
using TechMed.BL.Repository.BaseClasses;
using TechMed.BL.Repository.Interfaces;

namespace HealthWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISystemHealthRepository _systemHealthRepository;
        private readonly IOptions<MyConfig> config;
        public Worker(ILogger<Worker> logger, ISystemHealthRepository systemHealthRepository, IOptions<MyConfig> config)
        {
            _logger = logger;
            _systemHealthRepository = systemHealthRepository;
            this.config=config;
            var t = config.Value.ApplicationName;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation("ANG Status : " + await _systemHealthRepository.GetANGStatus());
                _logger.LogInformation("API Status : " + await _systemHealthRepository.GetAPIStatus());
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
    public class MyConfig
    {
        public string? ApplicationName { get; set; }
        public int Version { get; set; }
    }
}