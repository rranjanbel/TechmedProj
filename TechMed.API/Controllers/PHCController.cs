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
    public class PHCController : ControllerBase
    {
        private readonly IMapper _mapper;       
        private readonly IPHCRepository _phcRepository;
        private readonly ILogger<PHCController> _logger;
        public PHCController(IMapper mapper, TeleMedecineContext teleMedecineContext, IPHCRepository phcRepository, ILogger<PHCController> logger)
        {
            this._mapper = mapper;           
            this._phcRepository = phcRepository;
            this._logger = logger;
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
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("AddPHC")]
        [ProducesResponseType(201, Type = typeof(PHCDetailsVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                if (_phcRepository.IsPHCExit(phcdto.Phcname))
                {
                    ModelState.AddModelError("AddPHC", "Same name of PHC is already in system");
                    return StatusCode(404, ModelState);
                }
                if(phcMaster != null)
                {                     
                    phcMaster.CreatedOn = DateTime.Now;
                    phcMaster.UpdatedOn = DateTime.Now;
                    phcMaster.ClusterId = phcdto.ClusterId;
                    phcMaster.BlockId = phcdto.BlockId;
                    phcMaster.CreatedBy = phcdto.CreatedBy;
                    phcMaster.UpdatedBy = phcdto.CreatedBy;

                    UserMaster userMaster = new UserMaster();
                    userMaster.Email = phcMaster.MailId;
                    userMaster.Name = phcMaster.Moname;
                    userMaster.Mobile = phcMaster.PhoneNo;
                    userMaster.HashPassword = EncodeAndDecordPassword.EncodePassword("phc@12345"); 
                    userMaster.LoginAttempts = 0;
                    userMaster.LastLoginAt = DateTime.Now;
                    userMaster.IsActive = true;
                    userMaster.IsPasswordChanged = false;
                    userMaster.CreatedBy = phcdto.CreatedBy;
                    userMaster.UpdatedBy = phcdto.CreatedBy;
                    userMaster.CreatedOn = DateTime.Now;
                    userMaster.UpdatedOn = DateTime.Now;

                    newCreatedPHC = await this._phcRepository.AddPHCUser(phcMaster, userMaster);

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
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllPHC")]
        [ProducesResponseType(200, Type = typeof(List<PHCMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPHC()
        {
            List<PHCMasterDTO> phcList = new List<PHCMasterDTO>();
            try
            {

                phcList = await _phcRepository.GetAllPHC();
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
                return StatusCode(500, ModelState);
            }
        }

    }
}
