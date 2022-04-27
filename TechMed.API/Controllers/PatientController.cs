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
                _logger.LogInformation($"Add Patient : call web api add patient");
                var patientDetails = _mapper.Map<PatientMaster>(patientdto);
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Add Patient : model state is invalid");
                    return BadRequest(ModelState);
                }
                _logger.LogInformation($"Add Patient : going to check Is Patient Exist.");
                if (_patientRepository.IsPatientExist(patientDetails))
                {
                    _logger.LogInformation($"Add Patient : Patient is already in system.");
                    ModelState.AddModelError("AddPatient", "Patient name already in system");
                    return StatusCode(404, ModelState);
                }
                //newCreatedPatient = await this._patientRepository.Create(patientDetails);
                _logger.LogInformation($"Add Patient : call get patient id.");
                patientDetails.PatientId = this._patientRepository.GetPatientId();
                _logger.LogInformation($"Add Patient : get patient id." + patientDetails.PatientId);
                _logger.LogInformation($"Add Patient : call add patient method ");
                string fileName = _patientRepository.SaveImage(patientdto.Photo, contentRootPath);
                patientDetails.Photo = webRootPath + fileName;
                newCreatedPatient = await this._patientRepository.AddPatient(patientDetails);
                if (newCreatedPatient == null)
                {
                    _logger.LogInformation($"Add Patient : Patient did not added in the database ");
                    ModelState.AddModelError("AddPatient", $"Something went wrong when create Patient {patientdto.FirstName}");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    _logger.LogInformation($"Add Patient : Patient successfully added in the database ");
                    var createdPatient = _mapper.Map<PatientMasterDTO>(newCreatedPatient);
                    return CreatedAtRoute(201, createdPatient);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("AddPatient", $"Something went wrong when create Patient {ex.Message}");
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
                    ModelState.AddModelError("GetTodaysPatient", $"Something went wrong when get today's patient list");
                    return StatusCode(404, ModelState);
                }
                else
                {                    
                    return StatusCode(200, todaysPatientList);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetTodaysPatient", $"Something went wrong when get today's patient list {ex.Message}");
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
                    ModelState.AddModelError("GetConsultedPatient", $"Something went wrong when get today's patient list");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    return StatusCode(200, todaysPatientList);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetConsultedPatient", $"Something went wrong when get today's patient list {ex.Message}");
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
                    ModelState.AddModelError("GetPatientCount", $"Something went wrong when get today's patient count");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    return StatusCode(200, patientCount);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPatientCount", $"Something went wrong when get today's patient count {ex.Message}");
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
                    ModelState.AddModelError("GetTodaysPatient", $"Something went wrong when get today's patient list");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    return StatusCode(200, todaysPatientList);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetTodaysPatient", $"Something went wrong when get today's patient list {ex.Message}");
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
                    ModelState.AddModelError("GetYesterdaysPatient", $"Something went wrong when get yesterday's patient list");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    return StatusCode(200, patientList);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetYesterdaysPatient", $"Something went wrong when get yesterday's patient list {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }


      


    }
}
