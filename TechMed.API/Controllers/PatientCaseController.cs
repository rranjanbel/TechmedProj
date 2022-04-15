using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientCaseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPatientCaseRepository _patientCaeRepository;
        private readonly ILogger<PHCController> _logger;
        public PatientCaseController(IMapper mapper, TeleMedecineContext teleMedecineContext, IPatientCaseRepository patientCaeRepository, ILogger<PHCController> logger)
        {
            this._mapper = mapper;
            this._patientCaeRepository = patientCaeRepository;
            this._logger = logger;
        }
        [HttpPost]
        [Route("CreatePatientCase")]
        [ProducesResponseType(201, Type = typeof(PatientCaseCreateVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePatientCase([FromBody] PatientCaseCreateVM patientCasevm)
        {
            PatientCase patientcase = new PatientCase();
            PatientCase patientCaseNew = new PatientCase();
            PatientCaseCreateVM createdPatientCase = new PatientCaseCreateVM();
            try
            {              
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (_patientCaeRepository.IsPatientCaseExist(patientCasevm))
                {
                    ModelState.AddModelError("CreatePatientCase", "Patient case is already in system");
                    return StatusCode(404, ModelState);
                }
                if (patientCasevm != null)
                {
                    //Need to get here data from database runtime when not supplied by GUI

                    patientcase.CaseFileNumber = _patientCaeRepository.GetCaseFileNumber().ToString();
                    patientcase.PatientId = patientCasevm.PatientID;
                    patientcase.SpecializationId = patientCasevm.SpecializationID;
                    patientcase.CaseHeading = patientCasevm.CaseTitle;
                    patientcase.CreatedBy = patientCasevm.CreatedBy;
                    patientcase.UpdatedBy = patientCasevm.CreatedBy;
                    patientcase.CreatedOn = DateTime.Now;
                    patientcase.UpdatedOn = DateTime.Now;

                    if (patientcase.CreatedBy == 0)
                        patientcase.CreatedBy = 2;
                    if (patientcase.UpdatedBy == 0)
                        patientcase.UpdatedBy = 2;

                    patientCaseNew = await this._patientCaeRepository.CreateAsync(patientcase);
                    if(patientCaseNew != null)
                    {
                        createdPatientCase.PatientID = patientCaseNew.PatientId;
                        createdPatientCase.CaseFileNumber = patientCaseNew.CaseFileNumber;
                        createdPatientCase.SpecializationID = patientCaseNew.SpecializationId;
                        createdPatientCase.CaseFileID = patientCaseNew.Id;
                        createdPatientCase.CreatedBy = patientCaseNew.CreatedBy;
                        createdPatientCase.CaseTitle = patientCaseNew.CaseHeading;
                    }
                    
                }
                if (patientCaseNew == null)
                {
                    ModelState.AddModelError("AddPHC", $"Something went wrong when create PHC {patientCasevm.PatientID}");
                    return StatusCode(404, ModelState);
                }
                else
                {                   
                    return CreatedAtRoute(201, createdPatientCase);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("AddPHC", $"Something went wrong when create PHC {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetPatientCaseDetails")]
        [ProducesResponseType(200, Type = typeof(PatientCaseVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientCaseDetails(int PHCUserId, int PHCId, int PatientId =0)
        {
            PatientCaseVM patientcase = new PatientCaseVM();          
            try
            {
                if (PatientId == 0)
                {
                    ModelState.AddModelError("GetPatientCase", "Please check patient id.");
                    return StatusCode(404, ModelState);
                }               
                patientcase = await _patientCaeRepository.GetPatientCaseDetails(PHCUserId, PHCId, PatientId);
                if (patientcase != null)
                {
                    return StatusCode(200, patientcase);
                }               
                else
                {
                    ModelState.AddModelError("AddPHC", $"Something went wrong when get patient case {PatientId}");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("AddPHC", $"Something went wrong when create PHC {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }




    }
}
