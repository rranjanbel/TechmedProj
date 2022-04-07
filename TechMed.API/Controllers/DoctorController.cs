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
        [ProducesResponseType(200, Type = typeof(List<NotificationDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetListOfNotification(GetListOfNotificationVM getListOfNotificationVM)
        {
            try
            {
                if (getListOfNotificationVM == null)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _doctorRepository.GetListOfNotification(getListOfNotificationVM);
                if (DTO.Count > 0)
                {
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }
        [Route("GetCDSSGuideLines")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(CdssguidelineMasterDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCDSSGuideLines()
        {
            try
            {
                var DTO = await _doctorRepository.GetCDSSGuideLines();
                if (DTO!=null)
                {
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }
        [Route("GetDoctorDetails")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(DoctorDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorDetails(GetDoctorDetailVM getDoctorDetailVM)
        {
            try
            {
                if (getDoctorDetailVM == null)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _doctorRepository.GetDoctorDetails(getDoctorDetailVM);
                if (DTO.Id>0)
                {
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }
        [Route("GetListOfMedicine")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<MedicineMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetListOfMedicine()
        {
            try
            {
                //if (getListOfNotificationVM == null)
                //{
                //    return BadRequest(ModelState);
                //}
                var DTO = await _doctorRepository.GetListOfMedicine();
                if (DTO.Count > 0)
                {
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }
        [Route("GetListOfVital")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<VitalMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetListOfVital()
        {
            try
            {
                //if (getListOfNotificationVM == null)
                //{
                //    return BadRequest(ModelState);
                //}
                var DTO = await _doctorRepository.GetListOfVital();
                if (DTO.Count > 0)
                {
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }
        [Route("GetListOfPHCHospital")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<PHCHospitalDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetListOfPHCHospital()
        {
            try
            {
                //if (getListOfNotificationVM == null)
                //{
                //    return BadRequest(ModelState);
                //}
                var DTO = await _doctorRepository.GetListOfPHCHospital();
                if (DTO.Count > 0)
                {
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [Route("GetListOfSpecializationMaster")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<SpecializationDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetListOfSpecializationMaster()
        {
            try
            {
                //if (getListOfNotificationVM == null)
                //{
                //    return BadRequest(ModelState);
                //}
                var DTO = await _doctorRepository.GetListOfSpecializationMaster();
                if (DTO.Count > 0)
                {
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [Route("GetListOfSubSpecializationMaster")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<SubSpecializationDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetListOfSubSpecializationMaster(int SpecializationId)
        {
            try
            {
                if (SpecializationId == 0)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _doctorRepository.GetListOfSubSpecializationMaster(SpecializationId);
                if (DTO.Count > 0)
                {
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [Route("UpdateDoctorDetails")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDoctorDetails(DoctorDTO doctorDTO)
        {
            
            try
            {
                if (doctorDTO == null|| doctorDTO.Id<1|| !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _doctorRepository.UpdateDoctorDetails(doctorDTO);
                if (DTO)
                {
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not updated!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [Route("GetTodayesPatients")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<GetTodayesPatientsDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTodayesPatients(long DoctorID)
        {

            try
            {
                if (DoctorID == null || DoctorID< 1 || !ModelState.IsValid)
                {
                    return BadRequest(DoctorID);
                }
                var DTO = await _doctorRepository.GetTodayesPatients(DoctorID);
                if (DTO.Count>0)
                {
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }
        [Route("GetCompletedConsultationPatientsHistory")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<GetTodayesPatientsDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCompletedConsultationPatientsHistory(long DoctorID)
        {

            try
            {
                if (DoctorID == null || DoctorID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(DoctorID);
                }
                var DTO = await _doctorRepository.GetCompletedConsultationPatientsHistory(DoctorID);
                if (DTO.Count > 0)
                {
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }
    }
}
