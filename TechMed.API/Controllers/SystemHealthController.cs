using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,SysAdmin,GovEmployee")]
    public class SystemHealthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISystemHealthRepository  _systemHealthRepository;
        private readonly ILogger<DashBoardController> _logger;
        public SystemHealthController(IMapper mapper, TeleMedecineContext teleMedecineContext, ISystemHealthRepository systemHealthRepository, ILogger<DashBoardController> logger)
        {
            _systemHealthRepository = systemHealthRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [Route("GetAPIStatus")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAPIStatus()
        {
            try
            {
                return Ok(true);

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetAPIStatus API " + ex);
                return StatusCode(500, ModelState);
            }
        }
    }
}
