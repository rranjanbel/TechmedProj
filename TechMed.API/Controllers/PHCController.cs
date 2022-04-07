using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [ProducesResponseType(200, Type = typeof(List<PHCHospitalDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPHCDetailsByID(int Id)
        {
            try
            {               
                var phcmaster = await _phcRepository.GetByID(Id);              
                if (phcmaster != null)
                {                    
                    var phcMasterDTO = _mapper.Map<PHCHospitalDTO>(phcmaster);               
                    return Ok(phcMasterDTO);
                }
                else
                {
                    ModelState.AddModelError("GetPHCDetailsByID", "User list not found");
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
        [Route("GetPHCDetailsByUserID")]
        [ProducesResponseType(200, Type = typeof(List<PHCHospitalDTO>))]
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
                    ModelState.AddModelError("GetPHCDetailsByUserID", "User list not found");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetPHCDetailsByUserID", $"Something went wrong when GetPHCDetails {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }
    }
}
