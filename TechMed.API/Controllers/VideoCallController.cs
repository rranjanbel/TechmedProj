using Microsoft.AspNetCore.Mvc;
using TechMed.BL.TwilioAPI.Service;
using Twilio.Rest.Video.V1;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoCallController : ControllerBase
    {
        readonly IVideoService _videoService;

        public VideoCallController(IVideoService videoService)
            => _videoService = videoService;

        [HttpGet("token")]
        public IActionResult GetToken()
            => new JsonResult(new { token = _videoService.GetTwilioJwt(User.Identity.Name) });

        [HttpGet("rooms")]
        public async Task<IActionResult> GetRooms()
            => new JsonResult(await _videoService.GetAllRoomsAsync());

        [HttpPost("create-room-with-recording")]
        public async Task<RoomResource> CreateRoomsAsync(string roomname, string callBackUrl)
        { 
            return await _videoService.CreateRoomsAsync(roomname, callBackUrl);
        }

    }
}
