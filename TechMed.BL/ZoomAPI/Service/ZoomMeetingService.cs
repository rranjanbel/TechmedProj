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
    public class ZoomMeetingService : IZoomMeetingService
    {


        readonly TeleMedecineContext _teleMedecineContext;
        public ZoomMeetingService(TeleMedecineContext teleMedecineContext)
        {
            _teleMedecineContext = teleMedecineContext;
        }
        public async Task<NewMeetingResponseModel> NewMeeting(NewMeetingRequest.NewMeetingRequestModel newMeetingRequestModel)
        {
            //string token =await _zoomAccountService.GetTokenAsync();
            var token = _teleMedecineContext.Configurations.Where(a => a.Name == "ZoomToken").FirstOrDefault();
            NewMeetingResponseModel newUserResponse = new NewMeetingResponseModel();
            string json = JsonSerializer.Serialize<NewMeetingRequest.NewMeetingRequestModel>(newMeetingRequestModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient _httpClient = new HttpClient())
            {

                _httpClient.DefaultRequestHeaders.Add("X-Version", "1");
                _httpClient.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token.Value);
                var response = await _httpClient.PostAsync("https://api.zoom.us/v2/users/5DKFTtH9RU6BhYccXk94NQ/meetings", content);
                if (response.IsSuccessStatusCode)
                {
                    //201 created 
                    //409 conflict
                    //401 unautherize

                    var responseContent = await response.Content.ReadAsStringAsync();
                    newUserResponse = JsonSerializer.Deserialize<NewMeetingResponseModel>(responseContent);

                }

            }
            return newUserResponse;
        }
    }
}
