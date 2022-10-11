using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;
using Microsoft.AspNetCore.Authorization;
using TechMed.BL.ViewModels;
using TechMed.BL.CommanClassesAndFunctions;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "SuperAdmin,SysAdmin,PHCUser")]
    public class PHCController : ControllerBase
    {
        private readonly IMapper _mapper;       
        private readonly IPHCRepository _phcRepository;
        private readonly ILogger<PHCController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PHCController(IMapper mapper, TeleMedecineContext teleMedecineContext, IPHCRepository phcRepository, ILogger<PHCController> logger, IWebHostEnvironment webHostEnvironment)
        {
            this._mapper = mapper;
            this._phcRepository = phcRepository;
            this._logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("GetPHCDetailsByID")]
        //[HttpGet("{id:int}", Name = "GetPHCDetailsByID")]
        [ProducesResponseType(200, Type = typeof(PHCHospitalDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPHCDetailsByID(int id)
        {
            try
            {               
                var phcmaster = await _phcRepository.GetByID(id);              
                if (phcmaster != null)
                {
                    var phcMasterDTO = _mapper.Map<PHCHospitalDTO>(phcmaster);
                    return Ok(phcmaster);
                }
                else
                {
                    ModelState.AddModelError("GetPHCDetailsByID", "PHC detail did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetPHCDetailsByID", $"Something went wrong when GetPHCDetails {ex.Message}");
                _logger.LogError("Exception in GetPHCDetailsByID API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetPHCDetailsByEmailID")]
        [ProducesResponseType(200, Type = typeof(PHCDetailsIdsVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPHCDetailsByEmailID(string email)
        {
            PHCDetailsIdsVM pHCDetailsVM = new PHCDetailsIdsVM();
            try
            {
                pHCDetailsVM = await _phcRepository.GetPHCDetailByEmailID(email);
                if (pHCDetailsVM != null)
                {
                    //var phcMasterDTO = _mapper.Map<PHCHospitalDTO>(phcmaster);
                    return Ok(pHCDetailsVM);
                }
                else
                {
                    ModelState.AddModelError("GetPHCDetailsByEmailID", "PHC detail did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPHCDetailsByEmailID", $"Something went wrong when GetPHCDetailsByUserID {ex.Message}");
                _logger.LogError("Exception in GetPHCDetailsByEmailID API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        
        [HttpGet]
        [Route("SearchPHCDetailByName")]
        [ProducesResponseType(200, Type = typeof(SearchPHCDetailsIdsVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchPHCDetailByName(string name)
        {
            List<SearchPHCDetailsIdsVM> pHCDetailsVM = new List<SearchPHCDetailsIdsVM>();
            try
            {
                pHCDetailsVM = await _phcRepository.SearchPHCDetailByName(name);
                if (pHCDetailsVM != null)
                {
                    //var phcMasterDTO = _mapper.Map<PHCHospitalDTO>(phcmaster);
                    return Ok(pHCDetailsVM);
                }
                else
                {
                    ModelState.AddModelError("GetPHCDetailsByEmailID", "PHC detail did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPHCDetailsByEmailID", $"Something went wrong when GetPHCDetailsByUserID {ex.Message}");
                _logger.LogError("Exception in GetPHCDetailsByEmailID API " + ex);
                return StatusCode(500, ModelState);
            }
        }
        

        [HttpGet]
        [Route("GetPHCDetailByName")]
        [ProducesResponseType(200, Type = typeof(SearchPHCDetailsIdsVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPHCDetailByName(string name)
        {
            SearchPHCDetailsIdsVM pHCDetailsVM = new SearchPHCDetailsIdsVM();
            try
            {
                pHCDetailsVM = await _phcRepository.GetPHCDetailByByName(name);
                if (pHCDetailsVM != null)
                {
                    //var phcMasterDTO = _mapper.Map<PHCHospitalDTO>(phcmaster);
                    return Ok(pHCDetailsVM);
                }
                else
                {
                    ModelState.AddModelError("GetPHCDetailsByEmailID", "PHC detail did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPHCDetailsByEmailID", $"Something went wrong when GetPHCDetailsByUserID {ex.Message}");
                _logger.LogError("Exception in GetPHCDetailsByEmailID API " + ex);
                return StatusCode(500, ModelState);
            }
        }


        [HttpGet]
        [Route("GetAllPHCName")]
        [ProducesResponseType(200, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPHCName()
        {
            List<string> phCName = new List<string>();  
            try
            {
                phCName = await _phcRepository.GetAllPHCName();
                if (phCName != null)
                {
                    //var phcMasterDTO = _mapper.Map<PHCHospitalDTO>(phcmaster);
                    return Ok(phCName);
                }
                else
                {
                    ModelState.AddModelError("GetPHCDetailsByEmailID", "PHC detail did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPHCDetailsByEmailID", $"Something went wrong when GetPHCDetailsByUserID {ex.Message}");
                _logger.LogError("Exception in GetPHCDetailsByEmailID API " + ex);
                return StatusCode(500, ModelState);
            }
        }
        [HttpGet]
        [Route("GetPHCDetailsByUserID")]
        [ProducesResponseType(200, Type = typeof(PHCHospitalDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPHCDetailsByUserID(int userId)
        {
            try
            {
                var phcmaster = await _phcRepository.GetByPHCUserID(userId);
                if (phcmaster != null)
                {
                    var phcMasterDTO = _mapper.Map<PHCHospitalDTO>(phcmaster);
                    return Ok(phcMasterDTO);
                }
                else
                {
                    ModelState.AddModelError("GetPHCDetailsByUserID", "PHC detail did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPHCDetailsByUserID", $"Something went wrong when GetPHCDetailsByUserID {ex.Message}");
                _logger.LogError("Exception in GetPHCDetailsByUserID API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetPHCDetails")]
        [ProducesResponseType(200, Type = typeof(PHCDetailsVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPHCDetails(int userId)
        {
            try
            {
                PHCDetailsVM phcDetails = await _phcRepository.GetPHCDetailByUserID(userId);
                if (phcDetails != null)
                {                    
                    return Ok(phcDetails);
                }
                else
                {
                    ModelState.AddModelError("GetPHCDetails", "PHC details did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPHCDetails", $"Something went wrong when GetPHCDetails:  {ex.Message}");
                _logger.LogError("Exception in GetPHCDetails API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("AddPHC")]
        [ProducesResponseType(201, Type = typeof(PHCDetailsVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       // [Authorize(Roles = "SuperAdmin,SysAdmin")]
        public async Task<IActionResult> AddPHC([FromBody] PHCHospitalDTO phcdto)
        {
            Phcmaster newCreatedPHC = new Phcmaster();

            try
            {
                var phcMaster = _mapper.Map<Phcmaster>(phcdto);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _phcRepository.IsPHCExit(phcdto.Phcname))
                {
                    ModelState.AddModelError("AddPHC", "Same name of PHC is already in system");
                    return StatusCode(404, ModelState);
                }
                if (await _phcRepository.IsUserMailExist(phcdto.MailId))
                {
                    ModelState.AddModelError("AddPHC", "Same user mail already in system");
                    return StatusCode(404, ModelState);
                }
                if (phcMaster != null)
                { 
                    string user = string.Empty;
                    if (User.Identity.IsAuthenticated)
                    {
                        user = User.Identity.Name;
                    }
                    phcMaster.CreatedOn = UtilityMaster.GetLocalDateTime();
                    phcMaster.UpdatedOn = UtilityMaster.GetLocalDateTime();
                    phcMaster.ClusterId = phcdto.ClusterId;
                    phcMaster.DivisionId = phcdto.DivisionId;
                    phcMaster.DistrictId = phcdto.DistrictId;
                    phcMaster.BlockId = phcdto.BlockId;
                    phcMaster.EmployeeName = phcdto.EmployeeName;


                    //phcMaster.CreatedBy = User.Identity.Name;
                    //phcMaster.UpdatedBy = phcdto.CreatedBy;
                    string password = UtilityMaster.CreateRandomPassword(12);
                    UserMaster userMaster = new UserMaster();
                    userMaster.Email = phcMaster.MailId;
                    userMaster.Name = phcMaster.Moname;
                    userMaster.Mobile = phcMaster.PhoneNo;
                    userMaster.HashPassword = EncodeAndDecordPassword.EncodePassword(password);
                    userMaster.LoginAttempts = 0;
                    userMaster.LastLoginAt = UtilityMaster.GetLocalDateTime();
                    userMaster.IsActive = true;
                    userMaster.IsPasswordChanged = false;
                    //userMaster.CreatedBy = phcdto.CreatedBy;
                    //userMaster.UpdatedBy = phcdto.CreatedBy;
                    userMaster.CreatedOn = UtilityMaster.GetLocalDateTime();
                    userMaster.UpdatedOn = UtilityMaster.GetLocalDateTime();

                    newCreatedPHC = await this._phcRepository.AddPHCUser(phcMaster, userMaster, password);

                   // newCreatedPHC = await this._phcRepository.Create(phcMaster);
                }
               
                if (newCreatedPHC == null)
                {
                    ModelState.AddModelError("AddPHC", $"Something went wrong when create PHC {phcdto.Phcname}");
                    return StatusCode(404, ModelState);
                }
                else
                {
                    var createdPHC = _mapper.Map<PHCHospitalDTO>(newCreatedPHC);
                    //PHCDetailsVM phcDetails = await _phcRepository.GetPHCDetailByUserID(newCreatedPHC.UserId);
                    return CreatedAtRoute(201, createdPHC);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("AddPHC", $"Something went wrong when create PHC {ex.Message}");
                _logger.LogError("Exception in AddPHC API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllPHC")]
        [ProducesResponseType(200, Type = typeof(List<PHCMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPHC(int districtId)
        {
            List<PHCMasterDTO> phcList = new List<PHCMasterDTO>();
            try
            {

                phcList = await _phcRepository.GetAllPHC(districtId);
                if (phcList != null)
                {
                    return Ok(phcList);
                }
                else
                {
                    ModelState.AddModelError("GetAllPHC", "PHC details did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetAllPHC", $"Something went wrong when Get All PHC:  {ex.Message}");
                _logger.LogError("Exception in GetAllPHC API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("AddEmployeeTraining")]
        [ProducesResponseType(201, Type = typeof(EmployeeTrainingDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       // [Authorize(Roles = "SuperAdmin,SysAdmin")]
        public async Task<IActionResult> AddEmployeeTraining([FromBody] EmployeeTrainingDTO employeeTrainingDTO)
        {
           
            try
            {
                EmployeeTrainingDTO employeeTraining = new EmployeeTrainingDTO();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                } 
                else 
                {
                    if(employeeTrainingDTO != null)
                    {
                        employeeTraining = await _phcRepository.AddEmployeeTraining(employeeTrainingDTO);

                        if(employeeTraining != null)
                        {
                            if(employeeTraining.Id > 0)
                            {
                                return CreatedAtRoute(201, employeeTraining);
                            }
                            else
                            {
                                ModelState.AddModelError("AddEmployeeTraining", $"Something went wrong when add employee training {employeeTrainingDTO.TrainingSubject}");
                                return StatusCode(404, ModelState);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("AddEmployeeTraining", $"Something went wrong when add employee training {employeeTrainingDTO.TrainingSubject}");
                            return StatusCode(404, ModelState);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("AddEmployeeTraining", $"Something went wrong when add employee training {employeeTrainingDTO.TrainingSubject}");
                        return StatusCode(404, ModelState);
                    }
                }
              
               
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("AddPHC", $"Something went wrong when create PHC {ex.Message}");
                _logger.LogError("Exception in GetAllPHC API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("UploadPHCDoc")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      //  [Authorize(Roles = "SuperAdmin,SysAdmin")]
        public async Task<IActionResult> UploadPHCDoc([FromForm] SpokeMaintenanceDTO spokeMaintenances)
        {
            bool status = false;
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("UploadPHCDoc", "Please check did not get data");
                    _logger.LogError("UploadPHCDoc model state is invalid " );
                    return BadRequest(ModelState);
                }
                else
                {                   
                    if (spokeMaintenances != null)
                    {
                        status = _phcRepository.PostSpokeMaintenance(spokeMaintenances, contentRootPath);
                        if (status)
                        {
                            _logger.LogInformation("UploadPHCDoc Sending successfull response");
                            return Ok(new { status = "success" }); 
                        }
                        else
                        {
                            ModelState.AddModelError("UploadPHCDoc", "File did not save.");
                            _logger.LogError("UploadPHCDoc File did not save.");
                            return BadRequest(ModelState);
                        }
                        // return Ok();
                    }
                    else
                    {
                        ModelState.AddModelError("UploadPHCDoc", "Model has null value.");
                        _logger.LogError("UploadPHCDoc Model has null value.");
                        return BadRequest(ModelState);
                    }

                }
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Exception in PHC module " + ex);
                ModelState.AddModelError("UploadPHCDoc", $"Something went wrong when uplod file {ex.Message}");
                _logger.LogError("Exception in UploadPHCDoc module " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [Route("UpdatePHCDetails")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "SuperAdmin,SysAdmin")]
        public async Task<IActionResult> UpdatePHCDetails(UpdatePHCDTO updatePHCDTO)
        {

            try
            {
                _logger.LogInformation($"Update PHC Details : call web api add UpdatePHCDetails");
                if (updatePHCDTO == null || updatePHCDTO.Id < 1 || !ModelState.IsValid)
                {
                    _logger.LogError("UpdatePHCDetails : ModelState is invalid");
                    return BadRequest(ModelState);
                }
                string user = User.Identity.Name;
                var DTO = await _phcRepository.UpdatePHCDetails(updatePHCDTO, user.Trim());
                if (DTO)
                {
                    _logger.LogInformation($"UpdatePHCDetails : Sucess response returned ");
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not updated!");
                    _logger.LogError("UpdatePHCDetails : Data not updated");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in UpdateDoctorDetails API " + ex);
                return StatusCode(500, ModelState);
            }
        }
    }
}
