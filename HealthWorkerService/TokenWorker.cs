using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.ZoomAPI.Service;
using TechMed.DL.Models;

namespace HealthWorkerService
{
    public class TokenWorker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IZoomAccountService _zoomAccountService;
        //private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TeleMedecineContext _telemedecineContext;
        private readonly IOptions<ConnectionStrings> config;
        public TokenWorker(ILogger<Worker> logger, IZoomAccountService zoomAccountService, IOptions<ConnectionStrings> config)
        {
            _logger = logger;
            _zoomAccountService = zoomAccountService;
            //_serviceScopeFactory = serviceScopeFactory;
            //_telemedecineContext = telemedecineContext;
            this.config=config;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string filepath = @"C:\Users\ADMIN\source\repos\Dashboard_Changes\TechmedProj\HealthWorkerService\bin\Release\net6.0\publish\test.txt";

            while (!stoppingToken.IsCancellationRequested)
            {
                var optionsBuilder = new DbContextOptionsBuilder<TeleMedecineContext>();
                //optionsBuilder.UseSqlServer(Configuration.GetConnectionStringSecureValue("DefaultConnection"));
                optionsBuilder.UseSqlServer(config.Value.TeliMedConn);

                TeleMedecineContext _telemedecineContext = new TeleMedecineContext(optionsBuilder.Options);
                try
                {
                    //File.AppendAllLines(filepath, new List<string>() { "TokenWorker running at: " + DateTimeOffset.Now.ToString() });
                    //using (var scope = _serviceScopeFactory.CreateScope())
                    //{
                    //var _telemedecineContext = new TeleMedecineContext();
                    using (_telemedecineContext)
                    {
                        bool response = false;
                        ZoomToken zoomToken = _telemedecineContext.ZoomTokens.AsNoTracking().FirstOrDefault();

                        if (zoomToken == null)
                        {
                            //insert first record
                            //add token1
                            string token1 = await _zoomAccountService.GetNewTokenFromZoomAsync(0);
                            zoomToken = new ZoomToken
                            {
                                ActiveTokenNumber = 1,
                                Token1CreatedOn = UtilityMaster.GetLocalDateTime(),
                                Token1 = token1,
                                Token2CreatedOn = UtilityMaster.GetLocalDateTime(),
                                Token2 = token1
                            };
                            _telemedecineContext.ZoomTokens.Add(zoomToken);
                            _telemedecineContext.SaveChanges();

                        }
                        else
                        {
                            if (zoomToken.ActiveTokenNumber == 1)
                            {
                                //File.AppendAllLines(filepath, new List<string>() { "TokenWorker at: " + DateTimeOffset.Now + "zoomToken.ActiveTokenNumber == 1" });

                                // check time duration
                                // if more tha 50 min old
                                // create token2
                                // set ActiveTokenNumber 2 
                                var result = UtilityMaster.GetLocalDateTime().Subtract(zoomToken.Token1CreatedOn).TotalMinutes;
                                if (result > 15)
                                {
                                    //File.AppendAllLines(filepath, new List<string>() { "TokenWorker at: " + DateTimeOffset.Now + "result > 15" });

                                    string token2 = await _zoomAccountService.GetNewTokenFromZoomAsync(1);
                                    //File.AppendAllLines(filepath, new List<string>() { "TokenWorker at: " + DateTimeOffset.Now + "GetNewTokenFromZoomAsync(1)" });

                                    zoomToken.Token2 = token2;
                                    zoomToken.Token2CreatedOn = UtilityMaster.GetLocalDateTime();
                                    zoomToken.ActiveTokenNumber = 2;
                                    _telemedecineContext.ZoomTokens.Add(zoomToken);
                                    _telemedecineContext.Entry(zoomToken).State = EntityState.Modified;
                                    _telemedecineContext.SaveChanges();
                                    _telemedecineContext.Entry(zoomToken).State = EntityState.Detached;

                                    //File.AppendAllLines(filepath, new List<string>() { "TokenWorker at: " + DateTimeOffset.Now + "SaveChanges()" });


                                }
                            }
                            if (zoomToken.ActiveTokenNumber == 2)
                            {
                                //File.AppendAllLines(filepath, new List<string>() { "TokenWorker at: " + DateTimeOffset.Now + "zoomToken.ActiveTokenNumber == 2" });
                                // check time duration
                                // if more tha 50 min old
                                // create token1
                                // se1 ActiveTokenNumber 1
                                var result = UtilityMaster.GetLocalDateTime().Subtract(zoomToken.Token2CreatedOn).TotalMinutes;
                                if (result > 15)
                                {
                                    //File.AppendAllLines(filepath, new List<string>() { "TokenWorker at: " + DateTimeOffset.Now + "result > 15" });

                                    string token1 = await _zoomAccountService.GetNewTokenFromZoomAsync(0);
                                    //File.AppendAllLines(filepath, new List<string>() { "TokenWorker at: " + DateTimeOffset.Now + "GetNewTokenFromZoomAsync(1)" });

                                    zoomToken.Token1 = token1;
                                    zoomToken.Token1CreatedOn = UtilityMaster.GetLocalDateTime();
                                    zoomToken.ActiveTokenNumber = 1;
                                    _telemedecineContext.ZoomTokens.Add(zoomToken);
                                    _telemedecineContext.Entry(zoomToken).State = EntityState.Modified;
                                    _telemedecineContext.SaveChanges();
                                    _telemedecineContext.Entry(zoomToken).State = EntityState.Detached;
                                    //File.AppendAllLines(filepath, new List<string>() { "TokenWorker at: " + DateTimeOffset.Now + "SaveChanges()" });

                                }
                            }
                        }
                        //}

                    }

                    //File.AppendAllLines(filepath, new List<string>() { "TokenWorker EndLine at: " + DateTimeOffset.Now });
                    _logger.LogInformation("TokenWorker running at: {time}", DateTimeOffset.Now);
                    //Console.WriteLine("TokenWorker running at: {time}", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    File.AppendAllLines(filepath, new List<string>() { "TokenWorker Exception at: " + ex.StackTrace });

                    _logger.LogError("Exception : ", ex);

                }

                await Task.Delay(1000 * 10, stoppingToken);
            }
        }


    }
    public class ConnectionStrings 
    {
        public string? TeliMedConn { get; set; }
    }

}