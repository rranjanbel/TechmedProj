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
    public class DashBoardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDashBoardRepository _dashBoardRepository;
        public DashBoardController(IMapper mapper, TeleMedecineContext teleMedecineContext, IDashBoardRepository dashBoardRepository)
        {
            _dashBoardRepository = dashBoardRepository;
            _mapper = mapper;
        }
        [Route("DoctorsLoggedInToday")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<DoctorDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DoctorsLoggedInToday(DoctorsLoggedInTodayVM doctorsLoggedInTodayVM)
        {
            try
            {
                if (doctorsLoggedInTodayVM == null)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _dashBoardRepository.DoctorsLoggedInToday(doctorsLoggedInTodayVM);
                if (DTO.Count > 0)
                {
                    return Ok(DTO);
                }
                else
                {
                    ModelState.AddModelError("", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }
    }
}
