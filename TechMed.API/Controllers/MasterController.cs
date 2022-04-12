using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<PHCController> _logger;
       
        public MasterController(IMapper mapper, ISpecializationRepository specializationRepository, ILogger<PHCController> logger)
        {
            this._mapper = mapper;
            //this._phcRepository = phcRepository;
            this._logger = logger;
            this._specializationRepository = specializationRepository;
        }

        [HttpGet]
        [Route("GetAllSpecialization")]     
        [ProducesResponseType(200, Type = typeof(List<SpecializationMaster>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSpecialization()
        {
            try
            {
                var specializations = await _specializationRepository.Get();
                if (specializations != null)
                {                   
                    return Ok(specializations);
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
    }
}
