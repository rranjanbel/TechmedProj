using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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

        [HttpPost]
        [Route("GetDashboardConsultation")]
        [ProducesResponseType(200, Type = typeof(List<DashboardConsultationVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardConsultation(GetDashboardConsultationVM getDashboardConsultationVM)
        {
            try
            {
                if (getDashboardConsultationVM == null)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _dashBoardRepository.GetDashboardConsultation(getDashboardConsultationVM);
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
        [Route("GetPHCLoginHistoryReport")]
        [ProducesResponseType(200, Type = typeof(List<PHCLoginHistoryReportVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPHCLoginHistoryReport(int PHCId, DateTime? fromDate, DateTime? toDate)
        {
            List<PHCLoginHistoryReportVM> loginHistoryPHC = new List<PHCLoginHistoryReportVM>();
            DateTime? fromDateUtc = null;
            if (fromDate != null)
                fromDateUtc = fromDate.Value;
            DateTime? toDateUtc = null;
            if (toDate != null)
                toDateUtc = toDate.Value;
            try
            {
                loginHistoryPHC = _dashBoardRepository.GetPHCLoginHistoryReport(PHCId, fromDateUtc, toDateUtc);
                if (loginHistoryPHC != null)
                {
                    return Ok(loginHistoryPHC);
                }
                else
                {
                    ModelState.AddModelError("GetPHCLoginHistoryReport", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetPHCLoginHistoryReport", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetPHCConsultationReport")]
        [ProducesResponseType(200, Type = typeof(List<PHCConsultationVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPHCConsultationReport(int PHCId, DateTime? fromDate =null, DateTime? toDate =null)
        {
            List<PHCConsultationVM> phcConsultation = new List<PHCConsultationVM>();
            DateTime? fromDateUtc = null;
            if(fromDate != null)
                fromDateUtc = fromDate.Value;
            DateTime? toDateUtc = null;
            if (toDateUtc != null)
                toDateUtc = toDateUtc.Value;
            try
            {
                phcConsultation = _dashBoardRepository.GetPHCConsultationReport(PHCId, fromDateUtc, toDateUtc);
                if (phcConsultation != null)
                {
                    return Ok(phcConsultation);
                }
                else
                {
                    ModelState.AddModelError("GetPHCConsultationReport", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetPHCConsultationReport", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }

        }

        [HttpPost]
        [Route("GetDashboardReportSummary")]
        [ProducesResponseType(200, Type = typeof(List<DashboardReportSummaryVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardReportSummary(GetDashboardReportSummaryVM getDashboardReportSummaryVM)
        {
            try
            {
                if (getDashboardReportSummaryVM == null)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _dashBoardRepository.GetDashboardReportSummary(getDashboardReportSummaryVM);
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

      
        [HttpPost]
        [Route("GetDashboardReportSummaryMonthly")]
        [ProducesResponseType(200, Type = typeof(List<DashboardReportSummaryVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardReportSummaryMonthly(GetDashboardReportSummaryMonthVM getDashboardReportSummaryVM)
        {
            try
            {
                if (getDashboardReportSummaryVM == null)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _dashBoardRepository.GetDashboardReportSummaryMonthly(getDashboardReportSummaryVM);
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

        [HttpPost]
        [Route("GetDashboardReportConsultation")]
        [ProducesResponseType(200, Type = typeof(List<DashboardReportConsultationVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardReportConsultation(GetDashboardReportConsultationVM dashboardReportConsultationVM)
        {
            try
            {
                if (dashboardReportConsultationVM == null)
                {
                    return BadRequest(ModelState);
                }
                var DTO = await _dashBoardRepository.GetDashboardReportConsultation(dashboardReportConsultationVM);
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
        [Route("GetPHCManpowerReport")]
        [ProducesResponseType(200, Type = typeof(PHCMainpowerResultSetVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPHCManpowerReport(int year, int month)
        {
            PHCMainpowerResultSetVM phcmanpowerReport = new PHCMainpowerResultSetVM();


            try
            {
                if (year > 0 && month > 0)
                {
                    phcmanpowerReport = _dashBoardRepository.GetPHCManpowerReport(year,month);
                    if (phcmanpowerReport != null)
                    {
                        return Ok(phcmanpowerReport);
                    }
                    else
                    {
                        ModelState.AddModelError("GetPHCManpowerReport", $"Data not found!");
                        return StatusCode(404, ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError("GetPHCManpowerReport", $"year and month should not null.");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetPHCManpowerReport", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetPatientRegisterReport")]
        [ProducesResponseType(200, Type = typeof(List<RegisterPatientVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPatientRegisterReport(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<RegisterPatientVM> patientResiter = new List<RegisterPatientVM>();
            DateTime? fromDateUtc = null;
            if (fromDate != null)
                fromDateUtc = fromDate.Value;
            DateTime? toDateUtc = null;
            if (toDate != null)
                toDateUtc = toDate.Value;
            try
            {
                patientResiter = _dashBoardRepository.GetRegisterPatientReport(fromDateUtc, toDateUtc);
                if (patientResiter != null)
                {
                    return Ok(patientResiter);
                }
                else
                {
                    ModelState.AddModelError("GetPatientRegisterReport", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetPatientRegisterReport", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }

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
                        equipmentUptimedto = await _dashBoardRepository.AddEquipmentUptimeReport(equipmentUptime);

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


        [HttpGet]
        [Route("GetReferredPatientReport")]
        [ProducesResponseType(200, Type = typeof(List<GetReferredPatientVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetReferredPatientReport(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<GetReferredPatientVM> patientResiter = new List<GetReferredPatientVM>();
            DateTime? fromDateUtc = null;
            if (fromDate != null)
                fromDateUtc = fromDate.Value;
            DateTime? toDateUtc = null;
            if (toDate != null)
                toDateUtc = toDate.Value;
            try
            {
                patientResiter = _dashBoardRepository.GetReferredPatientReport(fromDateUtc, toDateUtc);
                if (patientResiter != null)
                {
                    return Ok(patientResiter);
                }
                else
                {
                    ModelState.AddModelError("GetReferredPatientReport", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetReferredPatientReport", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetReviewPatientReport")]
        [ProducesResponseType(200, Type = typeof(List<GetReviewPatientVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetReviewPatientReport(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<GetReviewPatientVM> patientResiter = new List<GetReviewPatientVM>();
            DateTime? fromDateUtc = null;
            if (fromDate != null)
                fromDateUtc = fromDate.Value;
            DateTime? toDateUtc = null;
            if (toDate != null)
                toDateUtc = toDate.Value;
            try
            {
                patientResiter = _dashBoardRepository.GetReviewPatientReport(fromDateUtc, toDateUtc);
                if (patientResiter != null)
                {
                    return Ok(patientResiter);
                }
                else
                {
                    ModelState.AddModelError("GetReviewPatientReport", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetReviewPatientReport", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }

        }



        [HttpGet]
        [Route("GetDashboardSpokeMaintenance")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardSpokeMaintenanceVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDashboardSpokeMaintenance(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<GetDashboardSpokeMaintenanceVM> patientResiter = new List<GetDashboardSpokeMaintenanceVM>();
            DateTime? fromDateUtc = null;
            if (fromDate != null)
                fromDateUtc = fromDate.Value;
            DateTime? toDateUtc = null;
            if (toDate != null)
                toDateUtc = toDate.Value;
            try
            {
                patientResiter = _dashBoardRepository.GetDashboardSpokeMaintenance(fromDateUtc, toDateUtc);
                if (patientResiter != null)
                {
                    return Ok(patientResiter);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardSpokeMaintenance", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardSpokeMaintenance", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetDashboardEmployeeFeedback")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardEmployeeFeedbackVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDashboardEmployeeFeedback(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<GetDashboardEmployeeFeedbackVM> patientResiter = new List<GetDashboardEmployeeFeedbackVM>();
            DateTime? fromDateUtc = null;
            if (fromDate != null)
                fromDateUtc = fromDate.Value;
            DateTime? toDateUtc = null;
            if (toDate != null)
                toDateUtc = toDate.Value;
            try
            {
                patientResiter = _dashBoardRepository.GetDashboardEmployeeFeedback(fromDateUtc, toDateUtc);
                if (patientResiter != null)
                {
                    return Ok(patientResiter);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardEmployeeFeedback", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardEmployeeFeedback", $"Something went wrong {ex.Message}");
                return StatusCode(500, ModelState);
            }

        }

    }
}
