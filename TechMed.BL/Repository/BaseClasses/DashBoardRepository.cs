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
                    d.ZoneId == (doctorsLoggedInTodayVM.ZoneID==null ? d.ZoneId : doctorsLoggedInTodayVM.ZoneID)
                    && d.ClusterId == (doctorsLoggedInTodayVM.ClusterID== null ? d.ClusterId : doctorsLoggedInTodayVM.ClusterID)
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
                        ZoneId = d.ZoneId,
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
            return specializationReports ;
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

     
    }
}
