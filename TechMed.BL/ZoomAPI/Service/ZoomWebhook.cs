using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.DL.Models;

namespace TechMed.BL.ZoomAPI.Service
{
    public class ZoomWebhook : IZoomWebhook
    {
        readonly TeleMedecineContext _teleMedecineContext;
        public ZoomWebhook(TeleMedecineContext teleMedecineContext)
        {
            _teleMedecineContext = teleMedecineContext;
        }
        public async Task<bool> ZoomWebhookService(string value)
        {
            _teleMedecineContext.ZoomWebHookEvents.Add(new ZoomWebHookEvent
            { CreatedOn = UtilityMaster.GetLocalDateTime(), RequestValue = value }
                );
            await _teleMedecineContext.SaveChangesAsync();
            //var content = request.Content;
            //string jsonContent = content.ReadAsStringAsync().Result;
            return true;
        }
    }
}
