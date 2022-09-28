using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.Repository.Interfaces;

namespace HealthWorkerService
{
    public class PatientCaseWorker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISystemHealthRepository _systemHealthRepository;
        public PatientCaseWorker(ILogger<Worker> logger, ISystemHealthRepository systemHealthRepository)
        {
            _logger = logger;
            _systemHealthRepository = systemHealthRepository;


        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            bool isexecuted = false;
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                   
                    //12pm-1am run once 
                    DateTime now = UtilityMaster.GetLocalDateTime();
                    DateTime hardcodedTime = UtilityMaster.GetLocalDateTime();

                    if (now.Date== hardcodedTime.Date)
                    {
                        if (now.Hour == 16)
                        {
                            if (now.Minute == 31)
                            {
                                isexecuted = false;
                            }
                            if (now.Minute == 32 && !isexecuted)
                            {
                                isexecuted = true;
                                _logger.LogInformation("PatientCaseWorker running at: {time}", DateTimeOffset.Now);
                                await _systemHealthRepository.UpdateYesterdayPedingCaseToOrphan();
                            }
                        }
                    }
                   
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception : ", ex);

                }

                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
