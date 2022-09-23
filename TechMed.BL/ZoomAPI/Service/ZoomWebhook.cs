using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ZoomAPI.Model;
using TechMed.DL.Models;

namespace TechMed.BL.ZoomAPI.Service
{
    public class ZoomWebhook : IZoomWebhook
    {
        readonly TeleMedecineContext _teleMedecineContext;
        readonly ITwilioMeetingRepository _twilioMeetingRepository;
        public ZoomWebhook(TeleMedecineContext teleMedecineContext, ITwilioMeetingRepository twilioMeetingRepository)
        {
            _teleMedecineContext = teleMedecineContext;
            _twilioMeetingRepository = twilioMeetingRepository;
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
            if (myDeserializedClass.@event.ToLower() == "meeting.ended")
            {
                //clear queue
                var response = JsonSerializer.Deserialize<WebHookEndMeetingResponse.WebHookEndMeetingModel>(value);
                if (response != null)
                {
                    if (response.payload.@object != null)
                    {
                        TwilioMeetingRoomInfo twilioMeetingRoomInfo = await _teleMedecineContext.TwilioMeetingRoomInfos.AsNoTracking().FirstOrDefaultAsync(a => a.MeetingSid == response.payload.@object.id);
                        if (twilioMeetingRoomInfo != null)
                        {
                            if (twilioMeetingRoomInfo.IsClosed == false)
                            {
                                bool result = await _twilioMeetingRepository.SetMeetingRoomClosed(response.payload.@object.id, true);

                            }
                        }
                    }
                }

            }
            if (myDeserializedClass.@event.ToLower() == "meeting.participant_left")
            {
                //end meeting
            }
            if (myDeserializedClass.@event.ToLower() == "recording.stopped")
            {
                //clear queue
            }

            return true;
        }
    }
}
