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
        public async Task<PatientMaster> Post([FromBody] PatientMasterDTO patientdto)
        {
            if (patientdto == null)
            {
                throw new ArgumentNullException("patient");
            }

            if (string.IsNullOrEmpty(patientdto.FirstName))
            {
                throw new ArgumentNullException("FirstName");
            }
            if (!ModelState.IsValid)
            {
                throw new ArgumentNullException("Model state not valid");
            }

            var patientDetails = _mapper.Map<PatientMaster>(patientdto);
            var createdPatient = await this._patientRepository.Create(patientDetails); 
            if (createdPatient == null)
            {                
                throw new ArgumentNullException($"Something went wrong when create park {patientdto.FirstName}");
            } 

            return createdPatient;
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
