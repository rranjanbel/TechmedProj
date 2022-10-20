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
    [Authorize]
    public class DashBoardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDashBoardRepository _dashBoardRepository;
        private readonly ILogger<DashBoardController> _logger;
        public DashBoardController(IMapper mapper, TeleMedecineContext teleMedecineContext, IDashBoardRepository dashBoardRepository, ILogger<DashBoardController> logger)
        {
            _dashBoardRepository = dashBoardRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [Route("DoctorsLoggedInToday")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<DoctorDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DoctorsLoggedInToday(DoctorsLoggedInTodayVM doctorsLoggedInTodayVM)
        {
            List<DoctorDTO> doctors = new List<DoctorDTO>();
            try
            {
                if (doctorsLoggedInTodayVM == null)
                {
                    return BadRequest(ModelState);
                }
                doctors = await _dashBoardRepository.DoctorsLoggedInToday(doctorsLoggedInTodayVM);
                return Ok(doctors);               
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in Doctors Logged In Today API " + ex);
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
                return Ok(loggedUserCounts);               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LoggedUserInToday", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in LoggedUserInToday API " + ex);
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
                return Ok(loggedUserCountVM);
              
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetLoggedUserCount", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetLoggedUserCount API " + ex);
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
                return Ok(todaysRegistorCase);               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetLoggedUserCount", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetTodaysTotalPatientCase API " + ex);
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
                return Ok(todaysRegistorCase);
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetLoggedUserCount", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetTodaysPatientQueue API " + ex);
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
            List<DashboardConsultationVM> todaysRegistorCase = new List<DashboardConsultationVM>();
            try
            {
                if (getDashboardConsultationVM == null)
                {
                    return BadRequest(ModelState);
                }
                todaysRegistorCase = await _dashBoardRepository.GetDashboardConsultation(getDashboardConsultationVM);
                return Ok(todaysRegistorCase);                
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardConsultation API " + ex);
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
            //DateTime? fromDateUtc = null;
            //if (fromDate != null)
            //    fromDateUtc = fromDate.Value;
            //DateTime? toDateUtc = null;
            //if (toDate != null)
            //    toDateUtc = toDate.Value;
            try
            {
                loginHistoryPHC = _dashBoardRepository.GetPHCLoginHistoryReport(PHCId, fromDate, toDate);
                return Ok(loginHistoryPHC);              
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetPHCLoginHistoryReport", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetPHCLoginHistoryReport API " + ex);
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
            try
            {
                phcConsultation = _dashBoardRepository.GetPHCConsultationReport(PHCId, fromDate, toDate);
                return Ok(phcConsultation);                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetPHCConsultationReport", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetPHCConsultationReport API " + ex);
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
            List<DashboardReportSummaryVM> reportSummaryVMs = new List<DashboardReportSummaryVM>();
            try
            {
                if (getDashboardReportSummaryVM == null)
                {
                    return BadRequest(ModelState);
                }
                reportSummaryVMs = await _dashBoardRepository.GetDashboardReportSummary(getDashboardReportSummaryVM);
                return Ok(reportSummaryVMs);
              
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardReportSummary API " + ex);
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
            List<DashboardReportSummaryVM> reportSummaryVMs = new List<DashboardReportSummaryVM>();
            try
            {
                if (getDashboardReportSummaryVM == null)
                {
                    return BadRequest(ModelState);
                }
                reportSummaryVMs = await _dashBoardRepository.GetDashboardReportSummaryMonthly(getDashboardReportSummaryVM);
                return Ok(reportSummaryVMs);               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardReportSummaryMonthly API " + ex);
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
                _logger.LogError("Exception in GetDashboardReportConsultation API " + ex);
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
                _logger.LogError("Exception in GetPHCManpowerReport API " + ex);
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
            //DateTime? fromDateUtc = null;
            //if (fromDate != null)
            //    fromDateUtc = fromDate.Value;
            //DateTime? toDateUtc = null;
            //if (toDate != null)
            //    toDateUtc = toDate.Value;
            try
            {
                patientResiter = _dashBoardRepository.GetRegisterPatientReport(fromDate, toDate);
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
                _logger.LogError("Exception in GetPatientRegisterReport API " + ex);
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
                _logger.LogError("Exception in AddEquipmentUptimeReport API " + ex);
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
            //DateTime? fromDateUtc = null;
            //if (fromDate != null)
            //    fromDateUtc = fromDate.Value;
            //DateTime? toDateUtc = null;
            //if (toDate != null)
            //    toDateUtc = toDate.Value;
            try
            {
                patientResiter = _dashBoardRepository.GetReferredPatientReport(fromDate, toDate);
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
                _logger.LogError("Exception in GetReferredPatientReport API " + ex);
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
            //DateTime? fromDateUtc = null;
            //if (fromDate != null)
            //    fromDateUtc = fromDate.Value;
            //DateTime? toDateUtc = null;
            //if (toDate != null)
            //    toDateUtc = toDate.Value;
            try
            {
                patientResiter = _dashBoardRepository.GetReviewPatientReport(fromDate, toDate);
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
                _logger.LogError("Exception in GetReviewPatientReport API " + ex);
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
                _logger.LogError("Exception in GetDashboardSpokeMaintenance API " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetDashboardEmployeeFeedback")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardEmployeeFeedbackVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDashboardEmployeeFeedback(int? Fromyear, string qtr)
        {
            List<GetDashboardEmployeeFeedbackVM> patientResiter = new List<GetDashboardEmployeeFeedbackVM>();
            //DateTime? fromDateUtc = null;
            //if (fromDate != null)
            //    fromDateUtc = fromDate.Value;
            //DateTime? toDateUtc = null;
            //if (toDate != null)
            //    toDateUtc = toDate.Value;
            try
            {
                patientResiter = _dashBoardRepository.GetDashboardEmployeeFeedback(Fromyear, qtr);
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
                _logger.LogError("Exception in GetDashboardEmployeeFeedback API " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetDashboardEquipmentUptimeReport")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardEquipmentUptimeReportVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDashboardEquipmentUptimeReport(int month , int year)
        {
            List<GetDashboardEquipmentUptimeReportVM> patientResiter = new List<GetDashboardEquipmentUptimeReportVM>();
            try
            {
                patientResiter = _dashBoardRepository.GetDashboardEquipmentUptimeReport(month, year);
                if (patientResiter != null)
                {
                    return Ok(patientResiter);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardEquipmentUptimeReport", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardEquipmentUptimeReport", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardEquipmentUptimeReport API " + ex);
                return StatusCode(500, ModelState);
            }

        }
       
        [HttpGet]
        [Route("GetDashboardAppointment")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardAppointmentVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDashboardAppointment(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<GetDashboardAppointmentVM> patientResiter = new List<GetDashboardAppointmentVM>();
            //DateTime? fromDateUtc = null;
            //if (fromDate != null)
            //    fromDateUtc = fromDate.Value;
            //DateTime? toDateUtc = null;
            //if (toDate != null)
            //    toDateUtc = toDate.Value;
            try
            {
                patientResiter = _dashBoardRepository.GetDashboardAppointment(fromDate, toDate);
                if (patientResiter != null)
                {
                    return Ok(patientResiter);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardAppointmentVM", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardAppointmentVM", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardAppointment API " + ex);
                return StatusCode(500, ModelState);
            }

        }




        [HttpGet]
        [Route("GetDashboardDoctorAvgTime")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardDoctorAvgTimeVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDashboardDoctorAvgTime(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<GetDashboardDoctorAvgTimeVM> patientResiter = new List<GetDashboardDoctorAvgTimeVM>();
            //DateTime? fromDateUtc = null;
            //if (fromDate != null)
            //    fromDateUtc = fromDate.Value;
            //DateTime? toDateUtc = null;
            //if (toDate != null)
            //    toDateUtc = toDate.Value;
            try
            {
                patientResiter = _dashBoardRepository.GetDashboardDoctorAvgTime(fromDate, toDate);
                if (patientResiter != null)
                {
                    return Ok(patientResiter);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardDoctorAvgTime", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardDoctorAvgTime", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardDoctorAvgTime API " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetDashboardDoctorAvailability")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardDoctorAvailabilityVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDashboardDoctorAvailability(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<GetDashboardDoctorAvailabilityVM> patientResiter = new List<GetDashboardDoctorAvailabilityVM>();
            //DateTime? fromDateUtc = null;
            //if (fromDate != null)
            //    fromDateUtc = fromDate.Value;
            //DateTime? toDateUtc = null;
            //if (toDate != null)
            //    toDateUtc = toDate.Value;
            try
            {
                patientResiter = _dashBoardRepository.GetDashboardDoctorAvailability(fromDate, toDate);
                if (patientResiter != null)
                {
                    return Ok(patientResiter);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardDoctorAvailability", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardDoctorAvailability", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardDoctorAvailability API " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetDashboardEquipmentHeaderReport")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardEquipmentHeaderReportVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDashboardEquipmentHeaderReport(int month, int year)
        {
            List<GetDashboardEquipmentHeaderReportVM> patientResiter = new List<GetDashboardEquipmentHeaderReportVM>();
           
            try
            {
                patientResiter = _dashBoardRepository.GetDashboardEquipmentHeaderReport( month, year);
                if (patientResiter != null)
                {
                    return Ok(patientResiter);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardEquipmentHeaderReport", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardEquipmentHeaderReport", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardEquipmentHeaderReport API " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetPrescribedMedicine")]
        [ProducesResponseType(200, Type = typeof(List<PrescribedMedicineVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPrescribedMedicine(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<PrescribedMedicineVM> prescribedMedicines = new List<PrescribedMedicineVM>();
            DateTime? fromDateUtc = null;
            if (fromDate != null)
                fromDateUtc = fromDate.Value;
            DateTime? toDateUtc = null;
            if (toDate != null)
                toDateUtc = toDate.Value;
            try
            {
                prescribedMedicines = await _dashBoardRepository.GetPrescribedMedicineList(fromDateUtc, toDateUtc);
                if (prescribedMedicines != null)
                {
                    return Ok(prescribedMedicines);
                }
                else
                {
                    ModelState.AddModelError("GetPrescribedMedicine", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetPrescribedMedicine", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetPrescribedMedicine API " + ex);
                return StatusCode(500, ModelState);
            }

        }


        [HttpGet]
        [Route("GetDashboardDiagnosticPrescribedTestWise")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardDiagnosticPrescribedTestWiseVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardDiagnosticPrescribedTestWise(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<GetDashboardDiagnosticPrescribedTestWiseVM> prescribedMedicines = new List<GetDashboardDiagnosticPrescribedTestWiseVM>();
            //DateTime? fromDateUtc = null;
            //if (fromDate != null)
            //    fromDateUtc = fromDate.Value;
            //DateTime? toDateUtc = null;
            //if (toDate != null)
            //    toDateUtc = toDate.Value;
            try
            {
                prescribedMedicines = await _dashBoardRepository.GetDashboardDiagnosticPrescribedTestWise(fromDate, toDate);
                if (prescribedMedicines != null)
                {
                    return Ok(prescribedMedicines);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardDiagnosticPrescribedTestWise", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardDiagnosticPrescribedTestWise", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardDiagnosticPrescribedTestWise API " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetDashboardDiagnosticPrescribedPHCWise")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardDiagnosticPrescribedPHCWiseVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardDiagnosticPrescribedPHCWise(DateTime? fromDate, DateTime? toDate)
        {
            List<GetDashboardDiagnosticPrescribedPHCWiseVM> prescribedMedicines = new List<GetDashboardDiagnosticPrescribedPHCWiseVM>();
            //DateTime? fromDateUtc = null;
            //if (fromDate != null)
            //    fromDateUtc = fromDate.Value;
            //DateTime? toDateUtc = null;
            //if (toDate != null)
            //    toDateUtc = toDate.Value;
            try
            {
                prescribedMedicines = await _dashBoardRepository.GetDashboardDiagnosticPrescribedPHCWise(fromDate, toDate);
                if (prescribedMedicines != null)
                {
                    return Ok(prescribedMedicines);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardDiagnosticPrescribedPHCWise", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardDiagnosticPrescribedPHCWise", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardDiagnosticPrescribedPHCWise API " + ex);
                return StatusCode(500, ModelState);
            }

        }


        [HttpGet]
        [Route("GetPrescribedMedicinePHCWise")]
        [ProducesResponseType(200, Type = typeof(List<PrescribedMedicinePHCWiseVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPrescribedMedicinePHCWise(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<PrescribedMedicinePHCWiseVM> prescribedMedicines = new List<PrescribedMedicinePHCWiseVM>();
            DateTime? fromDateUtc = null;
            if (fromDate != null)
                fromDateUtc = fromDate.Value;
            DateTime? toDateUtc = null;
            if (toDate != null)
                toDateUtc = toDate.Value;
            try
            {
                prescribedMedicines = await _dashBoardRepository.GetPrescribedMedicinePHCWiseList(fromDateUtc, toDateUtc);
                if (prescribedMedicines != null)
                {
                    return Ok(prescribedMedicines);
                }
                else
                {
                    ModelState.AddModelError("GetPrescribedMedicine", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetPrescribedMedicine", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetPrescribedMedicinePHCWise API " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetDashboardGraph")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardGraphVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardGraph()
        {
            List<GetDashboardGraphVM> prescribedMedicines = new List<GetDashboardGraphVM>();
            
            try
            {
                prescribedMedicines = await _dashBoardRepository.GetDashboardGraph();
                if (prescribedMedicines != null)
                {
                    return Ok(prescribedMedicines);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardGraph", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardGraph", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardGraph API " + ex);
                return StatusCode(500, ModelState);
            }

        }


        [HttpGet]
        [Route("GetDashboardFeedbackSummaryReport")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardFeedbackSummaryReportVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardFeedbackSummaryReport()
        {
            List<GetDashboardFeedbackSummaryReportVM> prescribedMedicines = new List<GetDashboardFeedbackSummaryReportVM>();
            
            try
            {
                prescribedMedicines = await _dashBoardRepository.GetDashboardFeedbackSummaryReport();
                if (prescribedMedicines != null)
                {
                    return Ok(prescribedMedicines);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardFeedbackSummaryReport", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardFeedbackSummaryReport", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardFeedbackSummaryReport API " + ex);
                return StatusCode(500, ModelState);
            }

        }
        
        [HttpGet]
        [Route("GetDashboardFeedbackReport")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardFeedbackReportVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardFeedbackReport(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<GetDashboardFeedbackReportVM> prescribedMedicines = new List<GetDashboardFeedbackReportVM>();
            DateTime? fromDateUtc = null;
            if (fromDate != null)
                fromDateUtc = fromDate.Value;
            DateTime? toDateUtc = null;
            if (toDate != null)
                toDateUtc = toDate.Value;
            try
            {
                prescribedMedicines = await _dashBoardRepository.GetDashboardFeedbackReport(fromDateUtc, toDateUtc);
                if (prescribedMedicines != null)
                {
                    return Ok(prescribedMedicines);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardFeedbackReport", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardFeedbackReport", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardFeedbackReport API " + ex);
                return StatusCode(500, ModelState);
            }

        }
        
        [HttpGet]
        [Route("GetDashboardDignosisSpecialityWise")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardDignosisSpecialityWiseVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardDignosisSpecialityWise( int Specialityid,DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<GetDashboardDignosisSpecialityWiseVM> prescribedMedicines = new List<GetDashboardDignosisSpecialityWiseVM>();
            DateTime? fromDateUtc = null;
            if (fromDate != null)
                fromDateUtc = fromDate.Value;
            DateTime? toDateUtc = null;
            if (toDate != null)
                toDateUtc = toDate.Value;
            try
            {
                prescribedMedicines = await _dashBoardRepository.GetDashboardDignosisSpecialityWise(fromDateUtc, toDateUtc, Specialityid);
                if (prescribedMedicines != null)
                {
                    return Ok(prescribedMedicines);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardDignosisSpecialityWise", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardDignosisSpecialityWise", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardDignosisSpecialityWise API " + ex);
                return StatusCode(500, ModelState);
            }

        }
        
        [HttpGet]
        [Route("GetDashboardDiseasephcWise")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardDiseasephcWiseVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardDiseasephcWise(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<GetDashboardDiseasephcWiseVM> prescribedMedicines = new List<GetDashboardDiseasephcWiseVM>();
            DateTime? fromDateUtc = null;
            if (fromDate != null)
                fromDateUtc = fromDate.Value;
            DateTime? toDateUtc = null;
            if (toDate != null)
                toDateUtc = toDate.Value;
            try
            {
                prescribedMedicines = await _dashBoardRepository.GetDashboardDiseasephcWise(fromDateUtc, toDateUtc);
                if (prescribedMedicines != null)
                {
                    return Ok(prescribedMedicines);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardDiseasephcWise", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardDiseasephcWise", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardDiseasephcWise API " + ex);
                return StatusCode(500, ModelState);
            }

        }
        
        [HttpGet]
        [Route("GetDashboardDiseaseAgeWise")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardDiseaseAgeWiseVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardDiseaseAgeWise(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<GetDashboardDiseaseAgeWiseVM> prescribedMedicines = new List<GetDashboardDiseaseAgeWiseVM>();
            DateTime? fromDateUtc = null;
            if (fromDate != null)
                fromDateUtc = fromDate.Value;
            DateTime? toDateUtc = null;
            if (toDate != null)
                toDateUtc = toDate.Value;
            try
            {
                
                prescribedMedicines = await _dashBoardRepository.GetDashboardDiseaseAgeWise(fromDateUtc, toDateUtc);
                if (prescribedMedicines != null)
                {
                    return Ok(prescribedMedicines);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardDiseaseAgeWise", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardDiseaseAgeWise", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardDiseaseAgeWise API " + ex);
                return StatusCode(500, ModelState);
            }

        }


        [HttpGet]
        [Route("GetDashboardSystemHealthReport")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardSystemHealthReportVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardSystemHealthReport(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<GetDashboardSystemHealthReportVM> prescribedMedicines = new List<GetDashboardSystemHealthReportVM>();
           
            try
            {

                prescribedMedicines = await _dashBoardRepository.GetDashboardSystemHealthReport(fromDate, toDate);
                if (prescribedMedicines.Count > 0)
                {
                    return Ok(prescribedMedicines);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardSystemHealthReport", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardSystemHealthReport", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardSystemHealthReport API " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("RemoteSiteDowntimeSummaryDaily")]
        [ProducesResponseType(200, Type = typeof(List<RemoteSiteDowntimeSummaryDailyVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoteSiteDowntimeSummaryDaily(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<RemoteSiteDowntimeSummaryDailyVM> prescribedMedicines = new List<RemoteSiteDowntimeSummaryDailyVM>();
            //DateTime? fromDateUtc = null;
            //if (fromDate != null)
            //    fromDateUtc = fromDate.Value;
            //DateTime? toDateUtc = null;
            //if (toDate != null)
            //    toDateUtc = toDate.Value;
            try
            {

                prescribedMedicines = await _dashBoardRepository.RemoteSiteDowntimeSummaryDaily(fromDate, toDate);
                if (prescribedMedicines != null)
                {
                    return Ok(prescribedMedicines);
                }
                else
                {
                    ModelState.AddModelError("RemoteSiteDowntimeSummaryDaily", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("RemoteSiteDowntimeSummaryDaily", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in RemoteSiteDowntimeSummaryDaily API " + ex);
                return StatusCode(500, ModelState);
            }

        }


        [HttpGet]
        [Route("RemoteSiteDowntimeSummaryMonthly")]
        [ProducesResponseType(200, Type = typeof(List<RemoteSiteDowntimeSummaryMonthlyVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoteSiteDowntimeSummaryMonthly(int year, int Month)
        {
            List<RemoteSiteDowntimeSummaryMonthlyVM> prescribedMedicines = new List<RemoteSiteDowntimeSummaryMonthlyVM>();
            //DateTime? fromDateUtc = null;
            //if (fromDate != null)
            //    fromDateUtc = fromDate.Value;
            //DateTime? toDateUtc = null;
            //if (toDate != null)
            //    toDateUtc = toDate.Value;
            try
            {

                prescribedMedicines = await _dashBoardRepository.RemoteSiteDowntimeSummaryMonthly(Month,year);
                if (prescribedMedicines != null)
                {
                    return Ok(prescribedMedicines);
                }
                else
                {
                    ModelState.AddModelError("RemoteSiteDowntimeSummaryMonthly", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("RemoteSiteDowntimeSummaryMonthly", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in RemoteSiteDowntimeSummaryMonthly API " + ex);
                return StatusCode(500, ModelState);
            }

        }


        [HttpGet]
        [Route("GetDashboardFeedbackSummaryReportData")]
        [ProducesResponseType(200, Type = typeof(List<GetDashboardFeedbackSummaryReportDataVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardFeedbackSummaryReportData()
        {
            List<GetDashboardFeedbackSummaryReportDataVM> listdata = new List<GetDashboardFeedbackSummaryReportDataVM>();
            
            try
            {
                listdata = await _dashBoardRepository.GetDashboardFeedbackSummaryReportData();
                if (listdata != null)
                {
                    return Ok(listdata);
                }
                else
                {
                    ModelState.AddModelError("GetDashboardFeedbackSummaryReportData", $"Data not found!");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetDashboardFeedbackSummaryReportData", $"Something went wrong {ex.Message}");
                _logger.LogError("Exception in GetDashboardFeedbackSummaryReportData API " + ex);
                return StatusCode(500, ModelState);
            }

        }

    }
}
