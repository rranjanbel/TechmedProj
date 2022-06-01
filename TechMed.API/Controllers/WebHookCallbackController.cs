using Microsoft.AspNetCore.Mvc;
using TechMed.BL.ViewModels;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHookCallbackController : Controller
    {
        [HttpPost("twilioroomstatuscallback")]
        public IActionResult TwilioRoomStatusCallback([FromQuery] string RoomName)
        {
            return Ok();
        }
    }
}
