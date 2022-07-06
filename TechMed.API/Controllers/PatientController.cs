using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TechMed.BL.DTOMaster;
using TechMed.BL.ModelMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;
using System.IO;
using TechMed.DL.ViewModel;
using TechMed.BL.CommanClassesAndFunctions;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IMapper _mapper;       
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<PatientController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PatientController(IMapper mapper, TeleMedecineContext teleMedecineContext, IPatientRepository patientRepository, ILogger<PatientController> logger, IWebHostEnvironment webHostEnvironment)
        {
            this._mapper = mapper;         
            this._patientRepository = patientRepository;
            this._logger = logger;
            this._webHostEnvironment = webHostEnvironment;
        }        
        [HttpPost]
        [Route("AddPatient")]
        [ProducesResponseType(201, Type = typeof(PatientMasterDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPatient([FromBody] PatientMasterDTO patientdto)
        {
            PatientMaster newCreatedPatient = new PatientMaster();
            try
            {
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
                {
                    //_webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "");
                    _webHostEnvironment.WebRootPath = "/MyFiles/Images/Patients/";
                    webRootPath = _webHostEnvironment.WebRootPath;
                }
                if (webRootPath == String.Empty || webRootPath == null)
                {
                    ModelState.AddModelError("AddPatient", "Path did not get proper " + webRootPath);
                    return StatusCode(404, ModelState);
                }
                _logger.LogInformation($"Add Patient : relative Path : " + webRootPath);
                _logger.LogInformation($"Add Patient : call web api add patient {patientdto.FirstName}");
                
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Add Patient : model state is invalid {patientdto}");
                    return BadRequest(ModelState);
                }
                var patientDetails = _mapper.Map<PatientMaster>(patientdto);
                _logger.LogInformation($"Add Patient : going to check Is Patient Exist.{patientdto.FirstName}");
                if (_patientRepository.IsPatientExist(patientDetails))
                {
                    _logger.LogInformation($"Add Patient : Patient is already in system.{patientdto.FirstName}");
                    ModelState.AddModelError("AddPatient", "Patient name or mobile number is already in system");
                    return StatusCode(404, ModelState);
                }
                //newCreatedPatient = await this._patientRepository.Create(patientDetails);
                _logger.LogInformation($"Add Patient : call get patient unique id. {patientdto.FirstName}");
                patientDetails.PatientId = this._patientRepository.GetPatientId();
                _logger.LogInformation($"Add Patient : Patient unique id is : " + patientDetails.PatientId);
                _logger.LogInformation($"Add Patient : call method save image : " + patientDetails.PatientId);
                string fileName = _patientRepository.SaveImage(patientdto.Photo, contentRootPath);
                webRootPath = @"/MyFiles/Images/Patients/";
                patientDetails.Photo = webRootPath + fileName;
                _logger.LogInformation($"Add Patient : saved patinet image : " + patientDetails.Photo);
                newCreatedPatient = await this._patientRepository.AddPatient(patientDetails);
                if (newCreatedPatient == null)
                {
                    _logger.LogInformation($"Add Patient : Patient did not added in the database {patientdto.FirstName}");
                    ModelState.AddModelError("AddPatient", $"Something went wrong when create Patient {patientdto.FirstName}");
                    return StatusCode(404, ModelState);
                }
                else
                {                   
                    var createdPatient = _mapper.Map<PatientMasterDTO>(newCreatedPatient);
                    createdPatient.Age = UtilityMaster.GetAgeOfPatient(createdPatient.Dob);
                    _logger.LogInformation($"Add Patient : Sucess response returned  {patientdto.FirstName}");
                    return CreatedAtRoute(201, createdPatient);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("AddPatient", $"Exception :Something went wrong when create Patient {ex.Message}");
                _logger.LogError("Exception in Add Patient module " + ex);
                return StatusCode(500, ModelState);
            }  
        }

        [HttpGet]
        [Route("GetTodaysPatient")]
        [ProducesResponseType(200, Type = typeof(List<TodaysPatientVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTodaysPatient(int phcID)
        {
            List<TodaysPatientVM> todaysPatientList = new List<TodaysPatientVM>();
            try
            {
                todaysPatientList = await this._patientRepository.GetTodaysPatientList(phcID);
                if (todaysPatientList == null)
                {
                    ModelState.AddModelError("GetTodaysPatient", $"did not get today's patient list");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    _logger.LogInformation($"GetTodaysPatient : Sucess response returned ");
                    return StatusCode(200, todaysPatientList);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetTodaysPatient", $"Something went wrong when get today's patient list {ex.Message}");
                _logger.LogError("Exception in GetTodaysPatient API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetConsultedPatient")]
        [ProducesResponseType(200, Type = typeof(List<TodaysPatientVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetConsultedPatient(int phcID)
        {
            List<TodaysPatientVM> todaysPatientList = new List<TodaysPatientVM>();
            try
            {
                todaysPatientList = await this._patientRepository.GetCheckedPatientList(phcID);
                if (todaysPatientList == null)
                {
                    ModelState.AddModelError("GetConsultedPatient", $"did not get today's consulted patient list");
                    _logger.LogError("GetConsultedPatient : did not get today's consulted patient list ");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    _logger.LogInformation($"GetConsultedPatient : Sucess response returned ");
                    return StatusCode(200, todaysPatientList);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetConsultedPatient", $"Something went wrong when get today's patient list {ex.Message}");
                _logger.LogError("Exception in GetConsultedPatient API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetTodaysPatientCount")]
        [ProducesResponseType(200, Type = typeof(PHCPatientCount))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTodaysPatientCount(int phcID)
        {
            PHCPatientCount patientCount = new PHCPatientCount();
            try
            {
                patientCount = await this._patientRepository.GetPatientCount(phcID);
                if (patientCount == null)
                {
                    ModelState.AddModelError("GetTodaysPatientCount", $"did not get today's patient count");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    _logger.LogInformation($"GetTodaysPatientCount : Sucess response returned ");
                    return StatusCode(200, patientCount);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetTodaysPatientCount", $"Something went wrong when get today's patient count {ex.Message}");
                _logger.LogError("Exception in GetTodaysPatientCount API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetTodaysSearchedPatients")]
        [ProducesResponseType(200, Type = typeof(List<TodaysPatientVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTodaysSearchedPatients(string patientName)
        {
            List<TodaysPatientVM> todaysPatientList = new List<TodaysPatientVM>();
            try
            {
                todaysPatientList = await this._patientRepository.GetSearchedTodaysPatientList(patientName);
                if (todaysPatientList == null)
                {
                    ModelState.AddModelError("GetTodaysSearchedPatients", $"Something went wrong when get today's patient list");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    _logger.LogInformation($"GetTodaysSearchedPatients : Sucess response returned ");
                    return StatusCode(200, todaysPatientList);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetTodaysSearchedPatients", $"Something went wrong when get today's patient list {ex.Message}");
                _logger.LogError("Exception in GetTodaysSearchedPatients API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetYesterdaysPatient")]
        [ProducesResponseType(200, Type = typeof(List<PatientViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetYesterdaysPatient(int phcID)
        {
            List<PatientViewModel> patientList = new List<PatientViewModel>();
            try
            {
                patientList = await this._patientRepository.GetYesterdaysPatientList(phcID);
                if (patientList == null)
                {
                    ModelState.AddModelError("GetYesterdaysPatient", $"did not get yesterday's patient list");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    _logger.LogInformation($"GetYesterdaysPatient : Sucess response returned ");
                    return StatusCode(200, patientList);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetYesterdaysPatient", $"Something went wrong when get yesterday's patient list {ex.Message}");
                _logger.LogError("Exception in GetYesterdaysPatient API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("AdvanceSearchResult")]
        [ProducesResponseType(201, Type = typeof(List<PatientSearchResultVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AdvanceSearchResult([FromBody] AdvanceSearchPatientVM searchParameter)
        {
            List<PatientSearchResultVM> patientSearchResults = new List<PatientSearchResultVM>();
            try
            {
                patientSearchResults = this._patientRepository.GetAdvanceSearchPatient(searchParameter);


                if (searchParameter == null)
                {
                    ModelState.AddModelError("AdvanceSearchResult", $"Search Parameter is null");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    _logger.LogInformation($"AdvanceSearchResult : Sucess response returned ");
                    return StatusCode(200, patientSearchResults);
                }               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("AdvanceSearchResult", $"Something went wrong when get yesterday's patient list {ex.Message}");
                _logger.LogError("Exception in AdvanceSearchResult API " + ex);
                return StatusCode(500, ModelState);
            }
          

        }

        [HttpGet]
        [Route("GetSuggestedSpecializations")]
        [ProducesResponseType(200, Type = typeof(List<SpecializationDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSuggestedSpecializations( int Id)
        {
            SpecializationDTO specialization = new SpecializationDTO();
            List<SpecializationDTO> specializations = new List<SpecializationDTO>();
            try
            {
                var spemasters = await _patientRepository.GetSuggestedSpcialiazationByPatientCaseID(Id);

               
                foreach (var item in spemasters)
                {
                    specialization = _mapper.Map<SpecializationDTO>(item);
                    specializations.Add(specialization);
                }
                if (specializations != null)
                {
                    return Ok(specializations);
                }
                else
                {
                    ModelState.AddModelError("GetSuggestedSpecializations", "Specialization detail did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetSuggestedSpecializations", $"Something went wrong when Get all Specialization {ex.Message}");
                _logger.LogError("Exception in GetSuggestedSpecializations API " + ex);
                return StatusCode(500, ModelState);
            }
        }






    }
}
