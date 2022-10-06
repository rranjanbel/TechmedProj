using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.BaseClasses
{
    public class DashBoardRepository : IDashBoardRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DashBoardRepository> _logger;
        public DashBoardRepository(ILogger<DashBoardRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<List<DoctorDTO>> DoctorsLoggedInToday(DoctorsLoggedInTodayVM doctorsLoggedInTodayVM)
        {
            List<DoctorDTO> list = new List<DoctorDTO>();
            list = (from d in _teleMedecineContext.DoctorMasters
                    join u in _teleMedecineContext.UserMasters on d.UserId equals u.Id
                    join ud in _teleMedecineContext.UserDetails on d.UserId equals ud.UserId
                    where
                    d.BlockId == (doctorsLoggedInTodayVM.ZoneID == null ? d.BlockId : doctorsLoggedInTodayVM.ZoneID)
                    && d.ClusterId == (doctorsLoggedInTodayVM.ClusterID == null ? d.ClusterId : doctorsLoggedInTodayVM.ClusterID)
                    && u.LastLoginAt.Value.Day == UtilityMaster.GetLocalDateTime().Day
                    && u.LastLoginAt.Value.Month == UtilityMaster.GetLocalDateTime().Month
                    && u.LastLoginAt.Value.Year == UtilityMaster.GetLocalDateTime().Year
                    select new DoctorDTO
                    {
                        AccountNumber = d.AccountNumber,
                        BankName = d.BankName,
                        BranchName = d.BranchName,
                        ClusterId = d.ClusterId,
                        Designation = d.Designation,
                        DigitalSignature = d.DigitalSignature,
                        Id = d.Id,
                        Ifsccode = d.Ifsccode,
                        Mciid = d.Mciid,
                        PanNo = d.Panno,
                        PhoneNumber = d.PhoneNumber,
                        Qualification = d.Qualification,
                        RegistrationNumber = d.RegistrationNumber,
                        SpecializationId = d.SpecializationId,
                        SubSpecializationId = d.SubSpecializationId,
                        UpdatedBy = d.UpdatedBy,
                        UserId = d.UserId,
                        BlockID = d.BlockId,
                        detailsDTO = new DetailsDTO
                        {
                            Address = ud.Address,
                            City = ud.City,
                            CountryId = ud.CountryId,
                            Dob = ud.Dob,
                            EmailId = ud.EmailId,
                            FirstName = ud.FirstName,
                            GenderId = ud.GenderId,
                            IdproofNumber = ud.IdproofNumber,
                            IdproofTypeId = ud.IdproofTypeId,
                            LastName = ud.LastName,
                            MiddleName = ud.MiddleName,
                            Photo = ud.Photo,
                            PinCode = ud.PinCode,
                            StateId = ud.StateId,
                            TitleId = ud.TitleId,
                        }

                    }).ToList();
            return list;
        }

        public LoggedUserCountVM GetLoggedUserTypeCount(int usertTypeId = 0)
        {
            LoggedUserCountVM loggedUserReport = new LoggedUserCountVM();
            if (usertTypeId > 0)
            {
                var Results = _teleMedecineContext.LoggedUserCount.FromSqlInterpolated($"EXEC [dbo].[GetPHCCount] @UserTypeID ={usertTypeId}");
                foreach (var item in Results)
                {
                    loggedUserReport.Count = item.Count;
                    loggedUserReport.UserTypeName = item.UserTypeName;
                }
            }
            return loggedUserReport;
        }

        public async Task<List<SpecializationReportVM>> GetTodaysConsultedPatientList()
        {
            List<SpecializationReportVM> specializationReports = new List<SpecializationReportVM>();
            //SpecializationReportVM specializationReport;
            //var Results = _teleMedecineContext.SpecializationReport.FromSqlInterpolated($"EXEC [dbo].[GetVisitedPatientCase]");
            //foreach (var item in Results)
            //{
            //    specializationReport = new SpecializationReportVM();
            //    specializationReport.Count = item.Count;
            //    specializationReport.SpecializationID = item.SpecializationID;
            //    specializationReport.Specialization = item.Specialization;
            //    specializationReports.Add(specializationReport);
            //}
            specializationReports = await _teleMedecineContext.SpecializationReport.FromSqlRaw("GetVisitedPatientCase").ToListAsync();
            return specializationReports;
        }

        public async Task<List<LoggedUserCountVM>> GetTodaysLoggedUsersCount()
        {
            List<LoggedUserCountVM> loggedUserReports = new List<LoggedUserCountVM>();

            loggedUserReports = await _teleMedecineContext.LoggedUserCount.FromSqlRaw("GetAllUserLoginCount").ToListAsync();

            return loggedUserReports;
        }

        public async Task<List<SpecializationReportVM>> GetTodaysRegistoredPatientList()
        {
            List<SpecializationReportVM> specializationReports = new List<SpecializationReportVM>();
            SpecializationReportVM specializationReport;
            //var Results = _teleMedecineContext.SpecializationReport.FromSqlInterpolated($"EXEC [dbo].[GetTotalPatientCase]");
            //foreach (var item in Results)
            //{
            //    specializationReport = new SpecializationReportVM();
            //    specializationReport.Count = item.Count;
            //    specializationReport.SpecializationID = item.SpecializationID;
            //    specializationReport.Specialization = item.Specialization;
            //    specializationReports.Add(specializationReport);
            //}
            specializationReports = await _teleMedecineContext.SpecializationReport.FromSqlRaw("GetTotalPatientCase").ToListAsync();
            return specializationReports;
        }

        public async Task<List<DashboardConsultationVM>> GetDashboardConsultation(GetDashboardConsultationVM getDashboardConsultationVM)
        {
            List<DashboardConsultationVM> dashboardConsultations = new List<DashboardConsultationVM>();

            int SrNo = 0;
            if (true)
            {
                DashboardConsultationVM CompletedConsultantReport;
                var Results = _teleMedecineContext.GetDashboardConsultation.FromSqlInterpolated($"EXEC [dbo].[GetDashboardConsultation] @SpecializationID={getDashboardConsultationVM.SpecializationID},@FromDate={getDashboardConsultationVM.FromDate},@ToDate={getDashboardConsultationVM.ToDate}");
                foreach (var item in Results)
                {
                    SrNo = SrNo + 1;
                    CompletedConsultantReport = new DashboardConsultationVM();
                    CompletedConsultantReport.SrNo = SrNo;

                    CompletedConsultantReport.DistrictName = item.DistrictName;
                    CompletedConsultantReport.BlockName = item.BlockName;
                    CompletedConsultantReport.Complaint = item.Complaint;
                    CompletedConsultantReport.AssignedOn = item.AssignedOn;
                    CompletedConsultantReport.PatientName = item.PatientName;
                    CompletedConsultantReport.Age = item.Age;
                    CompletedConsultantReport.Gender = item.Gender;
                    CompletedConsultantReport.PHCName = item.PHCName;
                    CompletedConsultantReport.PHCTechnician = item.PHCTechnician;

                    CompletedConsultantReport.Doctor = item.Doctor;
                    CompletedConsultantReport.PHCAddress = item.PHCAddress;
                    CompletedConsultantReport.Cluster = item.Cluster;
                    CompletedConsultantReport.CluserID = item.CluserID;
                    CompletedConsultantReport.BlockName = item.BlockName;
                    CompletedConsultantReport.BlockID = item.BlockID;

                    CompletedConsultantReport.PHCID = item.PHCID;
                    CompletedConsultantReport.CreatedBy = item.CreatedBy;
                    CompletedConsultantReport.PatientCreatedBy = item.PatientCreatedBy;
                    dashboardConsultations.Add(CompletedConsultantReport);
                }
            }

            return dashboardConsultations;

        }

        public async Task<List<DashboardReportSummaryVM>> GetDashboardReportSummary(GetDashboardReportSummaryVM getDashboardReportSummaryVM)
        {
            List<DashboardReportSummaryVM> dashboardConsultations = new List<DashboardReportSummaryVM>();

            //int SrNo = 0;
            if (true)
            {
                DashboardReportSummaryVM CompletedConsultantReport;
                var Results = _teleMedecineContext.GetDashboardReportSummary.FromSqlInterpolated($"EXEC [dbo].[GetDashboardReportSummary] @FromDate={getDashboardReportSummaryVM.FromDate},@ToDate={getDashboardReportSummaryVM.ToDate}");
                foreach (var item in Results)
                {
                    //SrNo = SrNo + 1;
                    CompletedConsultantReport = new DashboardReportSummaryVM();

                    CompletedConsultantReport.SLNo = item.SLNo;
                    CompletedConsultantReport.District = item.District;
                    CompletedConsultantReport.Block = item.Block;
                    CompletedConsultantReport.PHC = item.PHC;
                    CompletedConsultantReport.Total = item.Total;
                    CompletedConsultantReport.GeneralMedicine = item.GeneralMedicine;
                    CompletedConsultantReport.ObstetricsAndGyne = item.ObstetricsAndGyne;
                    CompletedConsultantReport.Pediatrics = item.Pediatrics;
                    dashboardConsultations.Add(CompletedConsultantReport);
                }
            }

            return dashboardConsultations;

        }

        public List<PHCLoginHistoryReportVM> GetPHCLoginHistoryReport(int PHCId, DateTime? fromDate, DateTime? toDate)
        {
            List<PHCLoginHistoryReportVM> phcLoginReports = new List<PHCLoginHistoryReportVM>();
            PHCLoginHistoryReportVM phcLoginHistoryReport;
            try
            {

                if (PHCId > -1)
                {
                    var Results = _teleMedecineContext.PHCLoginHistoryReports.FromSqlInterpolated($"EXEC [dbo].[GetPHCLoginReport] @PHCID ={PHCId}, @FromDate ={fromDate}, @ToDate ={toDate}");
                    foreach (var item in Results)
                    {
                        phcLoginHistoryReport = new PHCLoginHistoryReportVM();
                        phcLoginHistoryReport.SrNo = item.SrNo;
                        phcLoginHistoryReport.DistrictName = item.DistrictName;
                        phcLoginHistoryReport.BlockName = item.BlockName;
                        phcLoginHistoryReport.PHCName = item.PHCName;
                        phcLoginHistoryReport.UserID = item.UserID;
                        phcLoginHistoryReport.LogedDate = item.LogedDate;
                        phcLoginHistoryReport.LoginTime = item.LoginTime;
                        phcLoginHistoryReport.LogoutTime = item.LogoutTime;
                        phcLoginHistoryReport.Remark = item.Remark;
                        phcLoginHistoryReport.Status = item.Status;
                        phcLoginHistoryReport.TotalTime = item.TotalTime;
                        phcLoginReports.Add(phcLoginHistoryReport);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return phcLoginReports;
        }

        public List<PHCConsultationVM> GetPHCConsultationReport(int PHCId, DateTime? fromDate, DateTime? toDate)
        {
            List<PHCConsultationVM> phcconsultationReports = new List<PHCConsultationVM>();
            PHCConsultationVM phcconsultationReport;
            if (PHCId >= 0)
            {
                var Results = _teleMedecineContext.PHCConsultationResult.FromSqlInterpolated($"EXEC [dbo].[GetDashboardPHCConsultation] @PHCID ={PHCId}, @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    phcconsultationReport = new PHCConsultationVM();
                    phcconsultationReport.SrNo = item.SrNo;
                    phcconsultationReport.DistrictName = item.DistrictName;
                    phcconsultationReport.BlockName = item.BlockName;
                    phcconsultationReport.PHCName = item.PHCName;
                    phcconsultationReport.NoOfConsultation = item.NoOfConsultation;
                    phcconsultationReport.ConsultationDate = item.ConsultationDate;

                    phcconsultationReports.Add(phcconsultationReport);
                }
            }
            return phcconsultationReports;
        }

        #region Duplicate GetDashboardReportSummary
        //public async Task<List<DashboardReportSummaryVM>> GetDashboardReportSummary(GetDashboardReportSummaryVM getDashboardReportSummaryVM)
        //{
        //    List<DashboardReportSummaryVM> dashboardConsultations = new List<DashboardReportSummaryVM>();

        //    //int SrNo = 0;
        //    if (true)
        //    {
        //        DashboardReportSummaryVM CompletedConsultantReport;
        //        var Results = _teleMedecineContext.GetDashboardReportSummary.FromSqlInterpolated($"EXEC [dbo].[GetDashboardReportSummary] @FromDate={getDashboardReportSummaryVM.FromDate},@ToDate={getDashboardReportSummaryVM.ToDate}");
        //        foreach (var item in Results)
        //        {
        //            //SrNo = SrNo + 1;
        //            CompletedConsultantReport = new DashboardReportSummaryVM();

        //            CompletedConsultantReport.SLNo = item.SLNo;
        //            CompletedConsultantReport.District = item.District;
        //            CompletedConsultantReport.Block = item.Block;
        //            CompletedConsultantReport.PHC = item.PHC;
        //            CompletedConsultantReport.Total = item.Total;
        //            CompletedConsultantReport.GeneralPractice = item.GeneralPractice;
        //            CompletedConsultantReport.ObstetricsAndGyne = item.ObstetricsAndGyne;
        //            CompletedConsultantReport.Pediatrics = item.Pediatrics;
        //            dashboardConsultations.Add(CompletedConsultantReport);
        //        }
        //    }

        //    return dashboardConsultations;

        //}
        #endregion
        public async Task<List<DashboardReportSummaryVM>> GetDashboardReportSummaryMonthly(GetDashboardReportSummaryMonthVM getDashboardReportSummaryVM)
        {
            List<DashboardReportSummaryVM> dashboardConsultations = new List<DashboardReportSummaryVM>();

            //int SrNo = 0;
            if (true)
            {
                DashboardReportSummaryVM CompletedConsultantReport;
                var Results = _teleMedecineContext.GetDashboardReportSummaryMonthly.FromSqlInterpolated($"EXEC [dbo].[GetDashboardReportSummaryMonthly] @month={getDashboardReportSummaryVM.month},@year={getDashboardReportSummaryVM.year}");
                foreach (var item in Results)
                {
                    //SrNo = SrNo + 1;
                    CompletedConsultantReport = new DashboardReportSummaryVM();

                    CompletedConsultantReport.SLNo = item.SLNo;
                    CompletedConsultantReport.District = item.District;
                    CompletedConsultantReport.Block = item.Block;
                    CompletedConsultantReport.PHC = item.PHC;
                    CompletedConsultantReport.Total = item.Total;
                    CompletedConsultantReport.GeneralMedicine = item.GeneralMedicine;
                    CompletedConsultantReport.ObstetricsAndGyne = item.ObstetricsAndGyne;
                    CompletedConsultantReport.Pediatrics = item.Pediatrics;
                    dashboardConsultations.Add(CompletedConsultantReport);
                }
            }

            return dashboardConsultations;

        }
        public async Task<List<DashboardReportConsultationVM>> GetDashboardReportConsultation(GetDashboardReportConsultationVM getDashboardReportSummaryVM)
        {
            List<DashboardReportConsultationVM> dashboardConsultations = new List<DashboardReportConsultationVM>();

            //int SrNo = 0;
            if (true)
            {
                DashboardReportConsultationVM dashboardReportConsultationVM;
                var Results = _teleMedecineContext.GetDashboardReportConsultation
                    .FromSqlInterpolated
                    ($"EXEC [dbo].[GetDashboardReportConsultation] @PatientName={getDashboardReportSummaryVM.PatientName},@MobileNo={getDashboardReportSummaryVM.MobileNo},@OPDNo={getDashboardReportSummaryVM.OPDNo},@PHCID={getDashboardReportSummaryVM.PHCID},@FromDate={getDashboardReportSummaryVM.FromDate},@ToDate={getDashboardReportSummaryVM.ToDate}");
                foreach (var item in Results)
                {
                    //SrNo = SrNo + 1;
                    dashboardReportConsultationVM = new DashboardReportConsultationVM();
                    dashboardReportConsultationVM.MobileNo = item.MobileNo;
                    dashboardReportConsultationVM.OPDNo = item.OPDNo;
                    dashboardReportConsultationVM.ReviewDate = item.ReviewDate;
                    dashboardReportConsultationVM.ConsultDate = item.ConsultDate;
                    dashboardReportConsultationVM.starttime = item.starttime;
                    dashboardReportConsultationVM.endtime = item.endtime;
                    dashboardReportConsultationVM.specialization = item.specialization;
                    dashboardReportConsultationVM.SrNo = item.SrNo;
                    dashboardReportConsultationVM.DistrictName = item.DistrictName;//
                    dashboardReportConsultationVM.BlockName = item.BlockName;//
                    dashboardReportConsultationVM.Complaint = item.Complaint;//
                    dashboardReportConsultationVM.AssignedOn = item.AssignedOn;
                    dashboardReportConsultationVM.PatientName = item.PatientName;
                    dashboardReportConsultationVM.Age = item.Age;
                    dashboardReportConsultationVM.Gender = item.Gender;
                    dashboardReportConsultationVM.PHCName = item.PHCName;
                    dashboardReportConsultationVM.PHCTechnician = item.PHCTechnician;
                    dashboardReportConsultationVM.Doctor = item.Doctor;
                    dashboardReportConsultationVM.PHCAddress = item.PHCAddress;
                    dashboardReportConsultationVM.CluserID = item.CluserID;
                    dashboardReportConsultationVM.Cluster = item.Cluster;
                    dashboardReportConsultationVM.BlockID = item.BlockID;
                    dashboardReportConsultationVM.PHCID = item.PHCID;
                    dashboardReportConsultationVM.CreatedBy = item.CreatedBy;
                    dashboardReportConsultationVM.PatientCreatedBy = item.PatientCreatedBy;
                    dashboardReportConsultationVM.Prescription = item.Prescription;
                    dashboardConsultations.Add(dashboardReportConsultationVM);
                }
            }

            return dashboardConsultations;

        }

        public PHCMainpowerResultSetVM GetPHCManpowerReport(int year, int month)
        {
            List<PHCManpowerVM> phcManpowerReports = new List<PHCManpowerVM>();
            PHCManpowerVM phcManpowerReport;
            PHCMainpowerResultSetVM phcmanpowerresultset = new PHCMainpowerResultSetVM();
            int totalWorkingDays = 0;
            int totaldaysPresnt = 0;
            if (year > 0 && month > 0)
            {
                var Results = _teleMedecineContext.PHCManpowerReports.FromSqlInterpolated($"EXEC [dbo].[GetPHCManpowerReport] @year ={year}, @month ={month}");
                foreach (var item in Results)
                {
                    phcManpowerReport = new PHCManpowerVM();
                    phcManpowerReport.SrNo = item.SrNo;
                    phcManpowerReport.DistrictName = item.DistrictName;
                    phcManpowerReport.BlockName = item.BlockName;
                    phcManpowerReport.PHCName = item.PHCName;
                    phcManpowerReport.TotalWorkingDays = item.TotalWorkingDays;
                    phcManpowerReport.WorkingDays = item.WorkingDays;
                    phcManpowerReport.DaysPresent = item.DaysPresent;
                    phcManpowerReport.DaysAbsent = item.DaysAbsent;
                    phcManpowerReport.NoOfDaysInMonth = item.NoOfDaysInMonth;
                    phcManpowerReport.PHCID = item.PHCID;
                    phcManpowerReports.Add(phcManpowerReport);
                }
            }
            phcmanpowerresultset.PHCManpowerReports = phcManpowerReports;
            phcmanpowerresultset.NoOfDaysInMonth = phcManpowerReports.Select(a => a.NoOfDaysInMonth).FirstOrDefault();
            phcmanpowerresultset.TotalPresentDays = totaldaysPresnt = phcManpowerReports.Sum(a => a.DaysPresent);
            phcmanpowerresultset.TotalWorkingDays = totalWorkingDays = phcManpowerReports.Sum(a => a.TotalWorkingDays);
            phcmanpowerresultset.AvailabilityPercentage = ((totaldaysPresnt * 100) / totalWorkingDays);

            return phcmanpowerresultset;
        }

        public List<RegisterPatientVM> GetRegisterPatientReport(DateTime? fromDate, DateTime? toDate)
        {
            List<RegisterPatientVM> registerPatientReports = new List<RegisterPatientVM>();
            RegisterPatientVM registerPatientReport;
            try
            {
                var Results = _teleMedecineContext.RegisterPatientReports.FromSqlInterpolated($"EXEC [dbo].[GetRegisterPatientReport] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    registerPatientReport = new RegisterPatientVM();
                    registerPatientReport.SrNo = item.SrNo;
                    registerPatientReport.DistrictName = item.DistrictName;
                    registerPatientReport.BlockName = item.BlockName;
                    registerPatientReport.PHCName = item.PHCName;
                    registerPatientReport.PatientName = item.PatientName;
                    registerPatientReport.Gender = item.Gender;
                    registerPatientReport.Age = item.Age;
                    registerPatientReport.RegistrationDate = item.RegistrationDate;
                    //registerPatientReport.PatientCaseID = item.PatientCaseID;
                    registerPatientReports.Add(registerPatientReport);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return registerPatientReports;
        }

        public async Task<EquipmentUptimeReportDTO> AddEquipmentUptimeReport(EquipmentUptimeReportDTO equipmentUptimeReport)
        {
            int i = 0;
            EquipmentUptimeReport equipmentUptime = new EquipmentUptimeReport();

            EquipmentUptimeReportDTO equipmentUptimeReportdto = new EquipmentUptimeReportDTO();
            try
            {
                if (equipmentUptimeReport != null)
                {
                    equipmentUptime = _mapper.Map<EquipmentUptimeReport>(equipmentUptimeReport);
                    var ressult = _teleMedecineContext.EquipmentUptimeReports.AddAsync(equipmentUptime);
                    i = await _teleMedecineContext.SaveChangesAsync();
                    if (i > 0)
                    {
                        equipmentUptimeReportdto = _mapper.Map<EquipmentUptimeReportDTO>(equipmentUptime);
                    }
                }
            }
            catch (Exception ex)
            {
                string excpMessage = ex.Message;
            }

            return equipmentUptimeReportdto;
        }


        public List<GetReferredPatientVM> GetReferredPatientReport(DateTime? fromDate, DateTime? toDate)
        {
            List<GetReferredPatientVM> registerPatientReports = new List<GetReferredPatientVM>();
            GetReferredPatientVM registerPatientReport;
            try
            {
                var Results = _teleMedecineContext.ReferredPatientReports.FromSqlInterpolated($"EXEC [dbo].[GetReferredPatientReport] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    registerPatientReport = new GetReferredPatientVM();
                    registerPatientReport.SrNo = item.SrNo;
                    registerPatientReport.DistrictName = item.DistrictName;
                    registerPatientReport.BlockName = item.BlockName;
                    registerPatientReport.PHCName = item.PHCName;
                    registerPatientReport.PatientName = item.PatientName;
                    registerPatientReport.DoctorName = item.DoctorName;
                    registerPatientReport.Consultdate = item.Consultdate;
                    registerPatientReport.ReferralNote = item.ReferralNote;
                    registerPatientReport.Complaints = item.Complaints;
                    registerPatientReport.Prescription = item.Prescription;
                    registerPatientReports.Add(registerPatientReport);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return registerPatientReports;
        }
        public List<GetReviewPatientVM> GetReviewPatientReport(DateTime? fromDate, DateTime? toDate)
        {
            List<GetReviewPatientVM> registerPatientReports = new List<GetReviewPatientVM>();
            GetReviewPatientVM registerPatientReport;
            try
            {
                var Results = _teleMedecineContext.ReviewPatientReport.FromSqlInterpolated($"EXEC [dbo].[GetReviewPatientReport] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    registerPatientReport = new GetReviewPatientVM();
                    registerPatientReport.SrNo = item.SrNo;
                    registerPatientReport.DistrictName = item.DistrictName;
                    registerPatientReport.BlockName = item.BlockName;
                    registerPatientReport.PHCName = item.PHCName;
                    registerPatientReport.PatientName = item.PatientName;
                    registerPatientReport.DoctorName = item.DoctorName;
                    registerPatientReport.Consultdate = item.Consultdate;
                    registerPatientReport.ReviewDate = item.ReviewDate;
                    registerPatientReport.Complaints = item.Complaints;
                    registerPatientReport.Prescription = item.Prescription;
                    registerPatientReports.Add(registerPatientReport);

                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return registerPatientReports;
        }

        public List<GetDashboardSpokeMaintenanceVM> GetDashboardSpokeMaintenance(DateTime? fromDate, DateTime? toDate)
        {
            List<GetDashboardSpokeMaintenanceVM> registerPatientReports = new List<GetDashboardSpokeMaintenanceVM>();
            GetDashboardSpokeMaintenanceVM registerPatientReport;
            try
            {
                var Results = _teleMedecineContext.GetDashboardSpokeMaintenance.FromSqlInterpolated($"EXEC [dbo].[GetDashboardSpokeMaintenance] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    registerPatientReport = new GetDashboardSpokeMaintenanceVM();
                    registerPatientReport.SrNo = item.SrNo;
                    registerPatientReport.DistrictName = item.DistrictName;
                    registerPatientReport.BlockName = item.BlockName;
                    registerPatientReport.PHCName = item.PHCName;
                    registerPatientReport.DC = item.DC;
                    registerPatientReport.Date = item.Date;
                    registerPatientReport.FilePath = item.FilePath;
                    registerPatientReports.Add(registerPatientReport);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return registerPatientReports;
        }

        public List<GetDashboardEmployeeFeedbackVM> GetDashboardEmployeeFeedback(int? Fromyear, string qtr)
        {
            List<GetDashboardEmployeeFeedbackVM> registerPatientReports = new List<GetDashboardEmployeeFeedbackVM>();
            GetDashboardEmployeeFeedbackVM registerPatientReport;
            try
            {
                int? Frommonth = 0, ToMonth = 0;
                if ("1-Qtr" == qtr)
                {
                    Frommonth = 1;
                    ToMonth = 3;
                }
                if ("2-Qtr" == qtr)
                {
                    Frommonth = 4;
                    ToMonth = 6;
                }
                if ("3-Qtr" == qtr)
                {
                    Frommonth = 7;
                    ToMonth = 9;
                }
                if ("4-Qtr" == qtr)
                {
                    Frommonth = 10;
                    ToMonth = 12;
                }


                var Results = _teleMedecineContext.GetDashboardEmployeeFeedback.FromSqlInterpolated($"EXEC [dbo].[GetDashboardEmployeeFeedback] @Fromyear ={Fromyear}, @Frommonth ={Frommonth}, @ToMonth ={ToMonth}");
                foreach (var item in Results)
                {
                    registerPatientReport = new GetDashboardEmployeeFeedbackVM();
                    registerPatientReport.SrNo = item.SrNo;
                    registerPatientReport.DistrictName = item.DistrictName;
                    registerPatientReport.BlockName = item.BlockName;
                    registerPatientReport.PHCName = item.PHCName;
                    registerPatientReport.EmployeeName = item.EmployeeName;
                    registerPatientReport.TrainingSubject = item.TrainingSubject;
                    registerPatientReport.TrainingBy = item.TrainingBy;
                    registerPatientReport.TraingDate = item.TraingDate;
                    registerPatientReport.EmployeeFeedback = item.EmployeeFeedback;

                    registerPatientReports.Add(registerPatientReport);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return registerPatientReports;
        }

        public List<GetDashboardEquipmentUptimeReportVM> GetDashboardEquipmentUptimeReport(int month, int year)
        {
            List<GetDashboardEquipmentUptimeReportVM> registerPatientReports = new List<GetDashboardEquipmentUptimeReportVM>();
            GetDashboardEquipmentUptimeReportVM registerPatientReport;
            try
            {
                var Results = _teleMedecineContext.GetDashboardEquipmentUptimeReport.FromSqlInterpolated($"EXEC [dbo].[GetDashboardEquipmentUptimeReport] @month ={month}, @year ={year}");
                foreach (var item in Results)
                {
                    registerPatientReport = new GetDashboardEquipmentUptimeReportVM();
                    registerPatientReport.SrNo = item.SrNo;
                    registerPatientReport.DistrictName = item.DistrictName;
                    registerPatientReport.BlockName = item.BlockName;
                    registerPatientReport.PHCName = item.PHCName;
                    registerPatientReport.WokingDays = item.WokingDays;
                    registerPatientReport.Otoscope = item.Otoscope;
                    registerPatientReport.Dermascope = item.Dermascope;
                    registerPatientReport.FetalDoppler = item.FetalDoppler;
                    registerPatientReport.Headphone = item.Headphone;
                    registerPatientReport.WebCam = item.WebCam;
                    registerPatientReport.Printer = item.Printer;
                    registerPatientReport.Inverter = item.Inverter;
                    registerPatientReport.Computer = item.Computer;
                    registerPatientReports.Add(registerPatientReport);

                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return registerPatientReports;
        }

        public List<GetDashboardAppointmentVM> GetDashboardAppointment(DateTime? fromDate, DateTime? toDate)
        {
            List<GetDashboardAppointmentVM> registerPatientReports = new List<GetDashboardAppointmentVM>();
            GetDashboardAppointmentVM registerPatientReport;
            try
            {
                var Results = _teleMedecineContext.GetDashboardAppointment.FromSqlInterpolated($"EXEC [dbo].[GetDashboardAppointment] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    registerPatientReport = new GetDashboardAppointmentVM();
                    registerPatientReport.SrNo = item.SrNo;
                    registerPatientReport.DistrictName = item.DistrictName;
                    registerPatientReport.BlockName = item.BlockName;
                    registerPatientReport.PHCName = item.PHCName;
                    registerPatientReport.PatientName = item.PatientName;
                    registerPatientReport.MobileNo = item.MobileNo;
                    registerPatientReport.Doctor = item.Doctor;
                    registerPatientReport.AppointmentTime = item.AppointmentTime;
                    registerPatientReport.ConsultStatus = item.ConsultStatus;
                    registerPatientReport.DoctorAvailable = item.DoctorAvailable;
                    registerPatientReport.PatientAvailable = item.PatientAvailable;
                    registerPatientReports.Add(registerPatientReport);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return registerPatientReports;
        }

        public List<GetDashboardDoctorAvgTimeVM> GetDashboardDoctorAvgTime(DateTime? fromDate, DateTime? toDate)
        {
            List<GetDashboardDoctorAvgTimeVM> registerPatientReports = new List<GetDashboardDoctorAvgTimeVM>();
            GetDashboardDoctorAvgTimeVM registerPatientReport;
            try
            {
                var Results = _teleMedecineContext.GetDashboardDoctorAvgTime.FromSqlInterpolated($"EXEC [dbo].[GetDashboardDoctorAvgTime] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    registerPatientReport = new GetDashboardDoctorAvgTimeVM();
                    registerPatientReport.SrNo = item.SrNo;
                    registerPatientReport.Specialization = item.Specialization;
                    registerPatientReport.Doctor = item.Doctor;
                    registerPatientReport.AvgConsultTime = item.AvgConsultTime;
                    registerPatientReport.CurrentStatus = item.CurrentStatus;
                    registerPatientReports.Add(registerPatientReport);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return registerPatientReports;
        }
        public List<GetDashboardDoctorAvailabilityVM> GetDashboardDoctorAvailability(DateTime? fromDate, DateTime? toDate)
        {
            List<GetDashboardDoctorAvailabilityVM> registerPatientReports = new List<GetDashboardDoctorAvailabilityVM>();
            GetDashboardDoctorAvailabilityVM registerPatientReport;
            try
            {
                var Results = _teleMedecineContext.GetDashboardDoctorAvailability.FromSqlInterpolated($"EXEC [dbo].[GetDashboardDoctorAvailability] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    registerPatientReport = new GetDashboardDoctorAvailabilityVM();
                    registerPatientReport.SrNo = item.SrNo;
                    registerPatientReport.Specialization = item.Specialization;
                    registerPatientReport.Doctor = item.Doctor;
                    registerPatientReport.Date = item.Date;
                    registerPatientReport.LogedInTime = item.LogedInTime;
                    registerPatientReport.FirstConsultTime = item.FirstConsultTime;
                    registerPatientReport.LastConsultTime = item.LastConsultTime;
                    registerPatientReport.LogedoutTime = item.LogedoutTime;
                    registerPatientReport.NoOfConsultation = item.NoOfConsultation;
                    registerPatientReports.Add(registerPatientReport);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return registerPatientReports;
        }
        public List<GetDashboardEquipmentHeaderReportVM> GetDashboardEquipmentHeaderReport(int month, int year)
        {
            List<GetDashboardEquipmentHeaderReportVM> registerPatientReports = new List<GetDashboardEquipmentHeaderReportVM>();
            GetDashboardEquipmentHeaderReportVM registerPatientReport;
            try
            {
                var Results = _teleMedecineContext.GetDashboardEquipmentHeaderReport.FromSqlInterpolated($"EXEC [dbo].[GetDashboardEquipmentHeaderReport] @month ={month}, @year ={year}");
                foreach (var item in Results)
                {
                    registerPatientReport = new GetDashboardEquipmentHeaderReportVM();
                    registerPatientReport.SrNo = item.SrNo;
                    registerPatientReport.noOfPHC = item.noOfPHC;
                    registerPatientReport.workingDays = item.workingDays;
                    registerPatientReport.EquipmentAtPHC = item.EquipmentAtPHC;
                    registerPatientReport.ExpectedUpTime = item.ExpectedUpTime;
                    registerPatientReport.ActualUpTime = item.ActualUpTime;
                    registerPatientReport.Availability = item.Availability;
                    registerPatientReports.Add(registerPatientReport);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return registerPatientReports;
        }
        public async Task<List<PrescribedMedicineVM>> GetPrescribedMedicineList(DateTime? fromDate, DateTime? toDate)
        {
            List<PrescribedMedicineVM> prescribedMedicinesList = new List<PrescribedMedicineVM>();
            PrescribedMedicineVM prescribedMedicine;
            try
            {
                var Results = _teleMedecineContext.PrescribedMedicineReport.FromSqlInterpolated($"EXEC [dbo].[GetPrescribedMedicine] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    prescribedMedicine = new PrescribedMedicineVM();
                    prescribedMedicine.SrNo = item.SrNo;
                    prescribedMedicine.PrescribedMedicine = item.PrescribedMedicine;
                    prescribedMedicine.NumberOfTimePrescribed = item.NumberOfTimePrescribed;

                    prescribedMedicinesList.Add(prescribedMedicine);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return prescribedMedicinesList;
        }
        public async Task<List<PrescribedMedicinePHCWiseVM>> GetPrescribedMedicinePHCWiseList(DateTime? fromDate, DateTime? toDate)
        {
            List<PrescribedMedicinePHCWiseVM> prescribedMedicinesList = new List<PrescribedMedicinePHCWiseVM>();
            PrescribedMedicinePHCWiseVM prescribedMedicine;
            try
            {
                var Results = _teleMedecineContext.PrescribedMedicinePHCWiseReport.FromSqlInterpolated($"EXEC [dbo].[GetPrescribedMedicinePHCWise] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    prescribedMedicine = new PrescribedMedicinePHCWiseVM();
                    prescribedMedicine.SrNo = item.SrNo;
                    prescribedMedicine.DistrictName = item.DistrictName;
                    prescribedMedicine.BlockName = item.BlockName;
                    prescribedMedicine.PHCName = item.PHCName;
                    prescribedMedicine.PrescribedMedicine = item.PrescribedMedicine;
                    prescribedMedicine.NumberOfTimePrescribed = item.NumberOfTimePrescribed;
                    prescribedMedicine.EAushadhiStock = item.EAushadhiStock;
                    prescribedMedicine.QuantityPrescribed = item.QuantityPrescribed;

                    prescribedMedicinesList.Add(prescribedMedicine);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return prescribedMedicinesList;
        }

        public async Task<List<GetDashboardDiagnosticPrescribedTestWiseVM>> GetDashboardDiagnosticPrescribedTestWise(DateTime? fromDate, DateTime? toDate)
        {
            List<GetDashboardDiagnosticPrescribedTestWiseVM> prescribedMedicinesList = new List<GetDashboardDiagnosticPrescribedTestWiseVM>();
            GetDashboardDiagnosticPrescribedTestWiseVM prescribedMedicine;
            try
            {
                var Results = _teleMedecineContext.GetDashboardDiagnosticPrescribedTestWise.FromSqlInterpolated($"EXEC [dbo].[GetDashboardDiagnosticPrescribedTestWise] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    prescribedMedicine = new GetDashboardDiagnosticPrescribedTestWiseVM();
                    prescribedMedicine.SrNo = item.SrNo;
                    prescribedMedicine.Diagnostic = item.Diagnostic;
                    prescribedMedicine.NoOfTimePrescribed = item.NoOfTimePrescribed;
                    prescribedMedicinesList.Add(prescribedMedicine);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return prescribedMedicinesList;
        }
        public async Task<List<GetDashboardDiagnosticPrescribedPHCWiseVM>> GetDashboardDiagnosticPrescribedPHCWise(DateTime? fromDate, DateTime? toDate)
        {
            List<GetDashboardDiagnosticPrescribedPHCWiseVM> prescribedMedicinesList = new List<GetDashboardDiagnosticPrescribedPHCWiseVM>();
            GetDashboardDiagnosticPrescribedPHCWiseVM prescribedMedicine;
            try
            {
                var Results = _teleMedecineContext.GetDashboardDiagnosticPrescribedPHCWise.FromSqlInterpolated($"EXEC [dbo].[GetDashboardDiagnosticPrescribedPHCWise] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    prescribedMedicine = new GetDashboardDiagnosticPrescribedPHCWiseVM();
                    prescribedMedicine.SrNo = item.SrNo;
                    prescribedMedicine.DistrictName = item.DistrictName;
                    prescribedMedicine.BlockName = item.BlockName;
                    prescribedMedicine.PHCName = item.PHCName;
                    prescribedMedicine.Diagnostic = item.Diagnostic;
                    prescribedMedicine.NoOfTimePrescribed = item.NoOfTimePrescribed;
                    prescribedMedicinesList.Add(prescribedMedicine);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return prescribedMedicinesList;
        }

        public async Task<List<GetDashboardGraphVM>> GetDashboardGraph()
        {
            List<GetDashboardGraphVM> prescribedMedicinesList = new List<GetDashboardGraphVM>();
            GetDashboardGraphVM prescribedMedicine;
            try
            {
                var Results = _teleMedecineContext.GetDashboardGraph.FromSqlInterpolated($"EXEC [dbo].[GetDashboardGraph]");
                foreach (var item in Results)
                {
                    prescribedMedicine = new GetDashboardGraphVM();
                    prescribedMedicine.PatientCount = item.PatientCount;
                    prescribedMedicine.DistrictName = item.DistrictName;
                    prescribedMedicinesList.Add(prescribedMedicine);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return prescribedMedicinesList;
        }

        public async Task<List<GetDashboardFeedbackSummaryReportVM>> GetDashboardFeedbackSummaryReport()
        {
            List<GetDashboardFeedbackSummaryReportVM> prescribedMedicinesList = new List<GetDashboardFeedbackSummaryReportVM>();
            GetDashboardFeedbackSummaryReportVM prescribedMedicine;
            try
            {
                var Results = _teleMedecineContext.GetDashboardFeedbackSummaryReport.FromSqlInterpolated($"EXEC [dbo].[GetDashboardFeedbackSummaryReport]");
                foreach (var item in Results)
                {
                    prescribedMedicine = new GetDashboardFeedbackSummaryReportVM();
                    prescribedMedicine.Feedback = item.Feedback;
                    prescribedMedicine.FeedbackCount = item.FeedbackCount;
                    prescribedMedicinesList.Add(prescribedMedicine);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return prescribedMedicinesList;
        }
        public async Task<List<GetDashboardFeedbackReportVM>> GetDashboardFeedbackReport(DateTime? fromDate, DateTime? toDate)
        {
            List<GetDashboardFeedbackReportVM> prescribedMedicinesList = new List<GetDashboardFeedbackReportVM>();
            GetDashboardFeedbackReportVM prescribedMedicine;
            try
            {
                var Results = _teleMedecineContext.GetDashboardFeedbackReport.FromSqlInterpolated($"EXEC [dbo].[GetDashboardFeedbackReport] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    prescribedMedicine = new GetDashboardFeedbackReportVM();
                    prescribedMedicine.SrNo = item.SrNo;
                    prescribedMedicine.DistrictName = item.DistrictName;
                    prescribedMedicine.BlockName = item.BlockName;
                    prescribedMedicine.PHCName = item.PHCName;
                    prescribedMedicine.PatientName = item.PatientName;
                    prescribedMedicine.MobileNo = item.MobileNo;
                    prescribedMedicine.DoctorName = item.DoctorName;
                    prescribedMedicine.Feedback = item.Feedback;
                    prescribedMedicine.Comments = item.Comments;
                    prescribedMedicinesList.Add(prescribedMedicine);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return prescribedMedicinesList;
        }
        public async Task<List<GetDashboardDignosisSpecialityWiseVM>> GetDashboardDignosisSpecialityWise(DateTime? fromDate, DateTime? toDate, int Specialityid)
        {
            List<GetDashboardDignosisSpecialityWiseVM> prescribedMedicinesList = new List<GetDashboardDignosisSpecialityWiseVM>();
            GetDashboardDignosisSpecialityWiseVM prescribedMedicine;
            try
            {
                var Results = _teleMedecineContext.GetDashboardDignosisSpecialityWise.FromSqlInterpolated($"EXEC [dbo].[GetDashboardDignosisSpecialityWise] @FromDate ={fromDate}, @ToDate ={toDate}, @Specialityid={Specialityid}");
                foreach (var item in Results)
                {
                    prescribedMedicine = new GetDashboardDignosisSpecialityWiseVM();
                    prescribedMedicine.SrNo = item.SrNo;
                    prescribedMedicine.DistrictName = item.DistrictName;
                    prescribedMedicine.BlockName = item.BlockName;
                    prescribedMedicine.PHCName = item.PHCName;
                    prescribedMedicine.Disease = item.Disease;
                    prescribedMedicine.Occurrence = item.Occurrence;
                    prescribedMedicinesList.Add(prescribedMedicine);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return prescribedMedicinesList;
        }
        public async Task<List<GetDashboardDiseasephcWiseVM>> GetDashboardDiseasephcWise(DateTime? fromDate, DateTime? toDate)
        {
            List<GetDashboardDiseasephcWiseVM> prescribedMedicinesList = new List<GetDashboardDiseasephcWiseVM>();
            GetDashboardDiseasephcWiseVM prescribedMedicine;
            try
            {
                var Results = _teleMedecineContext.GetDashboardDiseasephcWise.FromSqlInterpolated($"EXEC [dbo].[GetDashboardDiseasephcWise] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    prescribedMedicine = new GetDashboardDiseasephcWiseVM();
                    prescribedMedicine.SrNo = item.SrNo;
                    prescribedMedicine.DistrictName = item.DistrictName;
                    prescribedMedicine.BlockName = item.BlockName;
                    prescribedMedicine.PHCName = item.PHCName;
                    prescribedMedicine.Disease = item.Disease;
                    prescribedMedicine.Occurrence = item.Occurrence;
                    prescribedMedicinesList.Add(prescribedMedicine);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return prescribedMedicinesList;
        }
        public async Task<List<GetDashboardDiseaseAgeWiseVM>> GetDashboardDiseaseAgeWise(DateTime? fromDate, DateTime? toDate)
        {
            List<GetDashboardDiseaseAgeWiseVM> prescribedMedicinesList = new List<GetDashboardDiseaseAgeWiseVM>();
            GetDashboardDiseaseAgeWiseVM prescribedMedicine;
            try
            {
                var Results = _teleMedecineContext.GetDashboardDiseaseAgeWise.FromSqlInterpolated($"EXEC [dbo].[GetDashboardDiseaseAgeWise] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    prescribedMedicine = new GetDashboardDiseaseAgeWiseVM();
                    prescribedMedicine.SrNo = item.SrNo;
                    prescribedMedicine.Age = item.Age;
                    prescribedMedicine.Disease = item.Disease;
                    prescribedMedicine.Occurrence = item.Occurrence;
                    prescribedMedicinesList.Add(prescribedMedicine);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return prescribedMedicinesList;
        }


        public async Task<List<GetDashboardSystemHealthReportVM>> GetDashboardSystemHealthReport(DateTime? fromDate, DateTime? toDate)
        {
            List<GetDashboardSystemHealthReportVM> prescribedMedicinesList = new List<GetDashboardSystemHealthReportVM>();
            GetDashboardSystemHealthReportVM data;
            try
            {
                var Results = _teleMedecineContext.GetDashboardSystemHealthReport.FromSqlInterpolated($"EXEC [dbo].[GetDashboardSystemHealthReport] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    data = new GetDashboardSystemHealthReportVM();
                    data.SrNo = item.SrNo;
                    data.date = item.date;
                    data.WorkingHours = item.WorkingHours;
                    data.WorkingTime = item.WorkingTime;
                    data.ServerUpTime = item.ServerUpTime;
                    data.ServerDownTime = item.ServerDownTime;
                    data.DownTimings = item.DownTimings;
                    data.Availability = item.Availability;
                    data.ServerDownTimeSS = item.ServerDownTimeSS;
                    data.ServerUpTimeSS = item.ServerUpTimeSS;
                    data.WorkingTimeSS = item.WorkingTimeSS;
                    prescribedMedicinesList.Add(data);
                }

            }
            catch (Exception ex)
            {
                throw;
            }


            return prescribedMedicinesList;
        }
        public async Task<List<RemoteSiteDowntimeSummaryDailyVM>> RemoteSiteDowntimeSummaryDaily(DateTime? fromDate, DateTime? toDate)
        {
            List<RemoteSiteDowntimeSummaryDailyVM> prescribedMedicinesList = new List<RemoteSiteDowntimeSummaryDailyVM>();
            RemoteSiteDowntimeSummaryDailyVM data;
            try
            {
                var Results = _teleMedecineContext.RemoteSiteDowntimeSummaryDaily.FromSqlInterpolated($"EXEC [dbo].[RemoteSiteDowntimeSummaryDaily] @FromDate ={fromDate}, @ToDate ={toDate}");
                foreach (var item in Results)
                {
                    data = new RemoteSiteDowntimeSummaryDailyVM();
                    data.SrNo = item.SrNo;
                    data.DistrictName = item.DistrictName;
                    data.BlockName = item.BlockName;
                    data.PHCName = item.PHCName;
                    data.TotalWorkingTime = item.TotalWorkingTime;
                    data.PHCDownTime = item.PHCDownTime;
                    data.DownTime = item.DownTime;
                    prescribedMedicinesList.Add(data);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return prescribedMedicinesList;
        }
        public async Task<List<RemoteSiteDowntimeSummaryMonthlyVM>> RemoteSiteDowntimeSummaryMonthly(int month, int year)
        {
            List<RemoteSiteDowntimeSummaryMonthlyVM> registerPatientReports = new List<RemoteSiteDowntimeSummaryMonthlyVM>();
            RemoteSiteDowntimeSummaryMonthlyVM data;
            try
            {
                var Results = _teleMedecineContext.RemoteSiteDowntimeSummaryMonthly.FromSqlInterpolated($"EXEC [dbo].[RemoteSiteDowntimeSummaryMonthly] @month ={month}, @year ={year}");
                foreach (var item in Results)
                {
                    data = new RemoteSiteDowntimeSummaryMonthlyVM();
                    data.SrNo = item.SrNo;
                    data.SrNo = item.SrNo;
                    data.DistrictName = item.DistrictName;
                    data.BlockName = item.BlockName;
                    data.PHCName = item.PHCName;
                    data.TotalWorkingTime = item.TotalWorkingTime;
                    data.PHCDownTime = item.PHCDownTime;
                    data.DownTime = item.DownTime;
                    registerPatientReports.Add(data);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return registerPatientReports;
        }

        public async Task<List<GetDashboardFeedbackSummaryReportDataVM>> GetDashboardFeedbackSummaryReportData()
        {
            List<GetDashboardFeedbackSummaryReportDataVM> listdata = new List<GetDashboardFeedbackSummaryReportDataVM>();
            GetDashboardFeedbackSummaryReportDataVM dataVM;
            try
            {
                var Results = _teleMedecineContext.GetDashboardFeedbackSummaryReportData.FromSqlInterpolated($"EXEC [dbo].[GetDashboardFeedbackSummaryReportData] ");
                foreach (var item in Results)
                {
                    dataVM = new GetDashboardFeedbackSummaryReportDataVM();
                    dataVM.SrNo = item.SrNo;
                    dataVM.DistrictName = item.DistrictName;
                    dataVM.BlockName = item.BlockName;
                    dataVM.PHCName = item.PHCName;
                    dataVM.PatientName = item.PatientName;
                    dataVM.MobileNo = item.MobileNo;
                    dataVM.DoctorName = item.DoctorName;
                    dataVM.Feedback = item.Feedback;
                    dataVM.Comments = item.Comments;
                    listdata.Add(dataVM);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return listdata;
        }
    }
}
