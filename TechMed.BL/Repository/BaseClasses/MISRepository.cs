using AutoMapper;
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
        public async Task<List<CompletedConsultationDTO>> CompletedConsultation(CompletedConsultationVM completedConsultationVM)
        {
            List<CompletedConsultationDTO> list = new List<CompletedConsultationDTO>();
            list = (from pc in _teleMedecineContext.PatientCases
                    join pm in _teleMedecineContext.PatientMasters on pc.PatientId equals pm.Id
                    join pq in _teleMedecineContext.PatientQueues on pc.Id equals pq.PatientCaseId
                    join pg in _teleMedecineContext.GenderMasters on pm.GenderId equals pg.Id
                    join ph in _teleMedecineContext.Phcmasters on pm.Phcid equals ph.Id
                    where pc.CreatedOn.Day >= (completedConsultationVM.fromDate.Day == null ? pc.CreatedOn.Day : completedConsultationVM.fromDate.Day)
                    && pc.CreatedOn.Month >= (completedConsultationVM.fromDate.Month == null ? pc.CreatedOn.Month : completedConsultationVM.fromDate.Month)
                    && pc.CreatedOn.Year >= (completedConsultationVM.fromDate.Year == null ? pc.CreatedOn.Year : completedConsultationVM.fromDate.Year)

                    && pc.CreatedOn.Day <= (completedConsultationVM.Todate.Day == null ? pc.CreatedOn.Day : completedConsultationVM.Todate.Day)
                    && pc.CreatedOn.Month <= (completedConsultationVM.Todate.Month == null ? pc.CreatedOn.Month : completedConsultationVM.Todate.Month)
                    && pc.CreatedOn.Year <= (completedConsultationVM.Todate.Year == null ? pc.CreatedOn.Year : completedConsultationVM.Todate.Year)


                    select new CompletedConsultationDTO
                    {
                        Age = 1,
                        Gender = pg.Gender,
                        PatientID = pm.Id,
                        PatientName = pm.FirstName + " " + pm.LastName,
                        PhoneNumber = pm.PhoneNumber,
                        ReferredbyPHCName = ph.Phcname
                    }).ToList();
            return list;
        }

    }
}
