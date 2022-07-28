using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.ViewModel;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CDSSGuideline : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICDSSGuidelineRepository _cDSSGuidelineRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<DoctorController> _logger;
        public CDSSGuideline(IMapper mapper, ILogger<DoctorController> logger, ICDSSGuidelineRepository cDSSGuidelineRepository, IWebHostEnvironment webHostEnvironment)
        {
            _cDSSGuidelineRepository = cDSSGuidelineRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        [Route("GetCDSSGuideLines")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(CDSSGuidelineVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCDSSGuideLines()
        {
            try
            {
                var DTO = await _cDSSGuidelineRepository.GetAllCDSSGuideline();
                if (DTO != null)
                {
                    _logger.LogInformation($"CDSSGuideline : Sucess response returned ");
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    _logger.LogError("GetCDSSGuideLines :  Data not found ");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetCDSSGuideLines API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [Route("GetCDSSGuideLinesByDiseases")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(CDSSGuidelineVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCDSSGuideLinesByDiseases(string Diseases )
        {
            try
            {
                var DTO = await _cDSSGuidelineRepository.GetAllCDSSGuidelineByDiseases(Diseases);
                if (DTO != null)
                {
                    _logger.LogInformation($"CDSSGuideline : Sucess response returned ");
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    _logger.LogError("GetCDSSGuideLines :  Data not found ");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetCDSSGuideLines API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [Route("GetCDSSGuideLinesByDiseasesAndAge")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(CDSSGuidelineVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCDSSGuideLinesByDiseasesAndAge(string Diseases ,int Age)
        {
            try
            {
                var DTO = await _cDSSGuidelineRepository.GetCDSSGuideLinesByDiseasesAndAge(Diseases,Age);
                if (DTO != null)
                {
                    _logger.LogInformation($"CDSSGuideline : Sucess response returned ");
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    _logger.LogError("GetCDSSGuideLines :  Data not found ");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetCDSSGuideLines API " + ex);
                return StatusCode(500, ModelState);
            }
        }
        [Route("GetCDSSGuidelineDiseasesByDiseasesAndAge")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(CDSSGuidelineDiseasesVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCDSSGuidelineDiseasesByDiseasesAndAge(string Diseases, int Age)
        {
            try
            {
                var DTO = await _cDSSGuidelineRepository.GetCDSSGuideLinesDiseasesByDiseasesAndAge(Diseases, Age);
                if (DTO != null)
                {
                    _logger.LogInformation($"GetCDSSGuidelineDiseasesByDiseasesAndAge : Sucess response returned ");
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    _logger.LogError("GetCDSSGuidelineDiseasesByDiseasesAndAge :  Data not found ");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetCDSSGuideLines API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [Route("GetAllCDSSGuidelineByID")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(CDSSGuidelineVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCDSSGuidelineByID(Int64 ID)
        {
            try
            {
                var DTO = await _cDSSGuidelineRepository.GetAllCDSSGuidelineByID(ID);
                if (DTO != null)
                {
                    _logger.LogInformation($"GetAllCDSSGuidelineByID : Sucess response returned ");
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    _logger.LogError("GetAllCDSSGuidelineByID :  Data not found ");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetAllCDSSGuidelineByID API " + ex);
                return StatusCode(500, ModelState);
            }
        }
    }
}
