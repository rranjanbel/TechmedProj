using Microsoft.AspNetCore.Mvc;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.TwilioAPI.Service;
using TechMed.BL.ViewModels;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHookCallbackController : Controller
    {
        readonly ITwilioVideoSDKService _twilioVideoSDK;
        private readonly ITwilioMeetingRepository _twilioRoomDb;
        private readonly ILogger<WebHookCallbackController> _logger;
        public WebHookCallbackController(
            ITwilioVideoSDKService twilioVideoSDK,
            ITwilioMeetingRepository twilioRoomDb,
            ILogger<WebHookCallbackController> logger)
        {
            _twilioVideoSDK = twilioVideoSDK;
            _twilioRoomDb = twilioRoomDb;
            _logger = logger;
        }
        [HttpPost("twilioroomstatuscallback")]
        public async Task<IActionResult> TwilioRoomStatusCallback([FromForm] RoomStatusRequest roomStatusRequest)
        {
            _logger.LogInformation("TwilioRoomStatusCallback, Received TwilioRoomStatusCallback", roomStatusRequest);
            try
            {
                _logger.LogInformation("TwilioRoomStatusCallback, Checking..  Is room null or empty?" + roomStatusRequest.RoomSid+" "+roomStatusRequest.RoomName + " " +  roomStatusRequest.AccountSid + " " + roomStatusRequest.RoomStatus);
                if (!string.IsNullOrEmpty(roomStatusRequest.RoomName) &&  !string.IsNullOrEmpty(roomStatusRequest.RoomStatus) && roomStatusRequest.RoomStatus.ToLower() == "completed")
                {

                    _logger.LogInformation("TwilioRoomStatusCallback, Isnull room check false" + roomStatusRequest.RoomName,roomStatusRequest.RoomType);
                    await _twilioRoomDb.UpdateRoomStatusFromTwilioWebHook(roomStatusRequest);

                    try
                    {
                        string callBackUrlForTwilio = string.Format("{0}://{1}{2}/api/webhookcallback/twiliocomposevideostatuscallback", Request.Scheme, Request.Host.Value, Request.PathBase);
                        var composeVideo = await _twilioVideoSDK.ComposeVideo(roomStatusRequest.RoomSid, callBackUrlForTwilio);
                        await _twilioRoomDb.MeetingRoomComposeVideoUpdate(composeVideo, roomStatusRequest.RoomName);

                    }
                    catch (Exception)
                    {
                    }


                    _logger.LogInformation("TwilioRoomStatusCallback, Room Status Request" + roomStatusRequest);
                    _logger.LogInformation("TwilioRoomStatusCallback Trigger Update");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in TwilioRoomStatusCallback API " + ex);
                throw;
            }
        }

        [HttpPost("twiliocomposevideostatuscallback")]
        public async Task<IActionResult> TwilioComposeVideoStatusCallback([FromForm] VideoCompositionStatusRequest videoCompositionStatusRequest)
        {
            try
            {
                _logger.LogInformation("Received twiliocomposevideostatuscallback, request model : " + videoCompositionStatusRequest.RoomSid+" "+ videoCompositionStatusRequest.MediaUri, videoCompositionStatusRequest.Size
                    , videoCompositionStatusRequest.StatusCallbackEvent, videoCompositionStatusRequest.CompositionSid, videoCompositionStatusRequest.CompositionUri, videoCompositionStatusRequest.Duration, videoCompositionStatusRequest.RoomSid);

                _logger.LogInformation("Received twiliocomposevideostatuscallback, SID: ", videoCompositionStatusRequest.RoomSid);
                if (!string.IsNullOrEmpty(videoCompositionStatusRequest.RoomSid))
                {
                    //await _twilioRoomDb.UpdateComposeVideoStatusFromTwilioWebHook(videoCompositionStatusRequest);
                    _logger.LogInformation("twiliocomposevideostatuscallback Trigger Update");
               
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in TwilioComposeVideoStatusCallback API " + ex);
                throw;
            }

        }
    }
}
