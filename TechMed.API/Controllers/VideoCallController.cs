using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using TechMed.API.NotificationHub;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.TwilioAPI.Service;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;
using Twilio.Base;
using Twilio.Rest.Video.V1;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoCallController : ControllerBase
    {
        readonly ITwilioVideoSDKService _twilioVideoSDK;
        private readonly IHubContext<SignalRBroadcastHub, IHubClient> _hubContext;
        private readonly ITwilioMeetingRepository _twilioRoomDb;

        public VideoCallController(IMapper mapper,
            ITwilioVideoSDKService twilioVideoSDK,
            ITwilioMeetingRepository twilioRoomDb,
            IHubContext<SignalRBroadcastHub,
                IHubClient> hubContext)
        {
            _twilioVideoSDK = twilioVideoSDK;
            _hubContext = hubContext;
            _twilioRoomDb = twilioRoomDb;
        }

        [HttpGet("token")]
        public IActionResult GetToken()
        {
            return new JsonResult(new { token = _twilioVideoSDK.GetTwilioJwt(User.Identity.Name) });
        }

        [HttpGet("isvalidroomdoctor")]
        public async Task<IActionResult> IsValidRoomDoctor([Required][FromQuery] int patientCaseId, [Required][FromQuery] string meetingInstance)
        {
            ApiResponseModel<bool> apiResponseModel = new ApiResponseModel<bool>();
            string callBackUrlForTwilio = string.Format("{0}://{1}{2}/api/webhookcallback/twilioroomstatuscallback", Request.Scheme, Request.Host.Value, Request.PathBase);
            try
            {

                if (patientCaseId == 0 || string.IsNullOrEmpty(meetingInstance))
                {
                    apiResponseModel.isSuccess = false;
                    apiResponseModel.errorMessage = "Invalid Patient Or Call Info";
                    return BadRequest(apiResponseModel);
                }
                var patientCaseInfo = await _twilioRoomDb.MeetingRoomInfoGet(meetingInstance);
                if (patientCaseInfo == null)
                {
                    var roomFromTwilio = await _twilioVideoSDK.CreateRoomsAsync(meetingInstance, callBackUrlForTwilio);

                    var isSaved = await _twilioRoomDb.MeetingRoomInfoAdd(new TwilioMeetingRoomInfo()
                    {
                        MeetingSid = roomFromTwilio.Sid,
                        RoomName = roomFromTwilio.UniqueName,
                        PatientCaseId = patientCaseId,
                        RoomStatusCallback = roomFromTwilio.StatusCallback.ToString(),
                        TwilioRoomStatus = roomFromTwilio.Status.ToString()
                    });
                    apiResponseModel.isSuccess = true;
                    apiResponseModel.data = true;
                    return Ok(apiResponseModel);
                }
                else
                {


                    var roomFromTwilio = await _twilioVideoSDK.GetRoomDetailFromTwilio(patientCaseInfo.MeetingSid);
                    if (roomFromTwilio == null)
                    {
                        await _twilioRoomDb.MeetingRoomCloseFlagUpdate(patientCaseInfo.Id, true);
                        apiResponseModel.isSuccess = false;
                        apiResponseModel.errorMessage = "Room Closed";
                        return BadRequest(apiResponseModel);
                    }
                    else if (roomFromTwilio.Status != RoomResource.RoomStatusEnum.InProgress)
                    {
                        await _twilioRoomDb.MeetingRoomCloseFlagUpdate(patientCaseInfo.Id, true);
                        apiResponseModel.isSuccess = false;
                        apiResponseModel.errorMessage = "Room Closed";
                        return BadRequest(apiResponseModel);
                    }
                    apiResponseModel.isSuccess = true;
                    apiResponseModel.data = true;
                    return Ok(apiResponseModel);
                }
            }
            catch (Exception ex)
            {
                apiResponseModel.isSuccess = false;
                apiResponseModel.errorMessage = ex.Message;
                return BadRequest(apiResponseModel);
            }



        }


        [HttpGet("isvalidroomphc")]
        public async Task<IActionResult> IsValidRoomPhc([Required][FromQuery] int patientCaseId, [Required][FromQuery] string meetingInstance)
        {
            ApiResponseModel<bool> apiResponseModel = new ApiResponseModel<bool>();
            string callBackUrlForTwilio = string.Format("{0}://{1}/api/callbackhandler/meetingstatus", Request.Scheme, Request.Host.Value);
            try
            {

                if (patientCaseId == 0 || string.IsNullOrEmpty(meetingInstance))
                {
                    apiResponseModel.isSuccess = false;
                    apiResponseModel.errorMessage = "Invalid Patient Or Call Info";
                    return BadRequest(apiResponseModel);
                }
                var patientCaseInfo = await _twilioRoomDb.MeetingRoomInfoGet(meetingInstance);

                if (patientCaseInfo == null)
                {
                    apiResponseModel.isSuccess = false;
                    apiResponseModel.errorMessage = "Invalid Patient Or Call Info";
                    return BadRequest(apiResponseModel);
                }
                else
                {
                    var roomFromTwilio = await _twilioVideoSDK.GetRoomDetailFromTwilio(patientCaseInfo.MeetingSid);
                    if (roomFromTwilio == null)
                    {
                        await _twilioRoomDb.MeetingRoomCloseFlagUpdate(patientCaseInfo.Id, true);
                        apiResponseModel.isSuccess = false;
                        apiResponseModel.errorMessage = "Room Closed";
                        return BadRequest(apiResponseModel);
                    }
                    else if (roomFromTwilio.Status != RoomResource.RoomStatusEnum.InProgress)
                    {
                        await _twilioRoomDb.MeetingRoomCloseFlagUpdate(patientCaseInfo.Id, true);
                        apiResponseModel.isSuccess = false;
                        apiResponseModel.errorMessage = "Room Closed";
                        return BadRequest(apiResponseModel);
                    }
                    apiResponseModel.isSuccess = true;
                    apiResponseModel.data = true;
                    return Ok(apiResponseModel);
                }
            }
            catch (Exception ex)
            {
                apiResponseModel.isSuccess = false;
                apiResponseModel.errorMessage = ex.Message;
                return BadRequest(apiResponseModel);
            }



        }


        //[HttpGet("rooms")]
        //public async Task<IActionResult> GetRooms()
        //    => new JsonResult(await _videoService.GetAllRoomsAsync());

        //[HttpPost("create-room-with-recording")]
        //public async Task<RoomResource> CreateRoomsAsync(string roomname, string callBackUrl)
        //{
        //    return await _videoService.CreateRoomsAsync(roomname, callBackUrl);
        //}
        //[HttpGet("get-completed-Compose")]
        //public async Task<ResourceSet<CompositionResource>> GetAllCompletedComposition()
        //{
        //    return await _videoService.GetAllCompletedComposition();
        //}
        //[HttpGet("get-completed-callrecord")]
        //public async Task<List<RoomResource>> GetAllCompletedCall()
        //{
        //    return await _videoService.GetAllCompletedCall();
        //}
        //[HttpGet("compose-video")]
        //public async Task<CompositionResource> ComposeVideo(string roomSid, string callBackUrl)
        //{
        //    return await _videoService.ComposeVideo(roomSid, callBackUrl);
        //}
        //[HttpGet("download-video")]
        //public async Task<string> DownloadComposeVideo(string compositionSid)
        //{
        //    return await _videoService.DownloadComposeVideo(compositionSid);
        //}
        //[HttpGet("delete-video")]
        //public async Task<bool> DeleteComposeVideo(string compositionSid)
        //{
        //    return await _videoService.DeleteComposeVideo(compositionSid);
        //}

        //[HttpGet("getroom-sid")]
        //public async Task<string> GetRoomSid(string roomName)
        //{
        //    return await _videoService.GetRoomSid(roomName);
        //}
        //[HttpGet("end-video-call")]
        //public async Task<RoomResource> EndVideoCall(string roomName)
        //{
        //    return await _videoService.EndVideoCall(roomName);
        //}


        [HttpPost("callingbypatientcaseid")]
        public async Task<IActionResult> CallingByPatientCaseId([Required][FromForm] int patientCaseId)
        {
            ApiResponseModel<bool> apiResponseModel = new ApiResponseModel<bool>();


            var patientInfo = await _twilioRoomDb.PatientQueueGet(patientCaseId);
            if (patientInfo == null)
            {
                apiResponseModel.isSuccess = false;
                apiResponseModel.errorMessage = "Invalid Patient Case";
                return BadRequest(apiResponseModel);
            }
            await _hubContext.Clients.All.BroadcastMessage(new SignalRNotificationModel()
            {
                receiverEmail = patientInfo.AssignedByNavigation.User.Email,
                senderEmail = User.Identity.Name,
                message = "Call Initiate",
                messageType = enumSignRNotificationType.CallingToPHC.ToString(),
                patientCaseId = patientCaseId
            });
            apiResponseModel.isSuccess = true;
            return Ok(apiResponseModel);
        }


        [HttpPost("dismisscall")]
        public async Task<IActionResult> DismissCall([Required][FromForm] string roomInstance, [Required][FromForm] int patientCaseId)
        {
            ApiResponseModel<dynamic> apiResponseModel = new ApiResponseModel<dynamic>();
            string callBackUrlForTwilio = string.Format("{0}://{1}{2}/api/webhookcallback/twiliocomposevideostatuscallback", Request.Scheme, Request.Host.Value, Request.PathBase);
            try
            {
                var queueInfo = await _twilioRoomDb.PatientQueueGet(patientCaseId);
                var roomInfo = await _twilioRoomDb.MeetingRoomInfoGet(roomInstance);
                if (queueInfo == null || roomInfo == null)
                {
                    apiResponseModel.isSuccess = false;
                    apiResponseModel.errorMessage = "Invalid Information";
                    BadRequest(apiResponseModel);
                }
                try
                {
                    var roomInfoFromTwilio = await _twilioVideoSDK.CloseRoomAsync(roomInstance);
                    var composeVideo = await _twilioVideoSDK.ComposeVideo(roomInfoFromTwilio.Sid, callBackUrlForTwilio);
                    await _twilioRoomDb.MeetingRoomComposeVideoUpdate(composeVideo, roomInstance);
                }
                catch (Exception ex)
                {

                }
                await _twilioRoomDb.SetMeetingRoomClosed(roomInstance);
               

                apiResponseModel.data = new
                {
                    patientId = queueInfo.PatientCase.PatientId,
                    patientCaseID = patientCaseId,
                    toUser = queueInfo.AssignedByNavigation.User.Email
                };
                apiResponseModel.isSuccess = true;
                return Ok(apiResponseModel);
            }
            catch (Exception ex)
            {
                apiResponseModel.isSuccess = false;
                apiResponseModel.errorMessage = ex.Message;
                return BadRequest(apiResponseModel);
            }
        }

    }
}
