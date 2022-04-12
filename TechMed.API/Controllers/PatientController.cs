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

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IMapper _mapper;       
        private readonly IPatientRepository _patientRepository;
        public PatientController(IMapper mapper, TeleMedecineContext teleMedecineContext, IPatientRepository patientRepository)
        {
            this._mapper = mapper;         
            this._patientRepository = patientRepository;
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
                var patientDetails = _mapper.Map<PatientMaster>(patientdto);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (_patientRepository.IsPatientExist(patientDetails))
                {
                    ModelState.AddModelError("AddPatient", "Patient name already in system");
                    return StatusCode(404, ModelState);
                }
                //newCreatedPatient = await this._patientRepository.Create(patientDetails);
                newCreatedPatient = await this._patientRepository.AddPatient(patientDetails);
                if (newCreatedPatient == null)
                {
                    ModelState.AddModelError("AddPatient", $"Something went wrong when create Patient {patientdto.FirstName}");
                    return StatusCode(404, ModelState);
                }
                else
                {                  
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


    }
}
