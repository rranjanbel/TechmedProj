using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.BaseClasses
{
    public class SystemHealthRepository : ISystemHealthRepository, IDisposable
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        public SystemHealthRepository(TeleMedecineContext teleMedecineContext)
        {
            _teleMedecineContext = teleMedecineContext;
        }

        public void Dispose()
        {
            try
            {
                // _teleMedecineContext.Dispose();
            }
            finally
            {

            }
        }

        public async Task<bool> GetANGStatus(string ANGHost)
        {
            //var v = _teleMedecineContext.ServerHealths.ToList();
            bool result = await IsValidUri(new Uri(ANGHost));
            return result;
        }

        public async Task<bool> GetAPIStatus(string APIHost)
        {
            APIHost = APIHost + "/api/SystemHealth/GetAPIStatus";
            bool result = await IsValidUri(new Uri(APIHost));
            return result;

        }
        public async Task<bool> IsValidUri(Uri uri)
        {
            try
            {
                using (HttpClient Client = new HttpClient())
                {
                    //Client.Timeout = TimeSpan.FromSeconds(1);
                    HttpResponseMessage result = Client.GetAsync(uri).Result;
                    HttpStatusCode StatusCode = result.StatusCode;

                    switch (StatusCode)
                    {

                        case HttpStatusCode.Accepted:
                            return true;
                        case HttpStatusCode.OK:
                            return true;
                        default:
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> SaveStatusInDB(string APIHost, string ANGHost)
        {
            try
            {
                bool apir = await GetAPIStatus(APIHost);
                bool angr = await GetANGStatus(ANGHost);
                string Status = "Online";
                DateTime StartTime = UtilityMaster.GetLocalDateTime();
                DateTime EndTime = UtilityMaster.GetLocalDateTime();
                string Details = "Online";

                if (!apir || !angr)
                {
                    Status = "Offline";
                }
                if (!apir)
                {
                    Details = "API Offline";
                }
                if (!angr)
                {
                    Details = "ANG Offline";
                }
                if (!apir && !angr)
                {
                    Details = "Offline";
                }
                var Results = _teleMedecineContext.UpdateServerHealth.FromSqlInterpolated($"EXEC [dbo].[UpdateServerHealth] @StartTime ={StartTime}, @EndTime ={EndTime}, @Status ={Status},@Details={Details}");
                UpdateServerHealthVM data;
                foreach (var item in Results)
                {
                    data = new UpdateServerHealthVM();
                    data.Success = item.Success;
                }

            }
            catch (Exception ex)
            {

                return false;
            }
            return true;


        }
        public async Task<bool> UpdateLogout()
        {
            try
            {

                DateTime StartTime = UtilityMaster.GetLocalDateTime();
                DateTime EndTime = UtilityMaster.GetLocalDateTime();

                var Results = _teleMedecineContext.UpdateLogout.FromSqlInterpolated($"EXEC [dbo].[UpdateLogout] ");
                UpdateServerHealthVM data;
                foreach (var item in Results)
                {
                    data = new UpdateServerHealthVM();
                    data.Success = item.Success;
                }

            }
            catch (Exception ex)
            {

                return false;
            }
            return true;


        }
    }

}
