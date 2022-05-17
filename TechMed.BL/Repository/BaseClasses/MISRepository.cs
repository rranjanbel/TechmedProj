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
    public class MISRepository : IMISRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;
        public MISRepository(ILogger<UserRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;
        }
        public async Task<List<CompletedConsultantVM>> CompletedConsultation(CompletedPatientSearchVM completedConsultationSearch)
        {
            bool IsPHCExit = await IsPHCExists(completedConsultationSearch.PHCID);
            List<CompletedConsultantVM> CompletedConsultantReports = new List<CompletedConsultantVM>();
            int SrNo = 0;
            if(IsPHCExit)
            {
                CompletedConsultantVM CompletedConsultantReport;
                var Results = _teleMedecineContext.CompletedConsultant.FromSqlInterpolated($"EXEC [dbo].[GetCompletedConsultant] @PHCID={completedConsultationSearch.PHCID},@FromDate={completedConsultationSearch.FromDate},@ToDate={completedConsultationSearch.ToDate}");
                foreach (var item in Results)
                {
                    SrNo = SrNo + 1;
                    CompletedConsultantReport = new CompletedConsultantVM();
                    CompletedConsultantReport.SrNo = SrNo;
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
                    CompletedConsultantReports.Add(CompletedConsultantReport);
                }
            }
           
            return CompletedConsultantReports;
            //List<CompletedConsultationDTO> list = new List<CompletedConsultationDTO>();
            //list = (from pc in _teleMedecineContext.PatientCases
            //        join pm in _teleMedecineContext.PatientMasters on pc.PatientId equals pm.Id
            //        join pq in _teleMedecineContext.PatientQueues on pc.Id equals pq.PatientCaseId
            //        join pg in _teleMedecineContext.GenderMasters on pm.GenderId equals pg.Id
            //        join ph in _teleMedecineContext.Phcmasters on pm.Phcid equals ph.Id
            //        where pc.CreatedOn.Day >= (completedConsultationVM.fromDate.Day == null ? pc.CreatedOn.Day : completedConsultationVM.fromDate.Day)
            //        && pc.CreatedOn.Month >= (completedConsultationVM.fromDate.Month == null ? pc.CreatedOn.Month : completedConsultationVM.fromDate.Month)
            //        && pc.CreatedOn.Year >= (completedConsultationVM.fromDate.Year == null ? pc.CreatedOn.Year : completedConsultationVM.fromDate.Year)

            //        && pc.CreatedOn.Day <= (completedConsultationVM.Todate.Day == null ? pc.CreatedOn.Day : completedConsultationVM.Todate.Day)
            //        && pc.CreatedOn.Month <= (completedConsultationVM.Todate.Month == null ? pc.CreatedOn.Month : completedConsultationVM.Todate.Month)
            //        && pc.CreatedOn.Year <= (completedConsultationVM.Todate.Year == null ? pc.CreatedOn.Year : completedConsultationVM.Todate.Year)


            //        select new CompletedConsultationDTO
            //        {
            //            Age = 1,
            //            Gender = pg.Gender,
            //            PatientID = pm.Id,
            //            PatientName = pm.FirstName + " " + pm.LastName,
            //            PhoneNumber = pm.PhoneNumber,
            //            ReferredbyPHCName = ph.Phcname
            //        }).ToList();
            //return list;
        }

        public List<ConsultedPatientByDoctorAndPHCVM> CompletedConsultationByDoctor(SearchDateVM searchField)
        {
            List<ConsultedPatientByDoctorAndPHCVM> CompletedConsultantReports = new List<ConsultedPatientByDoctorAndPHCVM>();          
            if (searchField !=null)
            {
                ConsultedPatientByDoctorAndPHCVM CompletedConsultantReport;
                var Results = _teleMedecineContext.ConsultedPatientByDoctorAndPHCResults.FromSqlInterpolated($"EXEC [dbo].[GetConsultationByDoctorAndPHC] @FromDate={searchField.FromDate},@ToDate={searchField.ToDate}");
                foreach (var item in Results)
                {
                    CompletedConsultantReport = new ConsultedPatientByDoctorAndPHCVM();
                    CompletedConsultantReport.SrNo = item.SrNo;
                    CompletedConsultantReport.NoOfConsultations = item.NoOfConsultations;
                    CompletedConsultantReport.Doctor = item.Doctor;
                    CompletedConsultantReport.PHCName = item.PHCName;                   
                    CompletedConsultantReports.Add(CompletedConsultantReport);
                }
            }

            return CompletedConsultantReports;
        }

        public List<CompletedConsultationChartVM> CompletedConsultationChart(int year)
        {
            List<CompletedConsultationChartVM> CompletedConsultantReports = new List<CompletedConsultationChartVM>();
            //if (year > 0)
            //{
                CompletedConsultationChartVM CompletedConsultantReport;
                var Results = _teleMedecineContext.CompletedConsultationChartResults.FromSqlInterpolated($"EXEC [dbo].[GetCompletedConsultationChart] @Year={year}");
                foreach (var item in Results)
                {
                    CompletedConsultantReport = new CompletedConsultationChartVM();
                    CompletedConsultantReport.NoOfConsultations = item.NoOfConsultations;
                    CompletedConsultantReport.Month = item.Month;
                    CompletedConsultantReport.Year = item.Year;                   
                    CompletedConsultantReports.Add(CompletedConsultantReport);
                }
            //}

            return CompletedConsultantReports;
        }

        public async Task<bool> IsPHCExists(int PHCID)
        {
            var result = await _teleMedecineContext.Phcmasters.AnyAsync(a => a.Id == PHCID);
            return result;
        }

    }
}
