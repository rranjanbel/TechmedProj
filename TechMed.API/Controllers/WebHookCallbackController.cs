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
        public async Task<IActionResult> TwilioRoomStatusCallback([FromQuery]RoomStatusRequest roomStatusRequest)
        {
            try
            {

                if (!string.IsNullOrEmpty(roomStatusRequest.RoomName))
                {
                    await _twilioRoomDb.UpdateRoomStatusFromTwilioWebHook(roomStatusRequest);
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
        public async Task<IActionResult> TwilioComposeVideoStatusCallback([FromQuery] VideoCompositionStatusRequest videoCompositionStatusRequest)
        {
            try
            {

                if (!string.IsNullOrEmpty(videoCompositionStatusRequest.RoomSid))
                {
                    await _twilioRoomDb.UpdateComposeVideoStatusFromTwilioWebHook(videoCompositionStatusRequest);
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
