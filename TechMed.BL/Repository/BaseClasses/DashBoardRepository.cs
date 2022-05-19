using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    && u.LastLoginAt.Value.Day == DateTime.Now.Day
                    && u.LastLoginAt.Value.Month == DateTime.Now.Month
                    && u.LastLoginAt.Value.Year == DateTime.Now.Year
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

                    CompletedConsultantReport.SLNo =item.SLNo;
                    CompletedConsultantReport.District = item.District;
                    CompletedConsultantReport.Block = item.Block;
                    CompletedConsultantReport.PHC = item.PHC;
                    CompletedConsultantReport.Total = item.Total;
                    CompletedConsultantReport.GeneralPractice = item.GeneralPractice;
                    CompletedConsultantReport.ObstetricsAndGyne = item.ObstetricsAndGyne;
                    CompletedConsultantReport.Pediatrics = item.Pediatrics;
                    dashboardConsultations.Add(CompletedConsultantReport);
                }
            }

            return dashboardConsultations;

        }

        public List<PHCLoginHistoryReportVM> GetPHCLoginHistoryReport(int PHCId, DateTime fromDate, DateTime toDate)
        {
            List<PHCLoginHistoryReportVM> phcLoginReports = new List<PHCLoginHistoryReportVM>();
            PHCLoginHistoryReportVM phcLoginHistoryReport;
            if (PHCId > 0)
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
                    phcLoginReports.Add(phcLoginHistoryReport);
                }
            }
            return phcLoginReports;
        }

        public List<PHCConsultationVM> GetPHCConsultationReport(int PHCId, DateTime? fromDate, DateTime? toDate)
        {
            List<PHCConsultationVM> phcconsultationReports = new List<PHCConsultationVM>();
            PHCConsultationVM phcconsultationReport;
            if (PHCId > 0)
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

                    phcconsultationReports.Add(phcconsultationReport);
                }
            }
            return phcconsultationReports;
        }

        #region Duplicate GetDashboardReportSummary
       /* public async Task<List<DashboardReportSummaryVM>> GetDashboardReportSummary(GetDashboardReportSummaryVM getDashboardReportSummaryVM)
        {
            List<DashboardReportSummaryVM> dashboardConsultations = new List<DashboardReportSummaryVM>();

            //int SrNo = 0;
            if (true)
            {
                DashboardReportSummaryVM CompletedConsultantReport;
                var Results =  _teleMedecineContext.GetDashboardReportSummary.FromSqlInterpolated($"EXEC [dbo].[GetDashboardReportSummary] @FromDate={getDashboardReportSummaryVM.FromDate},@ToDate={getDashboardReportSummaryVM.ToDate}");
                foreach (var item in Results)
                {
                    //SrNo = SrNo + 1;
                    CompletedConsultantReport = new DashboardReportSummaryVM();

                    CompletedConsultantReport.SLNo = item.SLNo;
                    CompletedConsultantReport.District = item.District;
                    CompletedConsultantReport.Block = item.Block;
                    CompletedConsultantReport.PHC = item.PHC;
                    CompletedConsultantReport.Total = item.Total;
                    CompletedConsultantReport.GeneralPractice = item.GeneralPractice;
                    CompletedConsultantReport.ObstetricsAndGyne = item.ObstetricsAndGyne;
                    CompletedConsultantReport.Pediatrics = item.Pediatrics;
                    dashboardConsultations.Add(CompletedConsultantReport);
                }
            }

            return dashboardConsultations;

        }*/
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
                    CompletedConsultantReport.GeneralPractice = item.GeneralPractice;
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
                    dashboardConsultations.Add(dashboardReportConsultationVM);
                }
            }

            return dashboardConsultations;

        }

    }
}
