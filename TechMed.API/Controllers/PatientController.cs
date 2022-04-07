using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TechMed.BL.DTOMaster;
using TechMed.BL.ModelMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> Post([FromBody] PatientMasterDTO patientdto)
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
                newCreatedPatient = await this._patientRepository.Create(patientDetails);
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
        //[HttpPost]
        //[Consumes(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> CreatePatientAsync(PatientMaster patient)
        //{
        //    PatientMaster addedPatientMaster = new PatientMaster();
        //    if (patient ==null)
        //    {
        //        return BadRequest();
        //    }

        //    addedPatientMaster = await _patientRepository.AddPatient(patient);

        //    return CreatedAtAction(nameof(_patientRepository.GetPatientByID), new { Id = patient.Id }, patient);
        //}
    }
}
