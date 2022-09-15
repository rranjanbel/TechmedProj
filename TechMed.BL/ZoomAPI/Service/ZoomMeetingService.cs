using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechMed.BL.ZoomAPI.Model;
using TechMed.BL.ZoomAPI.Model.Response;
using TechMed.DL.Models;

namespace TechMed.BL.ZoomAPI.Service
{
    public class ZoomMeetingService : IZoomMeetingService
    {


        readonly TeleMedecineContext _teleMedecineContext;
        readonly IZoomAccountService _zoomAccountService;
        public ZoomMeetingService(TeleMedecineContext teleMedecineContext, IZoomAccountService zoomAccountService)
        {
            _teleMedecineContext = teleMedecineContext;
            _zoomAccountService = zoomAccountService;
        }

        public async Task<bool> DeleteMeeting(string meetingID)
        {

            var token = await _zoomAccountService.GetIssuedTokenAsync();
            using (HttpClient _httpClient = new HttpClient())
            {

                _httpClient.DefaultRequestHeaders.Add("X-Version", "1");
                _httpClient.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.DeleteAsync("https://api.zoom.us/v2/meetings/" + meetingID);
                if (response.IsSuccessStatusCode)
                {
                    //204 deleted 
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return true;
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responsevalue = JsonSerializer.Deserialize<bool>(responseContent);

                }

            }
            return false;
        }

        public async Task<bool> EndMeeting(string meetingID)
        {
            EndMeetingModel endMeetingModel = new EndMeetingModel();
            var token = await _zoomAccountService.GetIssuedTokenAsync();
            string json = JsonSerializer.Serialize<EndMeetingModel>(endMeetingModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient _httpClient = new HttpClient())
            {

                _httpClient.DefaultRequestHeaders.Add("X-Version", "1");
                _httpClient.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PutAsync("https://api.zoom.us/v2/meetings/"+ meetingID + "/status", content);
                if (response.IsSuccessStatusCode)
                {
                    //204 deleted 
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return true;
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responsevalue = JsonSerializer.Deserialize<bool>(responseContent);

                }

            }
            return false;
        }

        public async Task<NewMeetingResponseModel> NewMeeting(NewMeetingRequestModel newMeetingRequestModel, string HostUserID)
        {
            //string token =await _zoomAccountService.GetTokenAsync();
            var token = await _zoomAccountService.GetIssuedTokenAsync();
            NewMeetingResponseModel responseDTO = new NewMeetingResponseModel();
            string json = JsonSerializer.Serialize<NewMeetingRequestModel>(newMeetingRequestModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient _httpClient = new HttpClient())
            {

                _httpClient.DefaultRequestHeaders.Add("X-Version", "1");
                _httpClient.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsync("https://api.zoom.us/v2/users/" + HostUserID + "/meetings", content);
                if (response.IsSuccessStatusCode)
                {
                    //204 deleted/No content
                    //201 created 
                    //401 unautherize

                    var responseContent = await response.Content.ReadAsStringAsync();
                    responseDTO = JsonSerializer.Deserialize<NewMeetingResponseModel>(responseContent);

                }

            }
            return responseDTO;
        }
    }
}
