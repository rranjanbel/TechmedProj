using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using TechMed.API.NotificationHub;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.TwilioAPI.Service;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;
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
        private readonly ILogger<VideoCallController> _logger;
        private bool CanCallByPHC = true;
        private readonly IPatientCaseRepository _patientCaseRepository;
        public VideoCallController(IMapper mapper,
            ITwilioVideoSDKService twilioVideoSDK,
            ITwilioMeetingRepository twilioRoomDb,
            IHubContext<SignalRBroadcastHub,
                IHubClient> hubContext,
            ILogger<VideoCallController> logger,
            IPatientCaseRepository patientCaseRepository)
        {
            _twilioVideoSDK = twilioVideoSDK;
            _hubContext = hubContext;
            _twilioRoomDb = twilioRoomDb;
            _logger = logger;
            _patientCaseRepository = patientCaseRepository; 
        }

        [HttpGet("token")]
        public IActionResult GetToken()
        {
            return new JsonResult(new { token = _twilioVideoSDK.GetTwilioJwt(User.Identity.Name) });
        }


        [HttpPost("begindialingcalltouser")]
        public async Task<IActionResult> BeginDialingCallToUser([Required][FromQuery] Int64 patientCaseId)
        {
            //Check user role who is calling
            var user = User.Identity.Name;
            bool role = User.IsInRole("PHCUser");
            if (role)            
                CanCallByPHC = true;      
            else          
                CanCallByPHC = false;

            //Add some time in call status check
            bool value = await _patientCaseRepository.UpdateCallStatusTime(patientCaseId);


            ApiResponseModel<int> apiResponseModel = new ApiResponseModel<int>();
            var patientInfo = await _twilioRoomDb.PatientQueueGet(patientCaseId);
            
            if (patientInfo == null)
            {
                apiResponseModel.isSuccess = false;
                return BadRequest(apiResponseModel);
            }


            //need to check availability of doctor by checkin in queue and other           
            
            // Check Doctor is free to receive the call
            bool isDoctorBusy = false;
            isDoctorBusy = await _patientCaseRepository.IsDoctorFreeToReceiveCall(patientCaseId);

            if (isDoctorBusy) //is enduser available to have call
            {
                PatientCase patientCase = await  _patientCaseRepository.GetByID(patientCaseId);
                apiResponseModel.isSuccess = true;
                apiResponseModel.data = patientCase.PatientId;
                await _hubContext.Clients.All.BroadcastMessage(new SignalRNotificationModel()
                {
                    receiverEmail = CanCallByPHC ? patientInfo.AssignedDoctor.User.Email : patientInfo.AssignedByNavigation.User.Email,
                    senderEmail = CanCallByPHC ? patientInfo.AssignedByNavigation.User.Email : patientInfo.AssignedDoctor.User.Email,
                    message = CanCallByPHC ? patientInfo.AssignedByNavigation.Phcname : patientInfo.AssignedDoctor.User.Name,
                    messageType = enumSignRNotificationType.BeginDialingCall.ToString(),
                    patientCaseId = patientCaseId
                   
                });
            }
            else
            {
                apiResponseModel.isSuccess = false;

            }
            return Ok(apiResponseModel);

        }


        [HttpGet("connecttomeetingroom")]
        public async Task<IActionResult> ConnectToMeetingRoom([Required][FromQuery] int patientCaseId, [Required][FromQuery] string meetingInstance, [Required][FromQuery] bool isAccepter)
        {
            var user = User.Identity.Name;
            bool role = User.IsInRole("PHCUser");
            if (role)
                CanCallByPHC = true;
            else
                CanCallByPHC = false;

            ApiResponseModel<int> apiResponseModel = new ApiResponseModel<int>();
            PatientCase patientCase = await _patientCaseRepository.GetByID(patientCaseId);
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
                var patientInfo = await _twilioRoomDb.PatientQueueGet(patientCaseId);
                // if ((patientCaseInfo == null && CanCallByPHC && isDoctor) || (patientCaseInfo == null && !CanCallByPHC && !isDoctor))

                if ((patientCaseInfo == null && CanCallByPHC && isAccepter) || (patientCaseInfo == null && !CanCallByPHC && !isAccepter))
                {
                    var roomFromTwilio = await _twilioVideoSDK.CreateRoomsAsync(meetingInstance, callBackUrlForTwilio);
                    var isSaved = await _twilioRoomDb.MeetingRoomInfoAdd(new TwilioMeetingRoomInfo()
                    {
                        MeetingSid = roomFromTwilio.Sid,
                        RoomName = roomFromTwilio.UniqueName,
                        PatientCaseId = patientCaseId,
                        RoomStatusCallback = roomFromTwilio.StatusCallback.ToString(),
                        TwilioRoomStatus = roomFromTwilio.Status.ToString(),
                        AssignedBy = patientInfo.AssignedBy,
                        AssignedDoctorId=patientInfo.AssignedDoctorId,
                        
                    });
                    apiResponseModel.isSuccess = true;
                    apiResponseModel.data = patientCase.PatientId;

                    await _hubContext.Clients.All.BroadcastMessage(new SignalRNotificationModel()
                    {
                        receiverEmail = CanCallByPHC ? patientInfo.AssignedByNavigation.User.Email : patientInfo.AssignedDoctor.User.Email ,
                        senderEmail = CanCallByPHC ? patientInfo.AssignedDoctor.User.Email : patientInfo.AssignedByNavigation.User.Email ,
                        message = "",
                        messageType = enumSignRNotificationType.NotifyParticipientToJoin.ToString(),
                        patientCaseId = patientCaseId,
                        roomName = meetingInstance
                       
                    });


                    return Ok(apiResponseModel);
                }
                else if (patientCaseInfo == null)
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
                    apiResponseModel.data = patientCase.PatientId;
                    return Ok(apiResponseModel);
                }
            }
            catch (Exception ex)
            {
                apiResponseModel.isSuccess = false;
                apiResponseModel.errorMessage = ex.Message;
                _logger.LogError("Exception in IsValidRoomDoctor API " + ex);
                return BadRequest(apiResponseModel);
            }



        }



        //[HttpGet("isvalidroomdoctor")]
        //public async Task<IActionResult> IsValidRoomDoctor([Required][FromQuery] int patientCaseId, [Required][FromQuery] string meetingInstance)
        //{
        //    ApiResponseModel<bool> apiResponseModel = new ApiResponseModel<bool>();
        //    string callBackUrlForTwilio = string.Format("{0}://{1}{2}/api/webhookcallback/twilioroomstatuscallback", Request.Scheme, Request.Host.Value, Request.PathBase);
        //    try
        //    {

        //        if (patientCaseId == 0 || string.IsNullOrEmpty(meetingInstance))
        //        {
        //            apiResponseModel.isSuccess = false;
        //            apiResponseModel.errorMessage = "Invalid Patient Or Call Info";
        //            return BadRequest(apiResponseModel);
        //        }
        //        var patientCaseInfo = await _twilioRoomDb.MeetingRoomInfoGet(meetingInstance);
        //        if (patientCaseInfo == null)
        //        {
        //            var roomFromTwilio = await _twilioVideoSDK.CreateRoomsAsync(meetingInstance, callBackUrlForTwilio);

        //            var isSaved = await _twilioRoomDb.MeetingRoomInfoAdd(new TwilioMeetingRoomInfo()
        //            {
        //                MeetingSid = roomFromTwilio.Sid,
        //                RoomName = roomFromTwilio.UniqueName,
        //                PatientCaseId = patientCaseId,
        //                RoomStatusCallback = roomFromTwilio.StatusCallback.ToString(),
        //                TwilioRoomStatus = roomFromTwilio.Status.ToString()
        //            });
        //            apiResponseModel.isSuccess = true;
        //            apiResponseModel.data = true;
        //            return Ok(apiResponseModel);
        //        }
        //        else
        //        {


        //            var roomFromTwilio = await _twilioVideoSDK.GetRoomDetailFromTwilio(patientCaseInfo.MeetingSid);
        //            if (roomFromTwilio == null)
        //            {
        //                await _twilioRoomDb.MeetingRoomCloseFlagUpdate(patientCaseInfo.Id, true);
        //                apiResponseModel.isSuccess = false;
        //                apiResponseModel.errorMessage = "Room Closed";
        //                return BadRequest(apiResponseModel);
        //            }
        //            else if (roomFromTwilio.Status != RoomResource.RoomStatusEnum.InProgress)
        //            {
        //                await _twilioRoomDb.MeetingRoomCloseFlagUpdate(patientCaseInfo.Id, true);
        //                apiResponseModel.isSuccess = false;
        //                apiResponseModel.errorMessage = "Room Closed";
        //                return BadRequest(apiResponseModel);
        //            }
        //            apiResponseModel.isSuccess = true;
        //            apiResponseModel.data = true;
        //            return Ok(apiResponseModel);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        apiResponseModel.isSuccess = false;
        //        apiResponseModel.errorMessage = ex.Message;
        //        _logger.LogError("Exception in IsValidRoomDoctor API " + ex);
        //        return BadRequest(apiResponseModel);
        //    }



        //}


        //[HttpGet("isvalidroomphc")]
        //public async Task<IActionResult> IsValidRoomPhc([Required][FromQuery] int patientCaseId, [Required][FromQuery] string meetingInstance)
        //{
        //    ApiResponseModel<bool> apiResponseModel = new ApiResponseModel<bool>();
        //    string callBackUrlForTwilio = string.Format("{0}://{1}/api/callbackhandler/meetingstatus", Request.Scheme, Request.Host.Value);
        //    try
        //    {

        //        if (patientCaseId == 0 || string.IsNullOrEmpty(meetingInstance))
        //        {
        //            apiResponseModel.isSuccess = false;
        //            apiResponseModel.errorMessage = "Invalid Patient Or Call Info";
        //            return BadRequest(apiResponseModel);
        //        }
        //        var patientCaseInfo = await _twilioRoomDb.MeetingRoomInfoGet(meetingInstance);

        //        if (patientCaseInfo == null)
        //        {
        //            apiResponseModel.isSuccess = false;
        //            apiResponseModel.errorMessage = "Invalid Patient Or Call Info";
        //            return BadRequest(apiResponseModel);
        //        }
        //        else
        //        {
        //            var roomFromTwilio = await _twilioVideoSDK.GetRoomDetailFromTwilio(patientCaseInfo.MeetingSid);
        //            if (roomFromTwilio == null)
        //            {
        //                await _twilioRoomDb.MeetingRoomCloseFlagUpdate(patientCaseInfo.Id, true);
        //                apiResponseModel.isSuccess = false;
        //                apiResponseModel.errorMessage = "Room Closed";
        //                return BadRequest(apiResponseModel);
        //            }
        //            else if (roomFromTwilio.Status != RoomResource.RoomStatusEnum.InProgress)
        //            {
        //                await _twilioRoomDb.MeetingRoomCloseFlagUpdate(patientCaseInfo.Id, true);
        //                apiResponseModel.isSuccess = false;
        //                apiResponseModel.errorMessage = "Room Closed";
        //                return BadRequest(apiResponseModel);
        //            }
        //            apiResponseModel.isSuccess = true;
        //            apiResponseModel.data = true;
        //            return Ok(apiResponseModel);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        apiResponseModel.isSuccess = false;
        //        apiResponseModel.errorMessage = ex.Message;
        //        _logger.LogError("Exception in IsValidRoomPhc API " + ex);
        //        return BadRequest(apiResponseModel);
        //    }



        //}


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



        [HttpPost("dismisscall")]
        public async Task<IActionResult> DismissCall([Required][FromForm] string roomInstance, [Required][FromForm] int patientCaseId,[Required][FromForm] bool isPartiallyClosed)
        {
            ApiResponseModel<dynamic> apiResponseModel = new ApiResponseModel<dynamic>();
            string callBackUrlForTwilio = string.Format("{0}://{1}{2}/api/webhookcallback/twiliocomposevideostatuscallback", Request.Scheme, Request.Host.Value, Request.PathBase);
            try
            {
                //var patientInfo = await _twilioRoomDb.PatientQueueGet(patientCaseId);
                var patientInfo = await _twilioRoomDb.PatientQueueAfterTretment(patientCaseId, isPartiallyClosed);
                var roomInfo = await _twilioRoomDb.MeetingRoomInfoGet(roomInstance);
                if (patientInfo == null || roomInfo == null)
                {
                    apiResponseModel.isSuccess = false;
                    apiResponseModel.errorMessage = "Invalid Information";
                   return BadRequest(apiResponseModel);
                }
                try
                {
                    var roomInfoFromTwilio = await _twilioVideoSDK.CloseRoomAsync(roomInfo.MeetingSid);
                    var composeVideo = await _twilioVideoSDK.ComposeVideo(roomInfoFromTwilio.Sid, callBackUrlForTwilio);
                    await _twilioRoomDb.MeetingRoomComposeVideoUpdate(composeVideo, roomInstance);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception in DismissCall API when call close room SDK. " + ex);
                }
                await _twilioRoomDb.SetMeetingRoomClosed(roomInstance, isPartiallyClosed);


                await _hubContext.Clients.All.BroadcastMessage(new SignalRNotificationModel()
                {
                    receiverEmail = patientInfo.AssignedDoctor.User.Email,
                    senderEmail = patientInfo.AssignedByNavigation.User.Email,
                    message = "",
                    messageType = enumSignRNotificationType.NotifyParticipientToExit.ToString(),
                    patientCaseId = patientCaseId,
                    roomName = roomInstance
                });

                await _hubContext.Clients.All.BroadcastMessage(new SignalRNotificationModel()
                {
                    receiverEmail = patientInfo.AssignedByNavigation.User.Email,
                    senderEmail = patientInfo.AssignedDoctor.User.Email,
                    message = "",
                    messageType = enumSignRNotificationType.NotifyParticipientToExit.ToString(),
                    patientCaseId = patientCaseId,
                    roomName = roomInstance
                });

                apiResponseModel.isSuccess = true;
                return Ok(apiResponseModel);
            }
            catch (Exception ex)
            {
                apiResponseModel.isSuccess = false;
                apiResponseModel.errorMessage = ex.Message;
                _logger.LogError("Exception in DismissCall API " + ex);
                return BadRequest(apiResponseModel);
            }
        }

    }
}
