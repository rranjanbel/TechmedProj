using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class VideoCallTransactionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVideoCallTransactionRespository _videoCallTransactionRespository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<VideoCallTransactionController> _logger;
        public VideoCallTransactionController(IMapper mapper, ILogger<VideoCallTransactionController> logger, TeleMedecineContext teleMedecineContext, IVideoCallTransactionRespository videoCallTransactionRespository, IWebHostEnvironment webHostEnvironment)
        {
            _videoCallTransactionRespository = videoCallTransactionRespository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        [Route("GetVideoCallTransactionByUserID")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<GetVideoCallTransactionByUserIDDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVideoCallTransactionByUserID(int UserID)
        {
            try
            {
                if (UserID < 1)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _videoCallTransactionRespository.GetVideoCallTransactionByUserID(UserID);
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
                _logger.LogError("Exception in GetVideoCallTransactionByUserID API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [Route("PostVideoCallTransaction")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(VideoCallTransactionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostVideoCallTransaction(VideoCallTransactionDTO videoCallTransactionDTO)
        {
            try
            {
                if (videoCallTransactionDTO==null)
                {
                    return BadRequest(ModelState);
                }
                VideoCallTransactionDTO DTO = await _videoCallTransactionRespository.PostVideoCallTransaction(videoCallTransactionDTO);
                if (DTO.Id>0)
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
                _logger.LogError("Exception in PostVideoCallTransaction API " + ex);
                return StatusCode(500, ModelState);
            }
        }
    }
}
