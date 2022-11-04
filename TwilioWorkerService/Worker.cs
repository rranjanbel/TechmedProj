using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;

namespace TwilioWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISystemHealthRepository _systemHealthRepository;
        private readonly IOptions<HostUrl> config;
        private readonly IOptions<TwilioConfig> twilioConfig;
        private readonly TeleMedecineContext _teleMedecineContext;
        public Worker(ILogger<Worker> logger, ISystemHealthRepository systemHealthRepository, IOptions<HostUrl> config, TeleMedecineContext teleMedecineContext, IOptions<TwilioConfig> twilioConfig)
        {
            _logger = logger;
            _systemHealthRepository = systemHealthRepository;
            this.config = config;
            this.twilioConfig = twilioConfig;
            _teleMedecineContext = teleMedecineContext;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string str=twilioConfig.Value.TwilioAuthToken;
                var data = _teleMedecineContext.TwilioVideoDownloadStatus.Include(a=>a.TwilioMeetingRoomInfo).ToList();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
    public class HostUrl
    {
        public string? APIHost { get; set; }
        public string? AngHost { get; set; }
    }
    public class TwilioConfig
    {
        public string? TwilioAccountSid { get; set; }
        public string? TwilioApiSecret { get; set; }
        public string? TwilioApiKey { get; set; }
        public string? TwilioAuthToken { get; set; }
    }
}