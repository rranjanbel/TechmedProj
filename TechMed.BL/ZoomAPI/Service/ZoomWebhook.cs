using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.ZoomAPI.Model;
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
            ZoomWebHookEvent zoomWebHookEvent = new ZoomWebHookEvent { CreatedOn = UtilityMaster.GetLocalDateTime(), RequestValue = value };
            _teleMedecineContext.ZoomWebHookEvents.Add(zoomWebHookEvent);
            await _teleMedecineContext.SaveChangesAsync();
            //var content = request.Content;
            //string jsonContent = content.ReadAsStringAsync().Result;
            var myDeserializedClass = JsonSerializer.Deserialize<ZoomWebHookResponseModel>(value);
            zoomWebHookEvent.EventName = myDeserializedClass.@event;
            await _teleMedecineContext.SaveChangesAsync();
            return true;
        }
    }
}
