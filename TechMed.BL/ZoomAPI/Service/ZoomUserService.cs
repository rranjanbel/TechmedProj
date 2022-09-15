using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechMed.BL.ZoomAPI.Model;
using TechMed.DL.Models;

namespace TechMed.BL.ZoomAPI.Service
{
    public class ZoomUserService : IZoomUserService
    {
        readonly ZoomSettings _zoomSettings;
        readonly TeleMedecineContext _teleMedecineContext;
        //readonly ZoomAccountService _zoomAccountService;
        public ZoomUserService(/*Microsoft.Extensions.Options.IOptions<ZoomSettings> zoomOptions,*/ TeleMedecineContext teleMedecineContext)
        {
            _teleMedecineContext = teleMedecineContext;
            //_zoomSettings =
            //    zoomOptions?.Value
            // ?? throw new ArgumentNullException(nameof(zoomOptions));
            //_zoomAccountService = zoomAccountService;
        }
        public async Task<NewUserResponseModel> CreateUser(NewUserRequestModel newUser)
        {
            //string token =await _zoomAccountService.GetTokenAsync();
            var token = _teleMedecineContext.Configurations.Where(a => a.Name == "ZoomToken").FirstOrDefault();
            NewUserResponseModel newUserResponse = new NewUserResponseModel();
            string json = JsonSerializer.Serialize<NewUserRequestModel>(newUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient _httpClient = new HttpClient())
            {

                _httpClient.DefaultRequestHeaders.Add("X-Version", "1");
                _httpClient.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token.Value);
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + AuthorizationString);
                //var company = JsonSerializer.Serialize(companyForCreation);
                //var requestContent = new StringContent(company, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://api.zoom.us/v2/users", content);
                if (response.IsSuccessStatusCode)
                {
                    //201 created 
                    //409 conflict
                    //401 unautherize

                    var responseContent = await response.Content.ReadAsStringAsync();
                    newUserResponse = JsonSerializer.Deserialize<NewUserResponseModel>(responseContent);

                }

            }
            return newUserResponse;
        }

        public async Task<GetUserResponseModel> GetUser(string EmailID)
        {
            //string token =await _zoomAccountService.GetTokenAsync();
            var token = _teleMedecineContext.Configurations.Where(a => a.Name == "ZoomToken").FirstOrDefault();
            GetUserResponseModel newUserResponse = new GetUserResponseModel();

            using (HttpClient _httpClient = new HttpClient())
            {

                _httpClient.DefaultRequestHeaders.Add("X-Version", "1");
                _httpClient.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token.Value);
                var response = await _httpClient.GetAsync("https://api.zoom.us/v2/users/" + EmailID.Trim());
                if (response.IsSuccessStatusCode)
                {
                    //200 success response
                    //200 status=pending
                    //200 status=active
                    //404 not found
                    //401 unautherize

                    var responseContent = await response.Content.ReadAsStringAsync();
                    newUserResponse = JsonSerializer.Deserialize<GetUserResponseModel>(responseContent);

                }

            }
            return newUserResponse;
        }

        public async Task<bool> UpdateRecodingSetting(string ZoomUserID)
        {
            UpdateUserRecordingSettingModel model = new UpdateUserRecordingSettingModel();
            var token = _teleMedecineContext.Configurations.Where(a => a.Name == "ZoomToken").FirstOrDefault();
            string json = JsonSerializer.Serialize<UpdateUserRecordingSettingModel>(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient _httpClient = new HttpClient())
            {

                _httpClient.DefaultRequestHeaders.Add("X-Version", "1");
                _httpClient.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token.Value);
                var response = await _httpClient.PatchAsync("https://api.zoom.us/v2/users/" + ZoomUserID + "/settings", content);
                if (response.IsSuccessStatusCode)
                {
                    //204 updated 
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
    }
}
