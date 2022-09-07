using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechMed.BL.ZoomAPI.Model;

namespace TechMed.BL.ZoomAPI.Service
{
    public class ZoomAccountService : IZoomAccountService
    {
        readonly ZoomSettings _zoomSettings;
        public ZoomAccountService(Microsoft.Extensions.Options.IOptions<ZoomSettings> zoomOptions)
        {
            _zoomSettings =
                zoomOptions?.Value
             ?? throw new ArgumentNullException(nameof(zoomOptions));
        }
        public async Task<string> GetTokenAsync()
        {
            //_zoomSettings.ZoomSSAccountId;
            //_zoomSettings.ZoomSSClientID;




            var values = new Dictionary<string, string>
                          {
                              { "thing1", "hello" },
                              { "thing2", "world" }
                          };

            var content = new FormUrlEncodedContent(values);
            using (HttpClient _httpClient = new HttpClient())
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(_zoomSettings.ZoomSSClientID.Trim() + ":" + _zoomSettings.ZoomSSClientSecret.Trim());
                string AuthorizationString = Convert.ToBase64String(plainTextBytes);
                _httpClient.DefaultRequestHeaders.Add("X-Version", "1");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + AuthorizationString);
                //var company = JsonSerializer.Serialize(companyForCreation);
                //var requestContent = new StringContent(company, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://zoom.us/oauth/token?grant_type=account_credentials&account_id=" + _zoomSettings.ZoomSSAccountId, null);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var zoomToken = JsonSerializer.Deserialize<ZoomTokenModel>(responseContent);
                    return zoomToken.access_token.ToString();
                }

            }
            return "";
        }
    }
}
