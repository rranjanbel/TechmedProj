using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.ZoomAPI.Model;
using TechMed.DL.Models;

namespace TechMed.BL.ZoomAPI.Service
{
    public class ZoomAccountService : IZoomAccountService
    {
        readonly ZoomSettings _zoomSettings;
        readonly TeleMedecineContext _telemedecineContext;
        public ZoomAccountService(TeleMedecineContext telemedecineContext, Microsoft.Extensions.Options.IOptions<ZoomSettings> zoomOptions)
        {
            _zoomSettings =
                zoomOptions?.Value
             ?? throw new ArgumentNullException(nameof(zoomOptions));
            _telemedecineContext = telemedecineContext;
        }

        public async Task<string> GetIssuedTokenAsync()
        {
            var token = await _telemedecineContext.ZoomTokens.FirstOrDefaultAsync();
            if (token == null)
            {
                throw new Exception("Token not found in database!");
            }
            else
            {
                if (token.ActiveTokenNumber == 1)
                {
                    return token.Token1;
                }
                else if (token.ActiveTokenNumber == 2)
                {
                    return token.Token2;
                }
                else
                {
                    throw new Exception("ActiveTokenNumber value is not in '1' or '2' in database!");
                }
            }
           
            //string token = await GetNewTokenFromZoomAsync();
            //return token;
        }

        public async Task<string> GetNewTokenFromZoomAsync(int index)
        {

            using (HttpClient _httpClient = new HttpClient())
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(_zoomSettings.ZoomSSClientID.Trim() + ":" + _zoomSettings.ZoomSSClientSecret.Trim());
                string AuthorizationString = Convert.ToBase64String(plainTextBytes);
                _httpClient.DefaultRequestHeaders.Add("X-Version", "1");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + AuthorizationString);
                //var company = JsonSerializer.Serialize(companyForCreation);
                //var requestContent = new StringContent(company, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://zoom.us/oauth/token?grant_type=account_credentials&account_id=" + _zoomSettings.ZoomSSAccountId + "&token_index=" + index.ToString(), null);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var zoomToken = JsonSerializer.Deserialize<ZoomTokenModel>(responseContent);
                    return zoomToken.access_token.ToString();
                }
            }
            return "";
        }



        public async Task<bool> RotateTokenAsync()
        {
            bool response = false;
            ZoomToken zoomToken = await _telemedecineContext.ZoomTokens.AsNoTracking().FirstOrDefaultAsync();
            if (zoomToken == null)
            {
                //insert first record
                //add token1
                string token1 = await GetNewTokenFromZoomAsync(0);
                zoomToken = new ZoomToken
                {
                    ActiveTokenNumber = 1,
                    Token1CreatedOn = UtilityMaster.GetLocalDateTime(),
                    Token1 = token1,
                    Token2CreatedOn = UtilityMaster.GetLocalDateTime(),
                    Token2 = token1
                };
                await _telemedecineContext.ZoomTokens.AddAsync(zoomToken);
                await _telemedecineContext.SaveChangesAsync();
            }
            else
            {
                if (zoomToken.ActiveTokenNumber == 1)
                {
                    // check time duration
                    // if more tha 50 min old
                    // create token2
                    // set ActiveTokenNumber 2 
                    var result = UtilityMaster.GetLocalDateTime().Subtract(zoomToken.Token1CreatedOn).TotalMinutes;
                    if (result > 20)
                    {
                        string token2 = await GetNewTokenFromZoomAsync(1);
                        zoomToken.Token2 = token2;
                        zoomToken.Token2CreatedOn = UtilityMaster.GetLocalDateTime();
                        zoomToken.ActiveTokenNumber = 2;
                        _telemedecineContext.ZoomTokens.Add(zoomToken);
                        _telemedecineContext.Entry(zoomToken).State = EntityState.Modified;
                        await _telemedecineContext.SaveChangesAsync();
                        return true;
                    }
                }
                if (zoomToken.ActiveTokenNumber == 2)
                {
                    // check time duration
                    // if more tha 50 min old
                    // create token1
                    // se1 ActiveTokenNumber 1
                    var result = UtilityMaster.GetLocalDateTime().Subtract(zoomToken.Token2CreatedOn).TotalMinutes;
                    if (result > 20)
                    {
                        string token1 = await GetNewTokenFromZoomAsync(0);
                        zoomToken.Token1 = token1;
                        zoomToken.Token1CreatedOn = UtilityMaster.GetLocalDateTime();
                        zoomToken.ActiveTokenNumber = 1;
                        _telemedecineContext.ZoomTokens.Add(zoomToken);
                        _telemedecineContext.Entry(zoomToken).State = EntityState.Modified;
                        await _telemedecineContext.SaveChangesAsync();
                        return true;
                    }
                }
            }
            return response;
        }
    }
}
