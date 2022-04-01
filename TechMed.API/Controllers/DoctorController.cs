using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.DTOMaster;
using TechMed.BL.ModelMaster;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        DoctorBusinessMaster doctorBusinessMaster;
        private readonly IMapper _mapper;
        public DoctorController(IMapper mapper, TeleMedecineContext teleMedecineContext)
        {

            doctorBusinessMaster = new DoctorBusinessMaster(teleMedecineContext, mapper);
        }
        [Route("GetListOfNotification")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<NotificationDTO>))]
        public IActionResult GetListOfNotification(GetListOfNotificationVM getListOfNotificationVM)
        {
            var listOfNotification = doctorBusinessMaster.GetListOfNotification(getListOfNotificationVM);
            return Ok(listOfNotification);
        }
        [Route("GetCDSSGuideLines")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(CdssguidelineMasterDTO))]
        public IActionResult GetCDSSGuideLines()
        {
            var responseData = doctorBusinessMaster.GetCDSSGuideLines();
            return Ok(responseData);
        }
    }
}
