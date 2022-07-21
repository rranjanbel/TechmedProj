using TechMed.BL.Repository.BaseClasses;
using TechMed.BL.Repository.Interfaces;

namespace HealthWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISystemHealthRepository _systemHealthRepository;

        public Worker(ILogger<Worker> logger, ISystemHealthRepository systemHealthRepository)
        {
            _logger = logger;
            _systemHealthRepository = systemHealthRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _systemHealthRepository.GetANGStatus();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}