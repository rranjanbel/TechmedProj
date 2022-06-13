using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.DTOMaster;
using TechMed.BL.ModelMaster;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.BL.CommanClassesAndFunctions;

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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<DoctorController> _logger;
        public DoctorController(IMapper mapper, ILogger<DoctorController> logger, TeleMedecineContext teleMedecineContext, IDoctorRepository doctorRepository, IWebHostEnvironment webHostEnvironment)
        {
            doctorBusinessMaster = new DoctorBusinessMaster(teleMedecineContext, mapper);
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
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
                _logger.LogError("Exception in GetListOfNotification API " + ex.Message);
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
                _logger.LogError("Exception in GetCDSSGuideLines API " + ex.Message);
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
                _logger.LogError("Exception in GetDoctorDetails API " + ex.Message);
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
                _logger.LogError("Exception in GetListOfMedicine API " + ex.Message);
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
                _logger.LogError("Exception in GetListOfVital API " + ex.Message);
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
                _logger.LogError("Exception in GetListOfPHCHospital API " + ex.Message);
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
                _logger.LogError("Exception in GetListOfSpecializationMaster API " + ex.Message);
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
                _logger.LogError("Exception in GetListOfSubSpecializationMaster API " + ex.Message);
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
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
                {
                    //_webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "");
                   
                }
                _webHostEnvironment.WebRootPath = "/MyFiles/Images/Doctor/";
                webRootPath = _webHostEnvironment.WebRootPath;
                if (webRootPath == String.Empty || webRootPath == null)
                {
                    ModelState.AddModelError("UpdateDoctorDetails", "Path did not get proper " + webRootPath);
                    return StatusCode(404, ModelState);
                }
                _logger.LogInformation($"Update Doctor Details : relative Path : " + webRootPath);
                _logger.LogInformation($"Update Doctor Details : call web api add UpdateDoctorDetails");
                if (doctorDTO == null || doctorDTO.Id < 1 || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _doctorRepository.UpdateDoctorDetails(doctorDTO, contentRootPath ,webRootPath);
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
                _logger.LogError("Exception in UpdateDoctorDetails API " + ex.Message);
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
                if (doctorVM.DoctorID < 1 || !ModelState.IsValid)
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
                _logger.LogError("Exception in GetTodayesPatients API " + ex.Message);
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
                _logger.LogError("Exception in GetCompletedConsultationPatientsHistory API " + ex.Message);
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
                _logger.LogError("Exception in GetYesterdayPatientsHistory API " + ex.Message);
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
                if (doctorVM.DoctorID < 1 || !ModelState.IsValid)
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
                _logger.LogError("Exception in GetPastPatientsHistory API " + ex.Message);
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
                _logger.LogError("Exception in GetPatientCaseDetailsAsync API " + ex.Message);
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
                _logger.LogError("Exception in PostTreatmentPlan API " + ex.Message);
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
                _logger.LogError("Exception in DeleteNotification API " + ex.Message);
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
                _logger.LogError("Exception in EHRdata API " + ex.Message);
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
                _logger.LogError("Exception in PatientAbsent API " + ex.Message);
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
                _logger.LogError("Exception in ReferHigherFacility API " + ex.Message);
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
                _logger.LogError("Exception in ReferHigherFacility API " + ex.Message);
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
                _logger.LogError("Exception in SearchPatientDrDashBoard API " + ex.Message);
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
                _logger.LogError("Exception in SearchPatientDrHistory API " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }

        [Route("GetListOfPHCHospitalBlockWise")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<PHCHospitalDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetListOfPHCHospitalBlockWise(GetListOfPHCHospitalVM getListOfPHCHospitalVM)
        {
            try
            {
                if (getListOfPHCHospitalVM == null)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _doctorRepository.GetListOfPHCHospitalBlockWise(getListOfPHCHospitalVM);
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
                _logger.LogError("Exception in GetListOfPHCHospitalBlockWise API " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }

        [Route("GetLatestReferred")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<GetTodayesPatientsDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLatestReferred(DoctorVM doctorVM)
        {
            try
            {
                if (doctorVM.DoctorID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(doctorVM.DoctorID);
                }
                var DTO = await _doctorRepository.GetLatestReferred(doctorVM);
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
                _logger.LogError("Exception in GetLatestReferred API " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }
        [Route("GetLatestReferredCount")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLatestReferredCount(DoctorVM doctorVM)
        {
            try
            {
                if (doctorVM.DoctorID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(doctorVM.DoctorID);
                }
                var DTO = await _doctorRepository.GetLatestReferredCount(doctorVM);
                if (DTO > 0)
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
                _logger.LogError("Exception in GetLatestReferredCount API " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }


        [Route("UpdateIsDrOnline")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<bool>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateIsDrOnline(UpdateIsDrOnlineVM doctorVM)
        {
            try
            {
                if (doctorVM.DoctorID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(doctorVM.DoctorID);
                }
                var DTO = await _doctorRepository.UpdateIsDrOnline(doctorVM);
                return Ok(DTO);
                //if (DTO)
                //{
                //    return Ok(DTO);
                //}
                //else
                //{
                //    ModelState.AddModelError("", $"Data not found!");
                //    return StatusCode(404, ModelState);
                //}
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in UpdateIsDrOnline API " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }

        [Route("IsDrOnline")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<bool>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IsDrOnline(DoctorVM doctorVM)
        {
            try
            {
                if (doctorVM.DoctorID < 1 || !ModelState.IsValid)
                {
                    return BadRequest(doctorVM.DoctorID);
                }
                var DTO = await _doctorRepository.IsDrOnline(doctorVM);
                return Ok(DTO);
                //if (DTO)
                //{
                //    return Ok(DTO);
                //}
                //else
                //{
                //    ModelState.AddModelError("", $"Data not found!");
                //    return StatusCode(404, ModelState);
                //}
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in IsDrOnline API " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }

        [Route("OnlineDrList")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<OnlineDrListDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> OnlineDrList()
        {
            try
            {
                //if (doctorVM.BlockID < 1 || !ModelState.IsValid)
                //{
                //    return BadRequest(doctorVM);
                //}
                var DTO = await _doctorRepository.OnlineDrList();
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
                _logger.LogError("Exception in OnlineDrList API " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }


        [HttpPost]
        [Route("AddDoctor")]
        [ProducesResponseType(200, Type = typeof(DoctorDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddDoctor([FromBody] AddDoctorDTO doctorDTO)
        {
            DoctorMaster doctor = new DoctorMaster();
            DoctorMaster doctorCreated = new DoctorMaster();
            UserMaster userMaster = new UserMaster();
            UserDetail userDetail = new UserDetail();
            try
            {
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
                {
                    //_webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "");
                    
                }
                _webHostEnvironment.WebRootPath = "/MyFiles/Images/Doctor/";
                webRootPath = _webHostEnvironment.WebRootPath;
                if (webRootPath == String.Empty || webRootPath == null)
                {
                    ModelState.AddModelError("AddDoctor", "Path did not get proper " + webRootPath);
                    return StatusCode(404, ModelState);
                }
                _logger.LogInformation($"Add Doctor : relative Path : " + webRootPath);
                _logger.LogInformation($"Add Doctor : call web api add Doctor");
                //check doctorPhone in doctor and user
                //email in user and details
                //"zoneId": 1,
                //"clusterId": 1,
                //"specializationId": 1,
                //"subSpecializationId": null,
                //"titleId": 1,
                //"genderId": 1,
                // "countryId": 1,
                //"stateId": 1,
                //"pinCode": "1222002",
                //"idproofTypeId": 1,
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string mobilechk = await _doctorRepository.CheckMobile(doctorDTO.PhoneNumber);
                string emailchk = await _doctorRepository.CheckEmail(doctorDTO.detailsDTO.EmailId);
                if (!string.IsNullOrEmpty(mobilechk))
                {
                    ModelState.AddModelError("doctorDTO", mobilechk);
                    return StatusCode(404, ModelState);
                }
                if (!string.IsNullOrEmpty(emailchk))
                {
                    ModelState.AddModelError("doctorDTO", emailchk);
                    return StatusCode(404, ModelState);
                }
                if (true)
                {
                    //Need to get here data from database runtime when not supplied by GUI

                    doctor.BlockId = doctorDTO.BlockId;
                    doctor.ClusterId = doctorDTO.ClusterId;
                    doctor.SpecializationId = doctorDTO.SpecializationId;
                    doctor.SubSpecializationId = doctorDTO.SubSpecializationId;
                    doctor.Mciid = doctorDTO.Mciid;
                    doctor.RegistrationNumber = doctorDTO.RegistrationNumber;
                    doctor.Qualification = doctorDTO.Qualification;
                    doctor.Designation = doctorDTO.Designation;
                    doctor.PhoneNumber = doctorDTO.PhoneNumber;
                    doctor.DigitalSignature = doctorDTO.DigitalSignature;
                    doctor.Panno = doctorDTO.PanNo;
                    doctor.BankName = doctorDTO.BankName;
                    doctor.BranchName = doctorDTO.BranchName;
                    doctor.AccountNumber = doctorDTO.AccountNumber;
                    doctor.Ifsccode = doctorDTO.Ifsccode;
                    doctor.IsOnline = false;
                    doctor.CreatedBy = doctorDTO.CreatedBy;
                    doctor.UpdatedBy = doctorDTO.CreatedBy;
                    doctor.CreatedOn = DateTime.Now;
                    doctor.UpdatedOn = DateTime.Now;

                    userMaster.Email = doctorDTO.detailsDTO.EmailId;
                    userMaster.Name = doctorDTO.detailsDTO.FirstName;
                    userMaster.Mobile = doctorDTO.PhoneNumber;
                    userMaster.HashPassword = EncodeAndDecordPassword.EncodePassword("doctot@123"); 
                    userMaster.LoginAttempts = 0;
                    userMaster.LastLoginAt = DateTime.Now;
                    userMaster.IsActive = true;
                    userMaster.IsPasswordChanged = false;
                    userMaster.CreatedBy = doctorDTO.CreatedBy;
                    userMaster.UpdatedBy = doctorDTO.CreatedBy;
                    userMaster.CreatedOn = DateTime.Now;
                    userMaster.UpdatedOn = DateTime.Now;

                    userDetail.TitleId = doctorDTO.detailsDTO.TitleId;
                    userDetail.FirstName = doctorDTO.detailsDTO.FirstName;
                    userDetail.MiddleName = doctorDTO.detailsDTO.MiddleName;
                    userDetail.LastName = doctorDTO.detailsDTO.LastName;
                    userDetail.Dob = doctorDTO.detailsDTO.Dob;
                    userDetail.GenderId = doctorDTO.detailsDTO.GenderId;
                    userDetail.EmailId = doctorDTO.detailsDTO.EmailId;
                    userDetail.PhoneNumber = doctorDTO.PhoneNumber.ToString();
                    userDetail.CountryId = doctorDTO.detailsDTO.CountryId;
                    userDetail.StateId = doctorDTO.detailsDTO.StateId;
                    userDetail.City = doctorDTO.detailsDTO.City;
                    userDetail.Address = doctorDTO.detailsDTO.Address;
                    userDetail.PinCode = doctorDTO.detailsDTO.PinCode;
                    userDetail.Photo = doctorDTO.detailsDTO.Photo;
                    userDetail.IdproofTypeId = doctorDTO.detailsDTO.IdproofTypeId;
                    userDetail.IdproofNumber = doctorDTO.detailsDTO.IdproofNumber;
                    userDetail.CreatedBy = doctorDTO.CreatedBy;
                    userDetail.CreatedOn = DateTime.Now;
                    userDetail.UpdatedBy = doctorDTO.CreatedBy;
                    userDetail.UpdatedOn = DateTime.Now;

                    doctorCreated = await this._doctorRepository.AddDoctor(doctor, userMaster, userDetail, doctorDTO, contentRootPath,webRootPath);
                }

                if (doctorCreated == null)
                {
                    ModelState.AddModelError("Add Doctor", $"Something went wrong when create Doctor {doctorDTO}");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    DoctorDTO doctorDTO1 = new DoctorDTO();
                    doctorDTO1.Id = doctorCreated.Id;
                    doctorDTO1.BlockID = doctorCreated.BlockId;
                    doctorDTO1.ClusterId = doctorCreated.ClusterId;
                    doctorDTO1.UserId = doctorCreated.UserId;
                    doctorDTO1.SpecializationId = doctorCreated.SpecializationId;
                    doctorDTO1.SubSpecializationId = doctorCreated.SubSpecializationId;
                    doctorDTO1.Mciid = doctorCreated.Mciid;
                    doctorDTO1.RegistrationNumber = doctorCreated.RegistrationNumber;
                    doctorDTO1.Qualification = doctorCreated.Qualification;
                    doctorDTO1.Designation = doctorCreated.Designation;
                    doctorDTO1.PhoneNumber = doctorCreated.PhoneNumber;
                    doctorDTO1.DigitalSignature = doctorCreated.DigitalSignature;
                    doctorDTO1.PanNo = doctorCreated.Panno;
                    doctorDTO1.BankName = doctorCreated.BankName;
                    doctorDTO1.BranchName = doctorCreated.BranchName;
                    doctorDTO1.AccountNumber = doctorCreated.AccountNumber;
                    doctorDTO1.Ifsccode = doctorCreated.Ifsccode;
                    doctorDTO1.UpdatedBy = doctorCreated.UpdatedBy;

                    doctorDTO1.detailsDTO.TitleId = userDetail.TitleId;
                    doctorDTO1.detailsDTO.FirstName = userDetail.FirstName;
                    doctorDTO1.detailsDTO.MiddleName = userDetail.MiddleName;
                    doctorDTO1.detailsDTO.LastName = userDetail.LastName;
                    doctorDTO1.detailsDTO.Dob = userDetail.Dob;
                    doctorDTO1.detailsDTO.GenderId = userDetail.GenderId;
                    doctorDTO1.detailsDTO.EmailId = userDetail.EmailId;
                    doctorDTO1.detailsDTO.CountryId = userDetail.CountryId;
                    doctorDTO1.detailsDTO.StateId = userDetail.StateId;
                    doctorDTO1.detailsDTO.City = userDetail.City;
                    doctorDTO1.detailsDTO.PinCode = userDetail.PinCode;
                    doctorDTO1.detailsDTO.Photo = userDetail.Photo;
                    doctorDTO1.detailsDTO.IdproofTypeId = userDetail.IdproofTypeId;
                    doctorDTO1.detailsDTO.IdproofNumber = userDetail.IdproofNumber;
                    doctorDTO1.detailsDTO.Address = userDetail.Address;
                    return CreatedAtRoute(200, doctorDTO1);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("AddDoctor", $"Something went wrong when create Doctor {ex.Message}");
                _logger.LogError("Exception in AddDoctor API " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }

        [Route("GetDoctorDetailsByUserID")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(DoctorDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorDetailsByUserID(GetDoctorDetailByUserIDVM getDoctorDetailVM)
        {
            try
            {
                if (getDoctorDetailVM == null)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _doctorRepository.GetDoctorDetailsByUserID(getDoctorDetailVM);
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
                _logger.LogError("Exception in GetDoctorDetailsByUserID API " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("AdvanceSearchResult")]
        [ProducesResponseType(201, Type = typeof(List<DoctorPatientSearchVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AdvanceSearchResult([FromBody] AdvanceDoctorPatientSearchVM searchParameter)
        {
            List<DoctorPatientSearchVM> patientSearchResults = new List<DoctorPatientSearchVM>();
            try
            {
                patientSearchResults = this._doctorRepository.GetAdvanceSearchDoctorsPatient(searchParameter);


                if (searchParameter == null)
                {
                    ModelState.AddModelError("AdvanceSearchResult", $"Search Parameter is null");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    return StatusCode(200, patientSearchResults);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("AdvanceSearchResult", $"Something went wrong when get yesterday's patient list {ex.Message}");
                _logger.LogError("Exception in AdvanceSearchResult API " + ex.Message);
                return StatusCode(500, ModelState);
            }


        }
    }
}
