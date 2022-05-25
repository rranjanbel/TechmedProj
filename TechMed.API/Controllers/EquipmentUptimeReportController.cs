using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;
using TechMed.BL.ViewModels;
using TechMed.BL.CommanClassesAndFunctions;
using AutoMapper;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EquipmentUptimeReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEquipmentUptimeReport _equipmentUptime;
        private readonly ILogger<EquipmentUptimeReportController> _logger;
        public EquipmentUptimeReportController(IMapper mapper, IEquipmentUptimeReport equipmentUptime, ILogger<EquipmentUptimeReportController> logger)
        {
            this._mapper = mapper;
            this._equipmentUptime = equipmentUptime;
            this._logger = logger;
        }

        [HttpPost]
        [Route("AddEquipmentUptimeReport")]
        [ProducesResponseType(201, Type = typeof(EquipmentUptimeReportDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEquipmentUptimeReport([FromBody] EquipmentUptimeReportDTO equipmentUptime)
        {

            try
            {
                EquipmentUptimeReportDTO equipmentUptimedto = new EquipmentUptimeReportDTO();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    if (equipmentUptime != null)
                    {
                        equipmentUptimedto = await _equipmentUptime.AddEquipmentUptimeReport(equipmentUptime);

                        if (equipmentUptimedto != null)
                        {
                            if (equipmentUptimedto.Id > 0)
                            {
                                return CreatedAtRoute(201, equipmentUptimedto);
                            }
                            else
                            {
                                ModelState.AddModelError("AddEquipmentUptimeReport", $"Something went wrong when add equipment Uptime ");
                                return StatusCode(404, ModelState);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("AddEquipmentUptimeReport", $"Something went wrong when add equipment Uptime");
                            return StatusCode(404, ModelState);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("AddEquipmentUptimeReport", $"Something went wrong when add equipment Uptime");
                        return StatusCode(404, ModelState);
                    }
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("AddEquipmentUptimeReport", $"Something went wrong when Add equipment Uptime {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

    }
}
