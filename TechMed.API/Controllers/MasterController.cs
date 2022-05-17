using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly ILogger<MasterController> _logger;
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly ICaseFileStatusMasterRpository _CaseFileStatusMasterRpository;

        
        public MasterController(IMapper mapper, ISpecializationRepository specializationRepository, ILogger<MasterController> logger
            , TeleMedecineContext teleMedecineContext
            , ICaseFileStatusMasterRpository caseFileStatusMasterRpository
            )
        {
            this._mapper = mapper;
            //this._phcRepository = phcRepository;
            this._logger = logger;
            this._specializationRepository = specializationRepository;
            this._teleMedecineContext = teleMedecineContext;
            this._CaseFileStatusMasterRpository = caseFileStatusMasterRpository;
        }

        [HttpGet]
        [Route("GetAllSpecialization")]
        [ProducesResponseType(200, Type = typeof(List<SpecializationDTO>))]      
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSpecialization()
        {
            SpecializationDTO mapdata = new SpecializationDTO();
            try
            {
               var spemasters = await _specializationRepository.Get();
              
                var DTOList = new List<SpecializationDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<SpecializationDTO>(item);
                    DTOList.Add(mapdata);
                }               
                if (DTOList != null)
                {                   
                    return Ok(DTOList);
                }
                else
                {
                    ModelState.AddModelError("GetAllSpecialization", "Specialization detail did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllSpecialization", $"Something went wrong when Get all Specialization {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllCaseFileStatusMaster")]
        [ProducesResponseType(200, Type = typeof(List<CaseFileStatusMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCaseFileStatusMaster()
        {
            CaseFileStatusMasterDTO mapdata = new CaseFileStatusMasterDTO();
            try
            {
                var spemasters = await _CaseFileStatusMasterRpository.GetAll();

                var DTOList = new List<CaseFileStatusMasterDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<CaseFileStatusMasterDTO>(item);
                    DTOList.Add(mapdata);
                }
                if (DTOList != null)
                {
                    return Ok(DTOList);
                }
                else
                {
                    ModelState.AddModelError("GetAllCaseFileStatusMaster", "GetAllCaseFileStatusMaster did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllCaseFileStatusMaster", $"Something went wrong when GetAllCaseFileStatusMaster {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllCountryMaster")]
        [ProducesResponseType(200, Type = typeof(List<CountryMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCountryMaster()
        {
            CountryMasterDTO mapdata = new CountryMasterDTO();
            try
            {
                var spemasters = await _teleMedecineContext.CountryMasters.ToListAsync();

                var DTOList = new List<CountryMasterDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<CountryMasterDTO>(item);
                    DTOList.Add(mapdata);
                }
                if (DTOList != null)
                {
                    return Ok(DTOList);
                }
                else
                {
                    ModelState.AddModelError("GetAllCountryMaster", "GetAllCountryMaster did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllCountryMaster", $"Something went wrong when GetAllCountryMaster {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllMaritalStatus")]
        [ProducesResponseType(200, Type = typeof(List<MaritalStatusDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMaritalStatus()
        {
            MaritalStatusDTO mapdata = new MaritalStatusDTO();
            try
            {
                var spemasters = await _teleMedecineContext.MaritalStatuses.ToListAsync();

                var DTOList = new List<MaritalStatusDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<MaritalStatusDTO>(item);
                    DTOList.Add(mapdata);
                }
                if (DTOList != null)
                {
                    return Ok(DTOList);
                }
                else
                {
                    ModelState.AddModelError("GetAllCountryMaster", "GetAllCountryMaster did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllCountryMaster", $"Something went wrong when GetAllCountryMaster {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllDistrictMaster")]
        [ProducesResponseType(200, Type = typeof(List<DistrictMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDistrictMaster()
        {
            DistrictMasterDTO mapdata = new DistrictMasterDTO();
            try
            {
                var spemasters = await _teleMedecineContext.DistrictMasters.ToListAsync();

                var DTOList = new List<DistrictMasterDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<DistrictMasterDTO>(item);
                    DTOList.Add(mapdata);
                }
                if (DTOList != null)
                {
                    return Ok(DTOList);
                }
                else
                {
                    ModelState.AddModelError("GetAllDistrictMaster", "GetAllDistrictMaster did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllDistrictMaster", $"Something went wrong when GetAllDistrictMaster {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllGenderMaster")]
        [ProducesResponseType(200, Type = typeof(List<GenderMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGenderMaster()
        {
            GenderMasterDTO mapdata = new GenderMasterDTO();
            try
            {
                var spemasters = await _teleMedecineContext.GenderMasters.ToListAsync();

                var DTOList = new List<GenderMasterDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<GenderMasterDTO>(item);
                    DTOList.Add(mapdata);
                }
                if (DTOList != null)
                {
                    return Ok(DTOList);
                }
                else
                {
                    ModelState.AddModelError("GetAllGenderMaster", "GetAllGenderMaster did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllGenderMaster", $"Something went wrong when GetAllGenderMaster {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllIDProofTypeMaster")]
        [ProducesResponseType(200, Type = typeof(List<IDProofTypeMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllIDProofTypeMaster()
        {
            IDProofTypeMasterDTO mapdata = new IDProofTypeMasterDTO();
            try
            {
                var spemasters = await _teleMedecineContext.IdproofTypeMasters.ToListAsync();

                var DTOList = new List<IDProofTypeMasterDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<IDProofTypeMasterDTO>(item);
                    DTOList.Add(mapdata);
                }
                if (DTOList != null)
                {
                    return Ok(DTOList);
                }
                else
                {
                    ModelState.AddModelError("GetAllIDProofTypeMaster", "GetAllIDProofTypeMaster did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllIDProofTypeMaster", $"Something went wrong when GetAllIDProofTypeMaster {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllTitleMaster")]
        [ProducesResponseType(200, Type = typeof(List<TitleMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTitleMaster()
        {
            TitleMasterDTO mapdata = new TitleMasterDTO();
            try
            {
                var spemasters = await _teleMedecineContext.TitleMasters.ToListAsync();

                var DTOList = new List<TitleMasterDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<TitleMasterDTO>(item);
                    DTOList.Add(mapdata);
                }
                if (DTOList != null)
                {
                    return Ok(DTOList);
                }
                else
                {
                    ModelState.AddModelError("GetAllTitleMaster", "GetAllTitleMaster did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllTitleMaster", $"Something went wrong when GetAllTitleMaster {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllUserTypeMaster")]
        [ProducesResponseType(200, Type = typeof(List<UserTypeMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUserTypeMaster()
        {
            UserTypeMasterDTO mapdata = new UserTypeMasterDTO();
            try
            {
                var spemasters = await _teleMedecineContext.UserTypeMasters.ToListAsync();

                var DTOList = new List<UserTypeMasterDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<UserTypeMasterDTO>(item);
                    DTOList.Add(mapdata);
                }
                if (DTOList != null)
                {
                    return Ok(DTOList);
                }
                else
                {
                    ModelState.AddModelError("GetAllTitleMaster", "GetAllTitleMaster did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllTitleMaster", $"Something went wrong when GetAllTitleMaster {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllClusterMaster")]
        [ProducesResponseType(200, Type = typeof(List<ClusterMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClusterMaster()
        {
            ClusterMasterDTO mapdata = new ClusterMasterDTO();
            try
            {
                var spemasters = await _teleMedecineContext.ClusterMasters.ToListAsync();

                var DTOList = new List<ClusterMasterDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<ClusterMasterDTO>(item);
                    DTOList.Add(mapdata);
                }
                if (DTOList != null)
                {
                    return Ok(DTOList);
                }
                else
                {
                    ModelState.AddModelError("GetAllClusterMaster", "GetAllClusterMaster did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllClusterMaster", $"Something went wrong when GetAllClusterMaster {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllZoneMaster")]
        [ProducesResponseType(200, Type = typeof(List<ZoneMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBlockMaster()
        {
            ZoneMasterDTO mapdata = new ZoneMasterDTO();
            try
            {
                var spemasters = await _teleMedecineContext.BlockMasters.ToListAsync();

                var DTOList = new List<ZoneMasterDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<ZoneMasterDTO>(item);
                    DTOList.Add(mapdata);
                }
                if (DTOList != null)
                {
                    return Ok(DTOList);
                }
                else
                {
                    ModelState.AddModelError("GetAllZoneMaster", "GetAllZoneMaster did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllZoneMaster", $"Something went wrong when GetAllZoneMaster {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllStateMaster")]
        [ProducesResponseType(200, Type = typeof(List<StateMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStateMaster()
        {
            StateMasterDTO mapdata = new StateMasterDTO();
            try
            {
                var spemasters = await _teleMedecineContext.StateMasters.ToListAsync();

                var DTOList = new List<StateMasterDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<StateMasterDTO>(item);
                    DTOList.Add(mapdata);
                }
                if (DTOList != null)
                {
                    return Ok(DTOList);
                }
                else
                {
                    ModelState.AddModelError("GetAllStateMaster", "GetAllStateMaster did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllStateMaster", $"Something went wrong when GetAllStateMaster {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllPatientStatusMaster")]
        [ProducesResponseType(200, Type = typeof(List<PatientStatusMastersDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPatientStatusMaster()
        {
            PatientStatusMastersDTO mapdata = new PatientStatusMastersDTO();
            try
            {
                var spemasters = await _teleMedecineContext.PatientStatusMasters.ToListAsync();

                var DTOList = new List<PatientStatusMastersDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<PatientStatusMastersDTO>(item);
                    DTOList.Add(mapdata);
                }
                if (DTOList != null)
                {
                    return Ok(DTOList);
                }
                else
                {
                    ModelState.AddModelError("GetAllStateMaster", "GetAllStateMaster did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllStateMaster", $"Something went wrong when GetAllStateMaster {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        ////CaseFileStatusMaster
        ////CountryMaster
        ////DistrictMaster
        ////GenderMaster
        ////IDProofTypeMaster
        ////TitleMaster
        ////UserTypeMaster
        ////ClusterMaster
        ////ZoneMaster
        ////StateMaster
    }
}
