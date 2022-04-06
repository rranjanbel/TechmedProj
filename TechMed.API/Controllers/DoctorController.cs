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
    //[Authorize]
    public class DoctorController : ControllerBase
    {
        DoctorBusinessMaster doctorBusinessMaster;
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        public DoctorController(IMapper mapper, TeleMedecineContext teleMedecineContext, IDoctorRepository doctorRepository)
        {

            doctorBusinessMaster = new DoctorBusinessMaster(teleMedecineContext, mapper);
            _doctorRepository = doctorRepository;
            _mapper = mapper;
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
        [Route("GetDoctorDetails")]
        [HttpPost]
        public async Task<DoctorDTO> GetDoctorDetails(GetDoctorDetailVM getDoctorDetailVM)
        {
            return await _doctorRepository.GetDoctorDetails(getDoctorDetailVM);
        }
        [Route("GetListOfMedicine")]
        [HttpPost]
        public async Task<List<MedicineMasterDTO>> GetListOfMedicine()
        {
            return await _doctorRepository.GetListOfMedicine();
        }
        [Route("GetListOfVital")]
        [HttpPost]
        public async Task<List<VitalMasterDTO>> GetListOfVital()
        {
            return await _doctorRepository.GetListOfVital();
        }
        [Route("GetListOfPHCHospital")]
        [HttpPost]
        public async Task<List<PHCHospitalDTO>> GetListOfPHCHospital()
        {
            return await _doctorRepository.GetListOfPHCHospital();
        }

        [Route("GetListOfSpecializationMaster")]
        [HttpPost]
        public async Task<List<SpecializationDTO>> GetListOfSpecializationMaster()
        {
            return await _doctorRepository.GetListOfSpecializationMaster();
        }

        [Route("GetListOfSubSpecializationMaster")]
        [HttpPost]
        public async Task<List<SubSpecializationDTO>> GetListOfSubSpecializationMaster(int SpecializationId)
        {
            return await _doctorRepository.GetListOfSubSpecializationMaster(SpecializationId);
        }

        [Route("UpdateDoctorDetails")]
        [HttpPost]
        public async Task<bool> UpdateDoctorDetails(DoctorDTO doctorDTO)
        {
            return await _doctorRepository.UpdateDoctorDetails(doctorDTO);
        }
    }
}
