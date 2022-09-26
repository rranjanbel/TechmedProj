using Microsoft.Extensions.Options;
using TechMed.BL.Repository.BaseClasses;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ZoomAPI.Service;

namespace HealthWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISystemHealthRepository _systemHealthRepository;
        private readonly IOptions<HostUrl> config;
        private readonly IZoomAccountService _zoomAccountService;
        public Worker(ILogger<Worker> logger, ISystemHealthRepository systemHealthRepository, IOptions<HostUrl> config, IZoomAccountService zoomAccountService)
        {
            _logger = logger;
            _systemHealthRepository = systemHealthRepository;
            this.config = config;
            _zoomAccountService = zoomAccountService;


        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var APIHost = config.Value.APIHost;
                    var AngHost = config.Value.AngHost;
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    bool SaveStatusInDB = await _systemHealthRepository.SaveStatusInDB(APIHost, AngHost);
                    bool UpdateLogout = await _systemHealthRepository.UpdateLogout();
                    bool RotateTokenAsyncResult = await _zoomAccountService.RotateTokenAsync();
                    //_logger.LogInformation("ANG Status : " + await _systemHealthRepository.GetANGStatus());
                    //_logger.LogInformation("API Status : " + await _systemHealthRepository.GetAPIStatus());
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception : ", ex);

                }

                await Task.Delay(1000 * 5, stoppingToken);
            }
        }
    }
    public class HostUrl
    {
        public string? APIHost { get; set; }
        public string? AngHost { get; set; }
    }
}