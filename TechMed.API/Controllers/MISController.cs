using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,SysAdmin,GovEmployee")]
    public class MISController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMISRepository _mISRepository;
        private readonly ILogger<MISController> _logger;
        public MISController(IMapper mapper, TeleMedecineContext teleMedecineContext, IMISRepository mISRepository, ILogger<MISController> logger)
        {
            _mISRepository = mISRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpPost]
        [Route("CompletedConsultation")]
        [ProducesResponseType(200, Type = typeof(List<CompletedConsultantVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CompletedConsultation(CompletedPatientSearchVM completedConsultation)
        {
            List<CompletedConsultantVM> mapdata = new List<CompletedConsultantVM>();
            try
            {
                mapdata = await  _mISRepository.CompletedConsultation(completedConsultation);
                if (mapdata.Count >0)
                {
                    return Ok(mapdata);
                }
                else
                {
                    ModelState.AddModelError("CompletedConsultation", "CompletedConsultation did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CompletedConsultation", $"Something went wrong when CompletedConsultation {ex.Message}");
                _logger.LogError("Exception in CompletedConsultation API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        [Route("CompletedConsultationByDoctors")]
        [ProducesResponseType(200, Type = typeof(List<ConsultedPatientByDoctorAndPHCVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CompletedConsultationByDoctors(SearchDateVM completedConsultation)
        {
            List<ConsultedPatientByDoctorAndPHCVM> mapdata = new List<ConsultedPatientByDoctorAndPHCVM>();
            try
            {
                mapdata = _mISRepository.CompletedConsultationByDoctor(completedConsultation);
                if (mapdata.Count > 0)
                {
                    return Ok(mapdata);
                }
                else
                {
                    ModelState.AddModelError("CompletedConsultationByDoctors", "CompletedConsultation did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CompletedConsultationByDoctors", $"Something went wrong when CompletedConsultation {ex.Message}");
                _logger.LogError("Exception in CompletedConsultationByDoctors API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetCompletedConsultationChart")]
        [ProducesResponseType(200, Type = typeof(List<CompletedConsultationChartVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCompletedConsultationChart(int year)
        {
            List<CompletedConsultationChartVM> mapdata = new List<CompletedConsultationChartVM>();
            try
            {
                mapdata = _mISRepository.CompletedConsultationChart(year);
                if (mapdata.Count > 0)
                {
                    return Ok(mapdata);
                }
                else
                {
                    ModelState.AddModelError("CompletedConsultationByDoctors", "CompletedConsultation did not find");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CompletedConsultationByDoctors", $"Something went wrong when CompletedConsultation {ex.Message}");
                _logger.LogError("Exception in CompletedConsultationByDoctors API " + ex);
                return StatusCode(500, ModelState);
            }
        }
    }
}
