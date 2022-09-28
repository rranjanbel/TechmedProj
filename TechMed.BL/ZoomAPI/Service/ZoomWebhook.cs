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
        readonly IZoomRecordingService _zoomRecordingService;
        public ZoomWebhook(IZoomRecordingService zoomRecordingService, TeleMedecineContext teleMedecineContext, ITwilioMeetingRepository twilioMeetingRepository)
        {
            _teleMedecineContext = teleMedecineContext;
            _twilioMeetingRepository = twilioMeetingRepository;
            _zoomRecordingService = zoomRecordingService;
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
                                bool result = await _twilioMeetingRepository.SetMeetingRoomClosed(response.payload.@object.id.ToString(), true);

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
                //save recording
                var response = JsonSerializer.Deserialize<RecordingStoppedModel.Root>(value);
                if (response != null)
                {
                    if (response.payload.@object != null)
                    {
                        TwilioMeetingRoomInfo twilioMeetingRoomInfo = await _teleMedecineContext.TwilioMeetingRoomInfos.AsNoTracking().FirstOrDefaultAsync(a => a.MeetingSid == response.payload.@object.id.ToString());
                        if (twilioMeetingRoomInfo != null)
                        {
                            var recording = await _zoomRecordingService.GetRecording(response.payload.@object.id.ToString());
                            if (recording != null)
                            {
                                RecordingFile recordingFile = recording.recording_files.FirstOrDefault(a => a.file_type.ToLower() == "mp4");
                                if (recordingFile != null)
                                {
                                    await _twilioMeetingRepository.MeetingRoomComposeVideoUpdate(response.payload.@object.id.ToString(), recordingFile.file_size, recordingFile.download_url);
                                }
                            }
                            if (twilioMeetingRoomInfo.IsClosed == false)
                            {
                                bool result = await _twilioMeetingRepository.SetMeetingRoomClosed(response.payload.@object.id.ToString(), true);

                            }
                        }
                    }
                }

            }

            return true;
        }
    }
}
