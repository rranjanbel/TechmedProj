﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class PatientCaseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPatientCaseRepository _patientCaeRepository;
        private readonly ILogger<PatientCaseController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationRootUri _myConfiguration;
        public PatientCaseController(IMapper mapper, TeleMedecineContext teleMedecineContext, IPatientCaseRepository patientCaeRepository, ILogger<PatientCaseController> logger, IWebHostEnvironment webHostEnvironment, ApplicationRootUri myConfiguration)
        {
            this._mapper = mapper;
            this._patientCaeRepository = patientCaeRepository;
            this._logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._myConfiguration = myConfiguration;
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
                    patientcase.Opdno = patientCasevm.OPDNumber;
                    patientcase.CreatedBy = patientCasevm.CreatedBy;
                    patientcase.UpdatedBy = patientCasevm.CreatedBy;
                    patientcase.CreatedOn = DateTime.Now;
                    patientcase.UpdatedOn = DateTime.Now;

                    if (patientcase.CreatedBy == 0)
                        patientcase.CreatedBy = 2;
                    if (patientcase.UpdatedBy == 0)
                        patientcase.UpdatedBy = 2;

                    patientCaseNew = await this._patientCaeRepository.CreateAsync(patientcase);
                    if (patientCaseNew != null)
                    {
                        createdPatientCase.PatientID = patientCaseNew.PatientId;
                        createdPatientCase.CaseFileNumber = patientCaseNew.CaseFileNumber;
                        createdPatientCase.SpecializationID = patientCaseNew.SpecializationId;
                        createdPatientCase.CaseFileID = patientCaseNew.Id;
                        createdPatientCase.CreatedBy = patientCaseNew.CreatedBy;
                        createdPatientCase.CaseTitle = patientCaseNew.CaseHeading;
                        createdPatientCase.OPDNumber = patientCaseNew.Opdno;
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
        public async Task<IActionResult> GetPatientCaseDetails(int PHCId = 0, int PatientId = 0)
        {
            PatientCaseVM patientcase = new PatientCaseVM();
            try
            {
                if (PatientId == 0 && PHCId == 0)
                {
                    ModelState.AddModelError("GetPatientCaseDetails", "Please provide patient id and PHCID.");
                    return StatusCode(404, ModelState);
                }
                patientcase = await _patientCaeRepository.GetPatientCaseDetails(PHCId, PatientId);
                if (patientcase != null)
                {
                    return StatusCode(200, patientcase);
                }
                else
                {
                    ModelState.AddModelError("GetPatientCaseDetails", $"Something went wrong when get patient case {PatientId}");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPatientCaseDetails", $"Something went wrong when get patient case {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("AddPatientCaseDetails")]
        [ProducesResponseType(201, Type = typeof(PatientCaseDetailsVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPatientCaseDetails([FromBody] PatientCaseDetailsVM patientCasevm)
        {
            PatientCaseDetailsVM updatedPatientCasevm = new PatientCaseDetailsVM();

            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("AddPatientCaseDetails", "Patient case model state is not valid");
                    return BadRequest(ModelState);
                }          
                if (patientCasevm != null)
                {
                    updatedPatientCasevm = await _patientCaeRepository.PostPatientCaseDetails(patientCasevm);
                    if(updatedPatientCasevm != null)
                    {
                        return CreatedAtRoute(201, updatedPatientCasevm);
                    }
                    else
                    {
                        ModelState.AddModelError("AddPatientCaseDetails", $"Something went wrong when saving data in database");
                        return StatusCode(404, ModelState);
                    }

                }
                else
                {
                    ModelState.AddModelError("AddPatientCaseDetails", "Patient case model state is not valid");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("AddPatientCaseDetails", $"Something went wrong when add case details {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("AddReferDoctorInPatientCase")]
        [ProducesResponseType(201, Type = typeof(PatientReferToDoctorVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddReferDoctorInPatientCase([FromBody] PatientReferToDoctorVM referDoctor)
        {
            PatientReferToDoctorVM updatedPatientCasevm = new PatientReferToDoctorVM();

            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("AddReferDoctorInPatientCase", "Patient case model state is not valid");
                    return BadRequest(ModelState);
                }
                if (referDoctor != null)
                {
                    updatedPatientCasevm = await _patientCaeRepository.PostPatientReferToDoctor(referDoctor);
                    if (updatedPatientCasevm != null)
                    {
                        return CreatedAtRoute(201, updatedPatientCasevm);
                    }
                    else
                    {
                        ModelState.AddModelError("AddReferDoctorInPatientCase", $"Something went wrong when saving data in database");
                        return StatusCode(404, ModelState);
                    }

                }
                else
                {
                    ModelState.AddModelError("AddReferDoctorInPatientCase", "Refer doctor model state is not valid");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("AddReferDoctorInPatientCase", $"Something went wrong when add refer doctor {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetPatientCaseQueue")]
        [ProducesResponseType(200, Type = typeof(PatientCaseWithDoctorVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientCaseQueue(int PHCId = 0, int PatientId = 0)
        {
            PatientCaseWithDoctorVM patientcasequeue = new PatientCaseWithDoctorVM();
            try
            {
                if (PatientId == 0 && PHCId == 0)
                {
                    ModelState.AddModelError("GetPatientCaseQueue", "Please provide patient id and PHCID.");
                    return StatusCode(404, ModelState);
                }
                patientcasequeue = await _patientCaeRepository.GetPatientQueueDetails(PHCId, PatientId);
                if (patientcasequeue != null)
                {
                    return StatusCode(200, patientcasequeue);
                }
                else
                {
                    ModelState.AddModelError("GetPatientCaseQueue", $"Something went wrong when get patient queue {PatientId}");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPatientCaseQueue", $"Something went wrong when get patient queue {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("PostFeedback")]
        [ProducesResponseType(201, Type = typeof(PatientFeedbackDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostFeedback([FromBody] PatientFeedbackDTO feedbackDTO)
        {
            PatientFeedbackDTO updatedFeedback = new PatientFeedbackDTO();

            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("PostFeedback", "Patient feedback model state is not valid");
                    return BadRequest(ModelState);
                }
                if (feedbackDTO != null)
                {
                    updatedFeedback = await _patientCaeRepository.PostPatientFeedBack(feedbackDTO);
                    if (updatedFeedback != null)
                    {
                        return CreatedAtRoute(201, updatedFeedback);
                    }
                    else
                    {
                        ModelState.AddModelError("PostFeedback", $"Something went wrong when saving data in database");
                        return StatusCode(404, ModelState);
                    }

                }
                else
                {
                    ModelState.AddModelError("PostFeedback", "Patient feedback model state is not valid");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("PostFeedback", $"Something went wrong when add patient feedback {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("UploadCaseDoc")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadCaseDoc([FromForm] List<CaseDocumentVM> caseDocumentVM)
        {
            bool status = false;
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("UploadCaseDoc", "Please check did not get data");
                    return BadRequest(ModelState);
                }
                else
                {
                    if(caseDocumentVM.Count > 0)
                    {
                        status = _patientCaeRepository.SaveCaseDocument(caseDocumentVM, contentRootPath);
                        if (status)
                        {
                            return Ok(new { status = "success" });
                        }
                        else
                        {
                            ModelState.AddModelError("UploadCaseDoc", "File did not save.");
                            return BadRequest(ModelState);
                        }
                        // return Ok();
                    }
                    else
                    {
                        ModelState.AddModelError("UploadCaseDoc", "Model has null value.");
                        return BadRequest(ModelState);
                    }

                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("UploadCaseDoc", $"Something went wrong when uplod file {ex.Message}");
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetPatientCaseDocList")]
        [ProducesResponseType(200, Type = typeof(List<PatientCaseDocDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientCaseDocList(int PatientCaseID = 0)
        {
            List<PatientCaseDocDTO> caseDocList = new List<PatientCaseDocDTO>();
            string rootUrl = string.Empty;
            try
            {
                if (PatientCaseID == 0)
                {
                    ModelState.AddModelError("GetPatientCaseDocList", "Please provide patient case Id");
                    return StatusCode(404, ModelState);
                }
                rootUrl = this._myConfiguration.Url;
                caseDocList = await _patientCaeRepository.GetPatientCaseDocList(PatientCaseID, rootUrl);
                if (caseDocList != null)
                {
                    return StatusCode(200, caseDocList);
                }
                else
                {
                    ModelState.AddModelError("GetPatientCaseDocList", $"Something went wrong when get patient case doc {PatientCaseID}");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPatientCaseDocList", $"Something went wrong when get patient case doc {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetPatientCaseDetailsByCaseID")]
        [ProducesResponseType(200, Type = typeof(PatientCaseVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientCaseDetailsByCaseID(int PatientCaseID = 0)
        {
            PatientCaseVM patientcase = new PatientCaseVM();
            try
            {
                if (PatientCaseID == 0)
                {
                    ModelState.AddModelError("GetPatientCaseDetailsByCaseID", "Please provide patient case Id");
                    return StatusCode(404, ModelState);
                }
                patientcase = await _patientCaeRepository.GetPatientCaseDetailsByCaseID(PatientCaseID);
                if (patientcase != null)
                {
                    return StatusCode(200, patientcase);
                }
                else
                {
                    ModelState.AddModelError("GetPatientCaseDetailsByCaseID", $"Something went wrong when get patient case {PatientCaseID}");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPatientCaseDetails", $"Something went wrong when get patient case {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetPatientCaseLevels")]
        [ProducesResponseType(200, Type = typeof(PatientCaseLevelDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientCaseLevels(int PatientID = 0)
        {
            PatientCaseLevelDTO patientcase = new PatientCaseLevelDTO();
            try
            {
                if (PatientID == 0)
                {
                    ModelState.AddModelError("GetPatientCaseLevels", "Please provide patient case Id");
                    return StatusCode(404, ModelState);
                }
                patientcase = await _patientCaeRepository.GetPatientCaseLevels(PatientID);
                if (patientcase != null)
                {
                    return StatusCode(200, patientcase);
                }
                else
                {
                    ModelState.AddModelError("GetPatientCaseLevels", $"Something went wrong when get patient case {PatientID}");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPatientCaseLevels", $"Something went wrong when get patient case {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }
    }
}
