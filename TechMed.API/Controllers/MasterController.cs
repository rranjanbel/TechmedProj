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
    public class MasterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly ILogger<MasterController> _logger;
       
        public MasterController(IMapper mapper, ISpecializationRepository specializationRepository, ILogger<MasterController> logger)
        {
            this._mapper = mapper;
            //this._phcRepository = phcRepository;
            this._logger = logger;
            this._specializationRepository = specializationRepository;
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
    }
}
