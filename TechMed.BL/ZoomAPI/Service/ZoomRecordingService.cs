using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechMed.BL.ZoomAPI.Model;
using TechMed.DL.Models;

namespace TechMed.BL.ZoomAPI.Service
{
    public class ZoomRecordingService : IZoomRecordingService
    {
        readonly TeleMedecineContext _teleMedecineContext;
        readonly IZoomAccountService _zoomAccountService;
        public ZoomRecordingService(TeleMedecineContext teleMedecineContext, IZoomAccountService zoomAccountService)
        {
            _teleMedecineContext = teleMedecineContext;
            _zoomAccountService = zoomAccountService;

        }
        public async Task<GetRecordingResponseModel> GetRecording(string MeetingID)
        {
            //string token =await _zoomAccountService.GetTokenAsync();
            var token = await _zoomAccountService.GetIssuedTokenAsync();

            GetRecordingResponseModel responseModel = new GetRecordingResponseModel();
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Add("X-Version", "1");
                _httpClient.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync("https://api.zoom.us/v2/meetings/"+ MeetingID.Trim() + "/recordings");
                if (response.IsSuccessStatusCode)
                {
                    //200 success response
                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseModel = JsonSerializer.Deserialize<GetRecordingResponseModel>(responseContent);
                }

            }
            return responseModel;
        }
    }
}
