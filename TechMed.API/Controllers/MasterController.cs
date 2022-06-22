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
        private readonly IDigonisisRepository _digonisisRepository;
        private readonly IDrugsRepository _drugsRepository;
        private readonly IMasterRepository _masterRepository;


        public MasterController(IMapper mapper, ISpecializationRepository specializationRepository, ILogger<MasterController> logger
            , TeleMedecineContext teleMedecineContext
            , ICaseFileStatusMasterRpository caseFileStatusMasterRpository,
            IDigonisisRepository digonisisRepository,
            IDrugsRepository drugsRepository,
            IMasterRepository masterRepository

            )
        {
            this._mapper = mapper;
            //this._phcRepository = phcRepository;
            this._logger = logger;
            this._specializationRepository = specializationRepository;
            this._teleMedecineContext = teleMedecineContext;
            this._CaseFileStatusMasterRpository = caseFileStatusMasterRpository;
            this._digonisisRepository = digonisisRepository;
            this._drugsRepository = drugsRepository;
            this._masterRepository = masterRepository;
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
                _logger.LogError("Exception in GetAllSpecialization API " + ex);
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
                _logger.LogError("Exception in GetAllCaseFileStatusMaster API " + ex);
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
                _logger.LogError("Exception in GetAllCountryMaster API " + ex);
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
                _logger.LogError("Exception in GetAllMaritalStatus API " + ex);
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
                _logger.LogError("Exception in GetAllDistrictMaster API " + ex);
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
                _logger.LogError("Exception in GetAllGenderMaster API " + ex);
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
                var spemasters = await _teleMedecineContext.IdproofTypeMasters.Where(a => a.IsActive ==true).ToListAsync();

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
                _logger.LogError("Exception in GetAllIDProofTypeMaster API " + ex);
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
                _logger.LogError("Exception in GetAllTitleMaster API " + ex);
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
                _logger.LogError("Exception in GetAllUserTypeMaster API " + ex);
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
                _logger.LogError("Exception in GetAllUserTypeMaster API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllBlockMaster")]
        [ProducesResponseType(200, Type = typeof(List<BlockMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBlockMaster()
        {
            BlockMasterDTO mapdata = new BlockMasterDTO();
            try
            {
                var spemasters = await _teleMedecineContext.BlockMasters.ToListAsync();

                var DTOList = new List<BlockMasterDTO>();
                foreach (var item in spemasters)
                {
                    mapdata = _mapper.Map<BlockMasterDTO>(item);
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
                _logger.LogError("Exception in GetAllBlockMaster API " + ex);
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
                _logger.LogError("Exception in GetAllStateMaster API " + ex);
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
                _logger.LogError("Exception in GetAllPatientStatusMaster API " + ex);
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
            List<PHCMasterDTO> phcs = new List<PHCMasterDTO>();
            PHCMasterDTO phc;
            try
            {
                var phclist = await _teleMedecineContext.Phcmasters.ToListAsync();               

                foreach (var item in phclist)
                {
                    phc = new PHCMasterDTO();
                    phc.ID = item.Id;
                    phc.PHCName = item.Phcname;
                    phcs.Add(phc);
                }
                if (phcs != null)
                {
                    return Ok(phcs);
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
                _logger.LogError("Exception in GetAllSpecialization API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetDiagnosticTest")]
        [ProducesResponseType(200, Type = typeof(List<DiagnosticTestMaster>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDiagnosticTest()
        {
            List<DiagnosticTestMaster> diagnosticTestList = new List<DiagnosticTestMaster>();
            try
            {
                diagnosticTestList = await this._digonisisRepository.GetAllDignosis();
                
                if (diagnosticTestList != null)
                {
                    return Ok(diagnosticTestList);
                }
                else
                {
                    ModelState.AddModelError("GetDiagnosticTest", "Diagnostic test detail did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDiagnosticTest", $"Something went wrong when Get all Diagnostic {ex.Message}");
                _logger.LogError("Exception in GetDiagnosticTest API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllDrugsName")]
        [ProducesResponseType(200, Type = typeof(List<DrugsMasterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDrugsName()
        {
            List<DrugsMasterDTO> dugsList = new List<DrugsMasterDTO>();
            try
            {
                dugsList = await this._drugsRepository.GetAllDrugs();

                if (dugsList != null)
                {
                    return Ok(dugsList);
                }
                else
                {
                    ModelState.AddModelError("GetAllDrugsName", "Diagnostic test detail did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllDrugsName", $"Something went wrong when get all Diagnostic {ex.Message}");
                _logger.LogError("Exception in GetAllDrugsName API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllDivision")]
        [ProducesResponseType(200, Type = typeof(List<DivisionDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDivision()
        {
            List<DivisionDTO> divisionList = new List<DivisionDTO>();
            try
            {
                divisionList = await this._masterRepository.GetAllDivision();

                if (divisionList != null)
                {
                    return Ok(divisionList);
                }
                else
                {
                    ModelState.AddModelError("GetAllDivision", "Diagnostic test detail did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllDivision", $"Something went wrong when get all Diagnostic {ex.Message}");
                _logger.LogError("Exception in GetAllDivision API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetDivisionsByClusterID")]
        [ProducesResponseType(200, Type = typeof(List<DivisionDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDivisionsByClusterID(int clusterId)
        {
            List<DivisionDTO> divisionList = new List<DivisionDTO>();
            try
            {
                divisionList = await this._masterRepository.GetDivisionByClusterID(clusterId);

                if (divisionList != null)
                {
                    return Ok(divisionList);
                }
                else
                {
                    ModelState.AddModelError("GetAllDivision", "Diagnostic test detail did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetAllDivision", $"Something went wrong when get all Diagnostic {ex.Message}");
                _logger.LogError("Exception in GetAllDivision API " + ex);
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
