using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MISController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMISRepository _mISRepository;
        public MISController(IMapper mapper, TeleMedecineContext teleMedecineContext, IMISRepository mISRepository)
        {
            _mISRepository = mISRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("CompletedConsultation")]
        [ProducesResponseType(200, Type = typeof(List<CompletedConsultationDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CompletedConsultation(CompletedConsultationVM completedConsultationVM)
        {
            List<CompletedConsultationDTO> mapdata = new List<CompletedConsultationDTO>();
            try
            {
                mapdata =await  _mISRepository.CompletedConsultation(completedConsultationVM);
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
                return StatusCode(500, ModelState);
            }
        }
    }
}
