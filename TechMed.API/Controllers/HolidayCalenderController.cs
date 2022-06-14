using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.DTOMaster;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HolidayCalenderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHolidayRepository _holidayRepository;
        private readonly ILogger<HolidayCalenderController> _logger;      
        public HolidayCalenderController(IMapper mapper, IHolidayRepository holidayRepository, ILogger<HolidayCalenderController> logger)
        {
            this._mapper = mapper;
            this._holidayRepository = holidayRepository;
            this._logger = logger;          
        }

        [HttpPost]
        [Route("AddHoliday")]       
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddHoliday(HolidayDTO holidayDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                bool IsholidayAdded = await _holidayRepository.CreateHoliday(holidayDTO);
                if (IsholidayAdded)
                {
                    return Ok(new { Status ="Holiday successfully added" });
                }
                else
                {
                    ModelState.AddModelError("AddHoliday", $"Holiday did not add {holidayDTO.HolidayName}");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("AddHoliday", $"Something went wrong when GetHolidayList {ex.Message}");
                _logger.LogError("Exception in AddHoliday API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("GetHolidayList")]
        //[HttpGet("{id:int}", Name = "GetPHCDetailsByID")]
        [ProducesResponseType(200, Type = typeof(List<HolidayDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHolidayList(int year)
        {
            try
            {
                var holidayList = await _holidayRepository.GetHolidayList(year);
                if (holidayList != null)
                {                   
                    return Ok(holidayList);
                }
                else
                {
                    ModelState.AddModelError("GetHolidayList", $"Holiday list for selected year did not find {year}");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetHolidayList", $"Something went wrong when GetHolidayList {ex.Message}");
                _logger.LogError("Exception in GetHolidayList API " + ex);
                return StatusCode(500, ModelState);
            }
        }
    }
}
