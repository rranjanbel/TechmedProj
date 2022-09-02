using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.ViewModel;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [Authorize(Roles = "SuperAdmin,SysAdmin")]
    public class MailController : ControllerBase
    {
        private readonly IMailService mailService;
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost("WelcomeMail")]
        public async Task<IActionResult> SendWelcomeMail([FromForm] WelcomeRequest request)
        {
            try
            {
                await mailService.SendWelcomeEmailAsync(request);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
