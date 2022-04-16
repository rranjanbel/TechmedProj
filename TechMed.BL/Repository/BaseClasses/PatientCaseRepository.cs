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


namespace TechMed.BL.Repository.BaseClasses
{
    public class PatientCaseRepository : Repository<PatientCase>, IPatientCaseRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<SpecializationRepository> _logger;
        public PatientCaseRepository(ILogger<SpecializationRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<PatientCase> CreateAsync(PatientCase patientCase)
        {
            PatientCase patientcasenew = new PatientCase();
            patientcasenew = await Create(patientCase);
            return patientcasenew;
        }

        public Task<PatientCase> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PatientCase> GetByPHCUserID(int userId)
        {
            throw new NotImplementedException();
        }

        public long GetCaseFileNumber()
        {            Setting setting = new Setting();
            Int64 currentNo = 0;
            Int64 casefileSerNo = 0;
            casefileSerNo = _teleMedecineContext.Settings.Select(a => a.CaseFileNumber).FirstOrDefault();
            if (casefileSerNo > 0)
            {
                currentNo = casefileSerNo;
                setting = _teleMedecineContext.Settings.FirstOrDefault();
                if (setting != null)
                {
                    setting.CaseFileNumber = currentNo + 1;
                }
                try
                {
                    _teleMedecineContext.Entry(setting).State = EntityState.Modified;
                    var result = _teleMedecineContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    string strExcep = ex.Message;
                    throw;
                }
                return currentNo + 1;
            }
            return 0;

        }

        public async Task<PatientCaseVM> GetPatientCaseDetails(int PHCID, int PatientID)
        {
            PatientCaseVM patientCase = new PatientCaseVM();
            if (PHCID > 0 && PatientID > 0)
            {
                List<PatientCaseVitalsVM> vitals = new List<PatientCaseVitalsVM>();
                PatientCaseVitalsVM vitalvm;
                var vitalMasters = _teleMedecineContext.PatientCaseVitals
                    .Include(a => a.Vital)
                    .Include(p => p.PatientCase)
                    .Where(a => a.PatientCase.PatientId == PatientID).ToList();
                foreach (var vital in vitalMasters)
                {
                    vitalvm = new PatientCaseVitalsVM();
                    vitalvm.PatientCaseId = vital.PatientCaseId;
                    vitalvm.VitalId = vital.VitalId;
                    vitalvm.Value = vital.Value;
                    vitalvm.VitalName = vital.Vital.Vital;
                    vitalvm.Date = vital.Date;
                    vitalvm.Id = vital.Id;
                    vitals.Add(vitalvm);
                }

                var phcresult = await (from pm in _teleMedecineContext.PatientMasters
                                       where pm.Id == PatientID && pm.Phcid == PHCID
                                       join pc in _teleMedecineContext.PatientCases on pm.Id equals pc.PatientId into pcase
                                       from pcdet in pcase.DefaultIfEmpty()
                                       join pd in _teleMedecineContext.PatientCaseDocuments on pcdet.Id equals pd.PatientCaseId into pdoc
                                       from pcdoc in pdoc.DefaultIfEmpty()
                                       select new PatientCaseVM
                                       {
                                           PatientID = pm.Id,
                                           PHCId = PHCID,
                                           PHCUserId = pm.CreatedBy,
                                           patientMaster = _mapper.Map<PatientMasterDTO>(pm),
                                           patientCase = _mapper.Map<PatientCaseDTO>(pcdet),
                                           vitals = vitals,
                                           caseDocuments = _mapper.Map<PatientCaseDocDTO>(pcdoc)

                                       }).FirstOrDefaultAsync();

                patientCase = (PatientCaseVM)phcresult;
            }
          
            return patientCase;
        }

        public async Task<PatientCaseWithDoctorVM> GetPatientQueueDetails(int PHCID, int PatientID)
        {
            PatientCaseWithDoctorVM patientCaseQueue = new PatientCaseWithDoctorVM();
            //if (PHCID > 0 && PatientID > 0)
            //{              

            //    var phcresult = await (from pm in _teleMedecineContext.PatientMasters
            //                           where pm.Id == PatientID && pm.Phcid == PHCID
            //                           join pc in _teleMedecineContext.PatientCases on pm.Id equals pc.PatientId into pcase
            //                           from pcdet in pcase.DefaultIfEmpty()
            //                           join pd in _teleMedecineContext.PatientQueues on pcdet.Id equals pd.PatientCaseId into pdoc
            //                           from pcdoc in pdoc.DefaultIfEmpty()
            //                           select new PatientCaseVM
            //                           {
            //                               PatientID = pm.Id,
            //                               PHCId = PHCID,
            //                               PHCUserId = pm.CreatedBy,
            //                               patientMaster = _mapper.Map<PatientMasterDTO>(pm),
            //                               patientCase = _mapper.Map<PatientCaseDTO>(pcdet),
            //                               vitals = vitals,
            //                               caseDocuments = _mapper.Map<PatientCaseDocDTO>(pcdoc)

            //                           }).FirstOrDefaultAsync();

            //    patientCase = (PatientCaseVM)phcresult;
            //}

            return patientCaseQueue;
        }

        public bool IsPatientCaseExist(PatientCaseCreateVM patientCase)
        {
           // We will dicide and complete the function later
            if(patientCase == null)
            {
                return true;
            }
            //       _teleMedecineContext.PatientCases.Any( a => a.PatientId == patientCase.PatientID && a.CreatedOn)
            return false;
            
        }
    }
}
