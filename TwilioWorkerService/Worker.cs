using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using System.Text;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TwilioWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISystemHealthRepository _systemHealthRepository;
        private readonly IOptions<HostUrl> config;
        private readonly IOptions<TwilioConfig> twilioConfig;
        //private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IOptions<ConnectionStrings> _configConnectionStr;
        public Worker(ILogger<Worker> logger, ISystemHealthRepository systemHealthRepository, IOptions<HostUrl> config/*, TeleMedecineContext teleMedecineContext*/, IOptions<TwilioConfig> twilioConfig, IOptions<ConnectionStrings> configConnectionStr)
        {
            _logger = logger;
            _systemHealthRepository = systemHealthRepository;
            this.config = config;
            this.twilioConfig = twilioConfig;
            //_teleMedecineContext = teleMedecineContext;
            _configConnectionStr = configConnectionStr;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //insert in to status table
                //select top (100) completed consulation with uncomplete download status
                //loop for each
                //download video
                //create folder of date if not exits and download to them
                //update staus 
                //on error update attempt
                var optionsBuilder = new DbContextOptionsBuilder<TeleMedecineContext>();
                //optionsBuilder.UseSqlServer(Configuration.GetConnectionStringSecureValue("DefaultConnection"));
                optionsBuilder.UseSqlServer(_configConnectionStr.Value.TeliMedConn);

                TeleMedecineContext _telemedecineContext = new TeleMedecineContext(optionsBuilder.Options);
                using (_telemedecineContext)
                {
                    var Results = _telemedecineContext.InsertIntoTwilioVideoDownloadStatus.FromSqlInterpolated($"EXEC [dbo].[InsertIntoTwilioVideoDownloadStatus] ");
                    InsertIntoTwilioVideoDownloadStatusVM Spdata;
                    foreach (var item in Results)
                    {
                        Spdata = new InsertIntoTwilioVideoDownloadStatusVM();
                        Spdata.Success = item.Success;
                    }

                    var data = _telemedecineContext.TwilioVideoDownloadStatus.Include(a => a.TwilioMeetingRoomInfo)
                        .Where(a => a.Status != "Completed" && a.Attempt < 1).Take(2)
                        .ToList();
                    //string accountSid = (twilioConfig.Value.TwilioAccountSid);
                    //string authToken = (twilioConfig.Value.TwilioAuthToken);
                    string FolderPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    foreach (var item in data)
                    {
                       string  fullpath=FolderPath + "\\" +Convert.ToDateTime( item.TwilioMeetingRoomInfo.CreateDate).ToString("yyyy-MM-dd");
                        DirectoryInfo directoryInfo = new DirectoryInfo(fullpath);
                        if (!directoryInfo.Exists)
                        {
                            directoryInfo.Create();
                        }
                        item.Attempt = item.Attempt + 1;
                        item.Status = "Completed";
                        item.StatusAt = UtilityMaster.GetLocalDateTime();
                        try
                        {

                            string uri = "https://video.twilio.com/v1/Compositions/" + item.TwilioMeetingRoomInfo.CompositeVideoSid + "/Media?Ttl=3600";
                            var request = (HttpWebRequest)WebRequest.Create(uri);
                            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(twilioConfig.Value.TwilioApiKey + ":" + twilioConfig.Value.TwilioApiSecret)));
                            request.AllowAutoRedirect = false;
                            string responseBody = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                            var mediaLocation = await
                                Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody)["redirect_to"]);

                            await HttpDownloadFileAsync(new HttpClient(), mediaLocation, fullpath + "\\" + item.TwilioMeetingRoomInfo.RoomName + ".mp4");

                            //TwilioClient.Init(accountSid, authToken);
                            //var recording = RecordingResource.Fetch(
                            //    pathSid: "CJ925b2ec5f7827dbeaa26bd50c1b9be4d"
                            //);
                            //Console.WriteLine(recording.CallSid);

                        }
                        catch (Exception ex)
                        {
                            item.Status = "Exception";
                            item.Message = ex.Message;
                        }
                        _telemedecineContext.SaveChanges();
                        _telemedecineContext.Entry(item).State = EntityState.Detached;
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
        static public async Task HttpDownloadFileAsync(HttpClient httpClient, string url, string fileToWriteTo)
        {
            using HttpResponseMessage response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            using Stream streamToReadFrom = await response.Content.ReadAsStreamAsync();
            using Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create);
            await streamToReadFrom.CopyToAsync(streamToWriteTo);
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
    public class ConnectionStrings
    {
        public string? TeliMedConn { get; set; }
    }
}