using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.DTOMaster;
using TechMed.BL.ModelMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

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
            string userId = string.Empty;
            int phcID = 0;
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
                   
                    patientcase.CaseFileNumber = _patientCaeRepository.GetCaseFileNumber().ToString();
                    patientcase.PatientId = patientCasevm.PatientID;
                    patientcase.SpecializationId = patientCasevm.SpecializationID;
                    patientcase.CaseHeading = patientCasevm.CaseTitle;
                    patientcase.Opdno = patientCasevm.OPDNumber;                   
                    patientcase.CreatedOn = UtilityMaster.GetLocalDateTime();
                    patientcase.UpdatedOn = UtilityMaster.GetLocalDateTime();
                    patientcase.CaseStatusID = 1;
                    if (patientCasevm.CreatedBy > 0)
                    {
                        patientcase.CreatedBy = patientCasevm.CreatedBy;
                        patientcase.UpdatedBy = patientCasevm.CreatedBy;
                    }
                    else
                    {
                        userId = User.Identity.Name;
                        if(userId != null)
                        {
                            phcID = _patientCaeRepository.GetLoggedPHCID(userId);
                            if(phcID > 0)
                            {
                                patientcase.CreatedBy = phcID;
                                patientcase.UpdatedBy = phcID;
                            }
                           
                        }
                       
                        
                    }


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
                        createdPatientCase.CaseStatusID = patientCaseNew.CaseStatusID;
                    }

                }
                if (patientCaseNew == null)
                {
                    ModelState.AddModelError("CreatePatientCase", $"did not create patient case {patientCasevm?.PatientID}");
                    _logger.LogError("CreatePatientCase : did not create patient case ");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    _logger.LogInformation($"CreatePatientCase : Sucess response returned ");
                    return CreatedAtRoute(201, createdPatientCase);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("CreatePatientCase", $"Something went wrong when create patient case {ex.Message}");
                _logger.LogError("Exception in CreatePatientCase API " + ex);
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
                    _logger.LogError($"Please provide patient id and PHCID {PatientId} and {PHCId}");
                    return StatusCode(404, ModelState);
                }
                patientcase = await _patientCaeRepository.GetPatientCaseDetails(PHCId, PatientId);
                if (patientcase != null)
                {
                    _logger.LogInformation($"GetPatientCaseDetails : Sucess response returned ");
                    return StatusCode(200, patientcase);
                }
                else
                {
                    ModelState.AddModelError("GetPatientCaseDetails", $"did not get patient case {PatientId}");
                    _logger.LogError("did not get patient case " );
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPatientCaseDetails", $"Something went wrong when get patient case {ex.Message}");
                _logger.LogError("Exception in GetPatientCaseDetails API " + ex);
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
                    _logger.LogError("AddPatientCaseDetails : ModelState is invalid ");
                    return BadRequest(ModelState);
                }
                if (patientCasevm != null)
                {
                    updatedPatientCasevm = await _patientCaeRepository.PostPatientCaseDetails(patientCasevm);
                    if (updatedPatientCasevm != null)
                    {
                        _logger.LogInformation($"AddPatientCaseDetails : Sucess response returned ");
                        return CreatedAtRoute(201, updatedPatientCasevm);
                    }
                    else
                    {
                        ModelState.AddModelError("AddPatientCaseDetails", $"did not add patient case details");
                        _logger.LogError("AddPatientCaseDetails :did not add patient case details ");
                        return StatusCode(404, ModelState);
                    }

                }
                else
                {
                    ModelState.AddModelError("AddPatientCaseDetails", "Patient case model state is not valid");
                    _logger.LogError("AddPatientCaseDetails :ModelState is null ");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("AddPatientCaseDetails", $"Something went wrong when add case details {ex.Message}");
                _logger.LogError("Exception in AddPatientCaseDetails API " + ex);
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
                    //var bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                    updatedPatientCasevm = await _patientCaeRepository.PostPatientReferToDoctor(referDoctor);
                    if (updatedPatientCasevm != null)
                    {
                        if(updatedPatientCasevm.Status.ToLower() == "success")
                        {
                            _logger.LogInformation($"AddReferDoctorInPatientCase : Sucess response returned ");
                            return CreatedAtRoute(201, updatedPatientCasevm);
                        }
                        else
                        {
                            _logger.LogInformation($"AddReferDoctorInPatientCase : Fail response returned ");
                            return StatusCode(404, updatedPatientCasevm);
                        }
                        
                    }
                    else
                    {
                        ModelState.AddModelError("AddReferDoctorInPatientCase", $"did not refer patient case to doctor");
                        _logger.LogError("AddReferDoctorInPatientCase :did not refer patient case to doctor");
                        return StatusCode(404, ModelState);
                    }

                }
                else
                {
                    ModelState.AddModelError("AddReferDoctorInPatientCase", "Refer doctor model state is not valid");
                    _logger.LogError("AddReferDoctorInPatientCase :refer doctor has null value");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("AddReferDoctorInPatientCase", $"Something went wrong when add refer doctor {ex.Message}");
                _logger.LogError("Exception in AddReferDoctorInPatientCase API " + ex);
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
                    _logger.LogInformation($"GetPatientCaseQueue : Sucess response returned ");
                    return StatusCode(200, patientcasequeue);
                }
                else
                {
                    ModelState.AddModelError("GetPatientCaseQueue", $"did not get patient queue {PatientId}");
                    _logger.LogError("GetPatientCaseQueue : did not get patient queue ");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPatientCaseQueue", $"Something went wrong when get patient queue {ex.Message}");
                _logger.LogError("Exception in GetPatientCaseQueue API " + ex);
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
                    _logger.LogError("PostFeedback : ModelState is invalid ");
                    return BadRequest(ModelState);
                }
                if (feedbackDTO != null)
                {
                    updatedFeedback = await _patientCaeRepository.PostPatientFeedBack(feedbackDTO);
                    if (updatedFeedback != null)
                    {
                        _logger.LogInformation($"PostFeedback : Sucess response returned ");
                        return CreatedAtRoute(201, updatedFeedback);
                    }
                    else
                    {
                        ModelState.AddModelError("PostFeedback", $"did not save feedback data in database");
                        _logger.LogError("PostFeedback : did not save feedback data in database");
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
                _logger.LogError("Exception in PostFeedback API " + ex);
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
                    _logger.LogError("UploadCaseDoc : ModelState is invalid ");
                    return BadRequest(ModelState);
                }
                else
                {
                    if (caseDocumentVM.Count > 0)
                    {
                        //status = _patientCaeRepository.SaveCaseDocument(caseDocumentVM, contentRootPath);
                        status = _patientCaeRepository.UploadCaseDoc(caseDocumentVM, contentRootPath);
                        if (status)
                        {
                            _logger.LogInformation($"UploadCaseDoc : Sucess response returned ");
                            return Ok(new { status = "success" });
                        }
                        else
                        {
                            ModelState.AddModelError("UploadCaseDoc", "File did not save.");
                            _logger.LogError("UploadCaseDoc : File did not save ");
                            return BadRequest(ModelState);
                        }
                        // return Ok();
                    }
                    else
                    {
                        ModelState.AddModelError("UploadCaseDoc", "Model has null value.");
                        _logger.LogError("UploadCaseDoc : model count is zero ");
                        return BadRequest(ModelState);
                    }

                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("UploadCaseDoc", $"Something went wrong when uplod file {ex.Message }");
                if (ex.InnerException != null)
                {
                    ModelState.AddModelError("UploadCaseDoc", $"Something went wrong when uplod file {ex.Message + ex.InnerException.StackTrace }");
                }
                _logger.LogError("Exception in UploadCaseDoc API " + ex);
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
                    _logger.LogError("GetPatientCaseDocList : PatientcaseId is null ");
                    return StatusCode(404, ModelState);
                }
                rootUrl = this._myConfiguration.baseUrl;
                caseDocList = await _patientCaeRepository.GetPatientCaseDocList(PatientCaseID, rootUrl);
                if (caseDocList != null)
                {
                    _logger.LogInformation($"GetPatientCaseDocList : Sucess response returned " + PatientCaseID);
                    return StatusCode(200, caseDocList);
                }
                else
                {
                    ModelState.AddModelError("GetPatientCaseDocList", $"did not get document list for {PatientCaseID}");
                    _logger.LogError("GetPatientCaseDocList : did not get document list for "+ PatientCaseID);
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPatientCaseDocList", $"Something went wrong when get patient case doc {ex.Message}");
                _logger.LogError("Exception in GetPatientCaseDocList API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetPatientCaseDetailsByCaseID")]
        [ProducesResponseType(200, Type = typeof(PatientCaseVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientCaseDetailsByCaseID(long PatientCaseID = 0)
        {
            PatientCaseVM patientcase = new PatientCaseVM();
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            try
            {
                if (PatientCaseID == 0)
                {
                    ModelState.AddModelError("GetPatientCaseDetailsByCaseID", "Please provide patient case Id");
                    _logger.LogError("GetPatientCaseDetailsByCaseID : PatientcaseId is null ");
                    return StatusCode(404, ModelState);
                }
                patientcase = await _patientCaeRepository.GetPatientCaseDetailsByCaseID(PatientCaseID, contentRootPath);
                if (patientcase != null)
                {
                    _logger.LogInformation($"GetPatientCaseDetailsByCaseID : Sucess response returned " + PatientCaseID);
                    return StatusCode(200, patientcase);
                }
                else
                {
                    ModelState.AddModelError("GetPatientCaseDetailsByCaseID", $"did not get patient case for PatientcaseID : {PatientCaseID}");
                    _logger.LogError("GetPatientCaseDetailsByCaseID : did not get patient case for PatientcaseID : "+ PatientCaseID);
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPatientCaseDetails", $"Something went wrong when get patient case {ex.Message}");
                _logger.LogError("Exception in GetPatientCaseDetails API " + ex);
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
                    ModelState.AddModelError("GetPatientCaseLevels", "Please provide patient Id");
                    _logger.LogError("GetPatientCaseLevels : PatientcaseId is null ");
                    return StatusCode(404, ModelState);
                }
                patientcase = await _patientCaeRepository.GetPatientCaseLevels(PatientID);
                if (patientcase != null)
                {
                    _logger.LogInformation($"GetPatientCaseLevels : Sucess response returned " + PatientID);
                    return StatusCode(200, patientcase);
                }
                else
                {
                    ModelState.AddModelError("GetPatientCaseLevels", $"did not get patient case level for PatientID: {PatientID}");
                    _logger.LogError("GetPatientCaseLevels : did not get patient case level for PatientID " + PatientID);
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPatientCaseLevels", $"Something went wrong when get patient case level {ex.Message}");
                _logger.LogError("Exception in GetPatientCaseLevels API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetSelectedOnlineDoctors")]
        [ProducesResponseType(200, Type = typeof(OnlineDoctorListVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSelectedOnlineDoctors(long PatientCaseID = 0)
        {
            OnlineDoctorListVM onlineDrLists = new OnlineDoctorListVM();
            try
            {
                if (PatientCaseID == 0)
                {
                    ModelState.AddModelError("GetSelectedOnlineDoctors", "Please provide patient case Id");
                    _logger.LogError("GetSelectedOnlineDoctors : Patient case Id is null ");
                    return StatusCode(404, ModelState);
                }
                onlineDrLists = await _patientCaeRepository.GetSelectedOnlineDoctors(PatientCaseID);
                if (onlineDrLists != null)
                { 
                    if(onlineDrLists.Status.ToLower()=="success")
                    {
                        _logger.LogInformation($"GetSelectedOnlineDoctors : Sucess response returned " + PatientCaseID);
                        return StatusCode(200, onlineDrLists);
                    }
                    else
                    {
                        return StatusCode(404, onlineDrLists);
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("GetSelectedOnlineDoctors", $"did not get doctor list for PatientcaseID : {PatientCaseID}");
                    _logger.LogError("GetSelectedOnlineDoctors : did not get doctor list for PatientcaseID : " + PatientCaseID);
                    return StatusCode(404, onlineDrLists);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetSelectedOnlineDoctors", $"Something went wrong when get online doctor list {ex.Message}");
                _logger.LogError("Exception in GetSelectedOnlineDoctors API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetPatientQueueByDoctors")]
        [ProducesResponseType(200, Type = typeof(List<PatientQueueByDoctor>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientQueueByDoctors(int SpecializationID)
        {
            List<PatientQueueByDoctor> patientQueues = new List<PatientQueueByDoctor>();
            try
            {
                patientQueues = await _patientCaeRepository.GetPatientQueueByDoctor(SpecializationID);
                if (patientQueues != null && patientQueues.Count > 0)
                {
                    _logger.LogInformation($"GetSelectedOnlineDoctors : Sucess response returned " );
                    return StatusCode(200, patientQueues);
                }
                else
                {
                    ModelState.AddModelError("GetSelectedOnlineDoctors", $"did not get doctor list for PatientcaseID ");
                    _logger.LogError("GetSelectedOnlineDoctors : did not get patient Queue : ");
                    return StatusCode(404, new { Message = "Doctor do not available for this specialization right now." });
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetSelectedOnlineDoctors", $"Something went wrong when get patient queue by doctor {ex.Message}");
                _logger.LogError("Exception in GetSelectedOnlineDoctors API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetPatientQueue")]
        [ProducesResponseType(200, Type = typeof(List<PatientQueueVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientQueue(int PHCID)
        {
            List<PatientQueueVM> patientQueues = new List<PatientQueueVM>();
            try
            {
                patientQueues = await _patientCaeRepository.GetPatientQueue(PHCID);
                if (patientQueues != null && patientQueues.Count > 0)
                {
                    _logger.LogInformation($"GetSelectedOnlineDoctors : Sucess response returned ");
                    return StatusCode(200, patientQueues);
                }
                else
                {
                    ModelState.AddModelError("GetSelectedOnlineDoctors", $"did not get doctor list for PatientcaseID ");
                    _logger.LogError("GetSelectedOnlineDoctors : did not get patient Queue : ");
                    return StatusCode(404, new { Message = "Did not get patient Queue" });
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetSelectedOnlineDoctors", $"Something went wrong when get patient queue by doctor {ex.Message}");
                _logger.LogError("Exception in GetSelectedOnlineDoctors API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetPatientQueueByDoctorID")]
        [ProducesResponseType(200, Type = typeof(List<PatientQueueVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientQueueByDoctorID(int doctorID)
        {
            List<PatientQueueVM> patientQueues = new List<PatientQueueVM>();
            try
            {
                patientQueues = await _patientCaeRepository.GetPatientQueueByDocotorID(doctorID);
                if (patientQueues != null && patientQueues.Count > 0)
                {
                    _logger.LogInformation($"GetPatientQueueByDoctorID : Sucess response returned ");
                    return StatusCode(200, patientQueues);
                }
                else
                {
                    ModelState.AddModelError("GetPatientQueueByDoctorID", $"did not get doctor list for PatientcaseID ");
                    _logger.LogError("GetPatientQueueByDoctorID : did not get patient Queue : ");
                    return StatusCode(404, new { Message = "Did not get patient Queue" });
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPatientQueueByDoctorID", $"Something went wrong when get patient queue by doctor {ex.Message}");
                _logger.LogError("Exception in GetPatientQueueByDoctorID API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("AddPatientInDoctorsQueue")]
        [ProducesResponseType(201, Type = typeof(PatientReferToDoctorVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPatientInDoctorsQueue([FromBody] PatientReferToDoctorVM referDoctor)
        {
            PatientReferToDoctorVM updatedPatientCasevm = new PatientReferToDoctorVM();

            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("AddPatientInDoctorsQueue", "Patient case model state is not valid");
                    return BadRequest(ModelState);
                }
                if (referDoctor != null)
                {
                    updatedPatientCasevm = await _patientCaeRepository.AddPatientInDoctorsQueue(referDoctor);
                    if (updatedPatientCasevm != null)
                    {
                        if (updatedPatientCasevm.Status.ToLower() == "success")
                        {
                            _logger.LogInformation($"AddPatientInDoctorsQueue : Sucess response returned ");
                            return CreatedAtRoute(201, updatedPatientCasevm);
                        }
                        else
                        {
                            _logger.LogInformation($"AddPatientInDoctorsQueue : Fail response returned ");
                            return StatusCode(404, updatedPatientCasevm);
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("AddPatientInDoctorsQueue", $"did not refer patient case to doctor");
                        _logger.LogError("AddPatientInDoctorsQueue :did not refer patient case to doctor");
                        return StatusCode(404, ModelState);
                    }

                }
                else
                {
                    ModelState.AddModelError("AddReferDoctorInPatientCase", "Refer doctor model state is not valid");
                    _logger.LogError("AddReferDoctorInPatientCase :refer doctor has null value");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("AddReferDoctorInPatientCase", $"Something went wrong when add refer doctor {ex.Message}");
                _logger.LogError("Exception in AddReferDoctorInPatientCase API " + ex);
                return StatusCode(500, ModelState);
            }
        }
        [HttpPost]
        [Route("RemovePatientFromDoctorsQueue")]
        [ProducesResponseType(201, Type = typeof(RemovePatientFromQueueVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemovePatientFromDoctorsQueue(long PatientCaseID = 0)
        {
            RemovePatientFromQueueVM removePatientFromQueue = new RemovePatientFromQueueVM();  
            try
            {              
                if (PatientCaseID > 0)
                {
                    removePatientFromQueue = await _patientCaeRepository.RemovePatientFromDoctorsQueue(PatientCaseID);
                    if (removePatientFromQueue != null)
                    {
                        if (removePatientFromQueue.Status.ToLower() == "success")
                        {
                            _logger.LogInformation($"RemovePatientFromDoctorsQueue : Sucess response returned ");                            
                            return StatusCode(200, removePatientFromQueue);
                        }
                        else
                        {
                            _logger.LogInformation($"RemovePatientFromDoctorsQueue : Fail response returned ");
                            return StatusCode(404, removePatientFromQueue);
                        }

                    }
                    else
                    {
                        removePatientFromQueue.Status = "fail";
                        removePatientFromQueue.ErrorMessage = "Do not remove patient from queue";
                        removePatientFromQueue.PatientCaseID = PatientCaseID;
                        removePatientFromQueue.PatientID = 0;
                        ModelState.AddModelError("RemovePatientFromDoctorsQueue", $"did not refer patient case to doctor");
                        _logger.LogError("RemovePatientFromDoctorsQueue :did not refer patient case to doctor");
                        return StatusCode(404, removePatientFromQueue);
                    }

                }
                else
                {
                    removePatientFromQueue.Status = "fail";
                    removePatientFromQueue.ErrorMessage = "Patient case ID has null value";
                    removePatientFromQueue.PatientCaseID = PatientCaseID;
                    removePatientFromQueue.PatientID = 0;
                    ModelState.AddModelError("RemovePatientFromDoctorsQueue", "Patient case ID has null value");
                    _logger.LogError("RemovePatientFromDoctorsQueue :Patient case ID has null value");
                    return StatusCode(404, removePatientFromQueue);
                }
            }
            catch (Exception ex)
            {
                removePatientFromQueue.Status = "fail";
                removePatientFromQueue.ErrorMessage = ex.Message;
                removePatientFromQueue.PatientCaseID = PatientCaseID;
                removePatientFromQueue.PatientID =  0;

                ModelState.AddModelError("RemovePatientFromDoctorsQueue", $"Something went wrong when remove refer doctor {ex.Message}");
                _logger.LogError("Exception in RemovePatientFromDoctorsQueue API " + ex);
                return StatusCode(500, removePatientFromQueue);
            }
        }

        [HttpGet]
        [Route("GetAllPHCPatientQueue")]
        [ProducesResponseType(200, Type = typeof(List<ALLPatientsQueueVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPHCPatientQueue()
        {
            List<ALLPatientsQueueVM> patientQueues = new List<ALLPatientsQueueVM>();
            try
            {
                patientQueues = await _patientCaeRepository.GetAllPatientQueue();
                if (patientQueues != null && patientQueues.Count > 0)
                {
                    _logger.LogInformation($"GetSelectedOnlineDoctors : Sucess response returned ");
                    return StatusCode(200, patientQueues);
                }
                else
                {
                    ModelState.AddModelError("GetSelectedOnlineDoctors", $"did not get doctor list for PatientcaseID ");
                    _logger.LogError("GetSelectedOnlineDoctors : did not get patient Queue : ");
                    return StatusCode(404, new { Message = "Did not get patient Queue" });
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetSelectedOnlineDoctors", $"Something went wrong when get patient queue by doctor {ex.Message}");
                _logger.LogError("Exception in GetSelectedOnlineDoctors API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllPandingPatientQueue")]
        [ProducesResponseType(200, Type = typeof(List<AllPendingPatient>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPandingPatientQueue()
        {
            List<AllPendingPatient> patientQueues = new List<AllPendingPatient>();
            try
            {
                patientQueues = await _patientCaeRepository.GetAllPendingPatient();
                if (patientQueues != null && patientQueues.Count > 0)
                {
                    _logger.LogInformation($"GetAllPandingPatientQueue : Sucess response returned ");
                    return StatusCode(200, patientQueues);
                }
                else
                {
                    ModelState.AddModelError("GetAllPandingPatientQueue", $"did not get panding list ");
                    _logger.LogError("GetAllPandingPatientQueue : did not get panding patient Queue : ");
                    return StatusCode(404, new { Message = "Did not get panding patient Queue" });
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetAllPandingPatientQueue", $"Something went wrong when get panding patient list {ex.Message}");
                _logger.LogError("Exception in GetAllPandingPatientQueue API " + ex);
                return StatusCode(500, ModelState);
            }
        }
    }
}
