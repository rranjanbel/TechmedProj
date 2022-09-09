using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.ZoomAPI.Model;
using TechMed.BL.ZoomAPI.Service;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoomController : ControllerBase
    {
        readonly IZoomAccountService _zoomAccountService;
        readonly IZoomUserService _zoomUserService;
        readonly IZoomMeetingService _zoomMeetingService;
        readonly IZoomRecordingService _zoomRecordingService;
        private readonly ILogger<ZoomController> _logger;
        public ZoomController(IZoomRecordingService zoomRecordingService,IZoomAccountService zoomAccountService, IZoomUserService zoomUserService, IZoomMeetingService zoomMeetingService, ILogger<ZoomController> logger)
        {
            _zoomAccountService = zoomAccountService;
            _zoomUserService = zoomUserService;
            _zoomMeetingService = zoomMeetingService;
            _zoomRecordingService = zoomRecordingService;
            _logger = logger;
        }
        [HttpPost("GetToken")]
        public async Task<IActionResult> GetToken()
        {
            try
            {
                _logger.LogInformation("Received GetToken ");

                string str = await _zoomAccountService.GetTokenAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetToken API " + ex);
                throw;
            }

        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser()
        {
            try
            {
                _logger.LogInformation("Received GetToken ");
                NewUserRequestModel newUserRequestModel = new NewUserRequestModel();
                newUserRequestModel.action = "create";
                newUserRequestModel.user_info.type = 1;
                newUserRequestModel.user_info.first_name = "Jitendra";
                newUserRequestModel.user_info.last_name = "Kumar";
                newUserRequestModel.user_info.email = "yjit73@gmail.com";
                newUserRequestModel.user_info.password = "Zoom@12345";



                var newUserResponse = await _zoomUserService.CreateUser(newUserRequestModel);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetToken API " + ex);
                throw;
            }

        }

        [HttpPost("GetUser")]
        public async Task<IActionResult> GetUser(string Email)
        {
            try
            {
                var newUserResponse = await _zoomUserService.GetUser(Email);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetToken API " + ex);
                throw;
            }

        }
        [HttpPost("CreateNewMeeting")]
        public async Task<IActionResult> CreateNewMeeting(string HostUserMailID)
        {
            try
            {
                NewMeetingRequest.NewMeetingRequestModel newMeetingRequestModel = new NewMeetingRequest.NewMeetingRequestModel();
                newMeetingRequestModel.action = "create";
                newMeetingRequestModel.user_info.email = HostUserMailID.Trim();
                newMeetingRequestModel.user_info.first_name = "FName";
                newMeetingRequestModel.user_info.last_name = "LName";
                newMeetingRequestModel.user_info.password = "Zoom@12345";
                newMeetingRequestModel.user_info.type = 1;
                newMeetingRequestModel.user_info.plan_united_type = "512";
                newMeetingRequestModel.user_info.feature.zoom_one_type = 16;
                newMeetingRequestModel.user_info.feature.zoom_phone =false;

                _logger.LogInformation("Received GetToken ");
                //{
                //"action": "create",
                //"user_info": {
                //   "email": "yjit73@gmail.com",
                //  "first_name": "Jill",
                //  "last_name": "Chill",
                //  "password": "jit@250357",
                //  "type": 1,
                //  "feature": {
                //    "zoom_phone": false,
                //    "zoom_one_type": 16
                //  },
                //  "plan_united_type": "512"
                //}
                //}


                var newUserResponse = await _zoomMeetingService.NewMeeting(newMeetingRequestModel);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetToken API " + ex);
                throw;
            }

        }
        [HttpPost("GetRecording")]
        public async Task<IActionResult> GetRecording(string MeetingID)
        {
            try
            {
                var newUserResponse = await _zoomRecordingService.GetRecording(MeetingID);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetToken API " + ex);
                throw;
            }

        }

    }
}
