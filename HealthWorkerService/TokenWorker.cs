using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.ZoomAPI.Service;

namespace HealthWorkerService
{
    public class TokenWorker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IZoomAccountService _zoomAccountService;
        public TokenWorker(ILogger<Worker> logger, IZoomAccountService zoomAccountService)
        {
            _logger = logger;
            _zoomAccountService = zoomAccountService;


        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                 
                    _logger.LogInformation("TokenWorker running at: {time}", DateTimeOffset.Now);
                    bool RotateTokenAsyncResult = await _zoomAccountService.RotateTokenAsync();
                 
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception : ", ex);

                }

                await Task.Delay(1000 * 10, stoppingToken);
            }
        }
    }
  
}