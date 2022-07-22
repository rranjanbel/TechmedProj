using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.BaseClasses
{
    public class SystemHealthRepository : ISystemHealthRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        public SystemHealthRepository(TeleMedecineContext teleMedecineContext)
        {
            _teleMedecineContext = teleMedecineContext;
        }

        public async Task<bool> GetANGStatus()
        {
            var v = _teleMedecineContext.ServerHealths.ToList();
            bool result = await IsValidUri(new Uri("https://telemed-ang-dev.azurewebsites.net/"));
            return result;
        }

        public async Task<bool> GetAPIStatus()
        {
            bool result = await IsValidUri(new Uri("https://localhost:7043/api/SystemHealth/GetAPIStatus"));
            return result;

        }
        public async Task<bool> IsValidUri(Uri uri)
        {
            try
            {
                using (HttpClient Client = new HttpClient())
                {

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

        public async Task<bool> SaveStatusInDB(string APIHost,string ANGHost)
        {
            bool apir = await GetAPIStatus();
            bool angr = await GetANGStatus();
            if (apir&& angr)
            {
                //get staus URL
                //if
                //ServerHealth serverHealth=new ServerHealth({ 
                
                //});
            }
            throw new NotImplementedException();
        }
    }

}
