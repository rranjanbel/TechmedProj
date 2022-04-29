using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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


        [HttpGet]
        [Route("LoggedUserInToday")]
        [ProducesResponseType(200, Type = typeof(List<LoggedUserCountVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoggedUserInToday()
        {            
            List<LoggedUserCountVM> loggedUserCounts = new List<LoggedUserCountVM>();
            try
            {
                loggedUserCounts = await _dashBoardRepository.GetTodaysLoggedUsersCount();
                if (loggedUserCounts.Count > 0)
                {
                    return Ok(loggedUserCounts);
                }
                else
                {
                    ModelState.AddModelError("LoggedUserInToday", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LoggedUserInToday", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }



        [HttpGet]
        [Route("GetLoggedUserCount")]
        [ProducesResponseType(200, Type = typeof(LoggedUserCountVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetLoggedUserCount(int userTypeId = 3)
        {
            LoggedUserCountVM loggedUserCountVM = new LoggedUserCountVM();
            try
            {
                if (userTypeId == 0)
                {
                    ModelState.AddModelError("GetLoggedUserCount", $"Please send userTypeId!");
                    return BadRequest(ModelState);
                }
                loggedUserCountVM = _dashBoardRepository.GetLoggedUserTypeCount(userTypeId);
                if (loggedUserCountVM != null)
                {
                    return Ok(loggedUserCountVM);
                }
                else
                {
                    ModelState.AddModelError("GetLoggedUserCount", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetLoggedUserCount", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetTodaysTotalPatientCase")]
        [ProducesResponseType(200, Type = typeof(SpecializationReportVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTodaysTotalPatientCase()
        {
            List<SpecializationReportVM> todaysRegistorCase = new List<SpecializationReportVM>();
            try
            {
                todaysRegistorCase = await _dashBoardRepository.GetTodaysRegistoredPatientList();
                if (todaysRegistorCase != null)
                {
                    return Ok(todaysRegistorCase);
                }
                else
                {
                    ModelState.AddModelError("GetLoggedUserCount", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetLoggedUserCount", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetTodaysPatientQueue")]
        [ProducesResponseType(200, Type = typeof(SpecializationReportVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTodaysPatientQueue()
        {
            List<SpecializationReportVM> todaysRegistorCase = new List<SpecializationReportVM>();
            try
            {
                todaysRegistorCase = await _dashBoardRepository.GetTodaysConsultedPatientList();
                if (todaysRegistorCase != null)
                {
                    return Ok(todaysRegistorCase);
                }
                else
                {
                    ModelState.AddModelError("GetLoggedUserCount", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetLoggedUserCount", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }

        }
    }
}
