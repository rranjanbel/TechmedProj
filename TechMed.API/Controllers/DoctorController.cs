using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.DTOMaster;
using TechMed.BL.ModelMaster;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;
using TechMed.BL.Repository.Interfaces;
namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        DoctorBusinessMaster doctorBusinessMaster;
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        public DoctorController(IMapper mapper, TeleMedecineContext teleMedecineContext, IDoctorRepository doctorRepository)
        {

            doctorBusinessMaster = new DoctorBusinessMaster(teleMedecineContext, mapper);
            _doctorRepository = doctorRepository;
        }
        [Route("GetListOfNotification")]
        [HttpPost]
        public async Task<List<NotificationDTO>> GetListOfNotification(GetListOfNotificationVM getListOfNotificationVM)
        {
            return await _doctorRepository.GetListOfNotification(getListOfNotificationVM);
        }
        [Route("GetCDSSGuideLines")]
        [HttpGet]
        public async Task<CdssguidelineMasterDTO> GetCDSSGuideLines()
        {
            return await _doctorRepository.GetCDSSGuideLines();
        }
    }
}
