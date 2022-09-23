using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TechMed.BL.DTOMaster.Zoom;
using TechMed.BL.ZoomAPI.Model;
using TechMed.BL.ZoomAPI.Service;
using TechMed.DL.Models;

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
        readonly IZoomWebhook _zoomWebhook;
        readonly IZoomService _zoomService;
        readonly TeleMedecineContext _teleMedecineContext;
        private readonly ILogger<ZoomController> _logger;

        public ZoomController(TeleMedecineContext teleMedecineContext, IZoomService zoomService, IZoomWebhook zoomWebhook, IZoomRecordingService zoomRecordingService, IZoomAccountService zoomAccountService, IZoomUserService zoomUserService, IZoomMeetingService zoomMeetingService, ILogger<ZoomController> logger)
        {
            _zoomAccountService = zoomAccountService;
            _zoomUserService = zoomUserService;
            _zoomMeetingService = zoomMeetingService;
            _zoomRecordingService = zoomRecordingService;
            _zoomWebhook = zoomWebhook;
            _zoomService = zoomService;
            _teleMedecineContext = teleMedecineContext;
            _logger = logger;
        }


        [Route("CreateUser")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ZoomUserDetailDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] string Email)
        {
            try
            {
                _logger.LogInformation("Received CreateUser ");
                ZoomUserDetailDTO response = await _zoomService.CreateUser(Email);
                if (response != null)
                {
                    _logger.LogInformation($"CreateUser : Sucess response returned ");
                    return Ok(response);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    _logger.LogError("CreateUser :  Data not found ");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in CreateUser API " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [Route("GetUserDetail")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ZoomUserDetailDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserDetail([FromBody] string Email)
        {
            try
            {
                _logger.LogInformation("Received GetUserDetail ");
                ZoomUserDetailDTO response = await _zoomService.GetUserDetails(Email);
                if (response != null)
                {
                    _logger.LogInformation($"GetUserDetail : Sucess response returned ");
                    return Ok(response);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    _logger.LogError("GetUserDetail :  Data not found ");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetUserDetail API " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [Route("UdateUserStatusFromZoom")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ZoomUserDetailDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UdateUserStatusFromZoom([FromBody] string Email)
        {
            try
            {
                _logger.LogInformation("Received UdateUserStatusFromZoom ");
                ZoomUserDetailDTO response = await _zoomService.GetUserStatusFromZoom(Email);
                if (response != null)
                {
                    _logger.LogInformation($"UdateUserStatusFromZoom : Sucess response returned ");
                    return Ok(response);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    _logger.LogError("UdateUserStatusFromZoom :  Data not found ");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in UdateUserStatusFromZoom API " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [Route("GetUserStatusFromZoom")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ZoomUserDetailDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserStatusFromZoom([FromBody] string Email)
        {
            try
            {
                _logger.LogInformation("Received GetUserDetail ");
                ZoomUserDetailDTO response = await _zoomService.GetUserStatusFromZoom(Email);
                if (response != null)
                {
                    _logger.LogInformation($"GetUserStatusFromZoom : Sucess response returned ");
                    return Ok(response);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    _logger.LogError("GetUserStatusFromZoom :  Data not found ");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetUserStatusFromZoom API " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [Route("CreateNewMeeting")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ZoomUserDetailDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNewMeeting([FromBody] string HostUserMailID)
        {
            try
            {
                ZoomUserDetail zoomUserDetail = await _teleMedecineContext.ZoomUserDetails.Include(a=>a.User).Where(a => a.User.Email.ToLower() == HostUserMailID.ToLower()).FirstOrDefaultAsync();
                if (zoomUserDetail != null)
                {
                    if (zoomUserDetail.Status.ToLower() == "active")
                    {
                        _logger.LogInformation("Received GetToken ");

                        var newUserResponse = await _zoomService.CreateMeeting(HostUserMailID, zoomUserDetail.ZoomUserID);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("User is not active!");
                    }

                }
                else
                {
                    return NotFound();
                }



            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetToken API " + ex);
                throw;
            }

        }

        [Route("DeleteMeeting")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMeeting(string MeetingID)
        {
            try
            {
                _logger.LogInformation("Received GetToken ");
                var Response = await _zoomService.DeleteMeeting(MeetingID);
                if (Response)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetToken API " + ex);
                throw;
            }

        }

        [Route("EndMeeting")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EndMeeting(string MeetingID)
        {
            try
            {
                _logger.LogInformation("Received EndMeeting ");
                var Response = await _zoomService.EndMeeting(MeetingID);
                if (Response)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetToken API " + ex);
                throw;
            }

        }

        [Consumes("application/json")]
        [Route("ZoomWebhookService")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ZoomWebhookService([FromBody]JsonElement body)
        {
            _logger.LogInformation("Received ZoomWebhookService ");

            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(body);
                var bodyString = HttpContext.Items["request_body"];
                // use the body, process the stuff...
                //var content =HttpContext.Request.Content;
                //string jsonContent = content.ReadAsStringAsync().Result;


                

                var newUserResponse = await _zoomWebhook.ZoomWebhookService(json);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in ZoomWebhookService API " + ex.StackTrace);
                throw;
            }

        }

        [Route("UpdateUserRecodingSetting")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserRecodingSetting(string ZoomUserID)
        {
            try
            {
                _logger.LogInformation("Received UpdateUserRecodingSetting ");
                var Response = await _zoomService.UpdateUserRecodingSetting(ZoomUserID);
                if (Response)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in UpdateUserRecodingSetting API " + ex);
                throw;
            }

        }


        [Route("GetRecording")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(GetRecordingResponseModel))]
        public async Task<IActionResult> GetRecording(string MeetingID)
        {
            try
            {
                GetRecordingResponseModel responseModel = await _zoomRecordingService.GetRecording(MeetingID);
                if (responseModel!=null)
                {
                    return Ok(responseModel);
                }
                else
                {
                    return NotFound();
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetToken API " + ex);
                throw;
            }

        }


        [HttpPost("GetToken")]
        public async Task<IActionResult> GetToken()
        {
            try
            {
                _logger.LogInformation("Received GetToken ");

                string str = await _zoomAccountService.GetNewTokenFromZoomAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetToken API " + ex);
                throw;
            }

        }
        [HttpPost("ZoomCreateUser")]
        public async Task<IActionResult> ZoomCreateUser()
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
                var UserResponse = await _zoomUserService.GetUser(Email);
                return Ok(UserResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetToken API " + ex);
                throw;
            }

        }

      

      

    }
}
