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
                if (DTO != null)
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
                if (DTO.Id > 0)
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
        [HttpGet]
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
        [HttpGet]
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
        [HttpGet]
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
        [HttpGet]
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
        [HttpGet]
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
                if (doctorDTO == null || doctorDTO.Id < 1 || !ModelState.IsValid)
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
        public async Task<IActionResult> GetTodayesPatients(DoctorVM doctorVM)
        {

            try
            {
                if (doctorVM.DoctorID > 0 || doctorVM.DoctorID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(doctorVM.DoctorID);
                }
                var DTO = await _doctorRepository.GetTodayesPatients(doctorVM);
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
        [Route("GetCompletedConsultationPatientsHistory")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<GetTodayesPatientsDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCompletedConsultationPatientsHistory(DoctorVM doctorVM)
        {

            try
            {
                if (doctorVM.DoctorID == null || doctorVM.DoctorID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(doctorVM.DoctorID);
                }
                var DTO = await _doctorRepository.GetCompletedConsultationPatientsHistory(doctorVM);
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
        [Route("GetYesterdayPatientsHistory")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<GetTodayesPatientsDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetYesterdayPatientsHistory(DoctorVM doctorVM)
        {

            try
            {
                if (doctorVM.DoctorID == null || doctorVM.DoctorID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(doctorVM.DoctorID);
                }
                var DTO = await _doctorRepository.GetYesterdayPatientsHistory(doctorVM);
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
        [Route("GetPastPatientsHistory")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<GetTodayesPatientsDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPastPatientsHistory(DoctorVM doctorVM)
        {

            try
            {
                if (doctorVM.DoctorID > 0 || doctorVM.DoctorID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(doctorVM.DoctorID);
                }
                var DTO = await _doctorRepository.GetPastPatientsHistory(doctorVM);
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

        [Route("GetPatientCaseDetailsAsync")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(GetPatientCaseDetailsDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientCaseDetailsAsync(GetPatientCaseDetailsVM caseDetailsVM)
        {
            try
            {
                if (caseDetailsVM.PatientCaseID == null || caseDetailsVM.PatientCaseID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(caseDetailsVM.PatientCaseID);
                }
                var DTO = await _doctorRepository.GetPatientCaseDetailsAsync(caseDetailsVM);
                if (DTO.PatientId > 0)
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

        [Route("PostTreatmentPlan")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostTreatmentPlan(TreatmentVM treatmentVM)
        {
            try
            {
                if (treatmentVM.PatientCaseID == null || treatmentVM.PatientCaseID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(treatmentVM.PatientCaseID);
                }
                var DTO = await _doctorRepository.PostTreatmentPlan(treatmentVM);
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

        [Route("DeleteNotification")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteNotification(long NotificationID)
        {
            try
            {
                if (NotificationID == null || NotificationID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(NotificationID);
                }
                var DTO = await _doctorRepository.DeleteNotification(NotificationID);
                if (DTO)
                {
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not deleted!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("EHRdata")]
        [ProducesResponseType(200, Type = typeof(GetEHRDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EHRdata(GetEHRVM getEHRVM)
        {
            try
            {
                if (getEHRVM.PatientCaseID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(getEHRVM);
                }
                GetEHRDTO DTO = await _doctorRepository.GetEHR(getEHRVM);
                if (DTO != null)
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

        [HttpPost]
        [Route("PatientAbsent")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatientAbsent(PatientAbsentVM patientAbsentVM)
        {
            try
            {
                if (patientAbsentVM.CaseID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(patientAbsentVM);
                }
                bool DTO = await _doctorRepository.PatientAbsent(patientAbsentVM);
                if (DTO)
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
                ModelState.AddModelError("CaseID", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("ReferHigherFacility")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReferHigherFacility(PatientAbsentVM patientAbsentVM)
        {
            try
            {
                if (patientAbsentVM.CaseID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(patientAbsentVM);
                }
                bool DTO = await _doctorRepository.ReferHigherFacility(patientAbsentVM);
                if (DTO)
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
                ModelState.AddModelError("CaseID", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("GetCaseLabel")]
        [ProducesResponseType(200, Type = typeof(List<GetCaseLabelDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCaseLabel(GetCaseLabelVM getCaseLabelVM)
        {
            try
            {
                if (getCaseLabelVM.PatientID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(getCaseLabelVM);
                }
                List<GetCaseLabelDTO> DTO = await _doctorRepository.GetCaseLabel(getCaseLabelVM);
                if (DTO!=null)
                {
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
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
              
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CaseID", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("SearchPatientDrDashBoard")]
        [ProducesResponseType(200, Type = typeof(List<SearchPatientsDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchPatientDrDashBoard(SearchPatientVM searchPatientVM)
        {
            try
            {
                if (searchPatientVM.DoctorID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(searchPatientVM);
                }
                List<SearchPatientsDTO> DTO = await _doctorRepository.SearchPatientDrDashBoard(searchPatientVM);
                if (DTO != null)
                {
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
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CaseID", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("SearchPatientDrHistory")]
        [ProducesResponseType(200, Type = typeof(List<SearchPatientsDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchPatientDrHistory(SearchPatientVM searchPatientVM)
        {
            try
            {
                if (searchPatientVM.DoctorID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(searchPatientVM);
                }
                List<SearchPatientsDTO> DTO = await _doctorRepository.SearchPatientDrHistory(searchPatientVM);
                if (DTO != null)
                {
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
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CaseID", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [Route("GetListOfPHCHospitalZoneWise")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<PHCHospitalDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetListOfPHCHospitalZoneWise(GetListOfPHCHospitalVM getListOfPHCHospitalVM)
        {
            try
            {
                if (getListOfPHCHospitalVM == null)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _doctorRepository.GetListOfPHCHospitalZoneWise(getListOfPHCHospitalVM);
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
