﻿using AutoMapper;
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
        private readonly ILogger<PatientCaseRepository> _logger;
        public PatientCaseRepository(ILogger<PatientCaseRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
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
                    vitalvm.Id = vital.Vital.Id;
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

        public async Task<PatientCaseDetailsVM> PostPatientCaseDetails(PatientCaseDetailsVM patientCaseVM)
        {
            PatientCaseDetailsVM patientcasecreateVM = new PatientCaseDetailsVM();
            PatientCase patientCase ;
            PatientCaseVital patientCaseVital;
            PatientCaseDocument patientCaseDocument;
            int i = 0;
            int j = 0;
            int k = 0;
            int l = 0;
            if(patientCaseVM != null)
            {
                if(patientCaseVM.PatientID != 0)
                {
                    if(patientCaseVM.patientCase != null)
                    {
                        try
                        {
                            if (patientCaseVM.patientCase.ID > 0)
                            {
                                patientCase = _teleMedecineContext.PatientCases.FirstOrDefault(a => a.Id == patientCaseVM.patientCase.ID);
                                patientCase.UpdatedBy = patientCaseVM.patientCase.UpdatedBy;
                                patientCase.UpdatedOn = DateTime.Now;
                                patientCase.PatientId = patientCaseVM.patientCase.PatientId;
                                patientCase.Allergies = patientCaseVM.patientCase.Allergies;
                                patientCase.CaseFileNumber = patientCaseVM.patientCase.CaseFileNumber;
                                patientCase.Test = patientCaseVM.patientCase.Test;
                                patientCase.Instruction = patientCaseVM.patientCase.Instruction;
                                patientCase.CaseHeading = patientCaseVM.patientCase.CaseHeading;
                                patientCase.Symptom = patientCaseVM.patientCase.Symptom;
                                patientCase.Prescription = "";
                                patientCase.Observation = patientCaseVM.patientCase.Observation;
                                patientCase.FamilyHistory = patientCaseVM.patientCase.FamilyHistory;
                                patientCase.Diagnosis = patientCaseVM.patientCase.Diagnosis;

                                this._teleMedecineContext.Entry(patientCase).State = EntityState.Modified;
                                i = await this.Context.SaveChangesAsync();
                                patientcasecreateVM.patientCase = patientCaseVM.patientCase;
                                if (i > 0)
                                {
                                    _logger.LogInformation($"update Patient case : sucessfully {patientCase.Id}");
                                }


                            }
                            else
                            {
                                patientCase = new PatientCase();
                                patientCase.Id = 0;
                                patientCase.PatientId = patientCaseVM.patientCase.PatientId;
                                patientCase.Allergies = patientCaseVM.patientCase.Allergies;
                                patientCase.CaseFileNumber = patientCaseVM.patientCase.CaseFileNumber;
                                patientCase.Test = patientCaseVM.patientCase.Test;
                                patientCase.Instruction = patientCaseVM.patientCase.Instruction;
                                patientCase.CaseHeading = patientCaseVM.patientCase.CaseHeading;
                                patientCase.Symptom = patientCaseVM.patientCase.Symptom;
                                patientCase.Prescription = "";
                                patientCase.Observation = patientCaseVM.patientCase.Observation;
                                patientCase.FamilyHistory = patientCaseVM.patientCase.FamilyHistory;
                                patientCase.Diagnosis = patientCaseVM.patientCase.Diagnosis;
                                patientCase.UpdatedBy = patientCaseVM.patientCase.UpdatedBy;
                                patientCase.UpdatedOn = DateTime.Now;
                                patientCase.CreatedBy = patientCaseVM.patientCase.CreatedBy;
                                patientCase.CreatedOn = DateTime.Now;

                                this._teleMedecineContext.Entry(patientCase).State = EntityState.Added;
                                j = await this.Context.SaveChangesAsync();
                                patientcasecreateVM.patientCase = _mapper.Map<PatientCaseDTO>(patientCase);
                                if (j > 0)
                                {
                                    _logger.LogInformation($"Patient case added : sucessfully {patientCase.Id}");
                                }
                            }

                            if (i > 0 || j > 0)
                            {

                                foreach (var vital in patientCaseVM.vitals)
                                {
                                    patientCaseVital = new PatientCaseVital();
                                    patientCaseVital.Date = DateTime.Now;
                                    patientCaseVital.PatientCaseId = vital.PatientCaseId;
                                    patientCaseVital.VitalId = vital.VitalId;
                                    patientCaseVital.Value = vital.Value;
                                    this._teleMedecineContext.Entry(patientCaseVital).State = EntityState.Added;
                                    k = await this.Context.SaveChangesAsync();
                                    if (k > 0)
                                    {
                                        _logger.LogInformation($"Patient vital added : sucessfully {patientCase.Id}");
                                    }
                                }

                                patientcasecreateVM.vitals = patientCaseVM.vitals;

                                patientCaseDocument = new PatientCaseDocument();
                                patientCaseDocument = _mapper.Map<PatientCaseDocument>(patientCaseVM.caseDocuments);
                                this._teleMedecineContext.Entry(patientCaseDocument).State = EntityState.Added;
                                l = await this.Context.SaveChangesAsync();
                                if (l > 0)
                                {
                                    _logger.LogInformation($"Patient case document added : sucessfully {patientCase.Id}");
                                }
                                patientcasecreateVM.caseDocuments = _mapper.Map<PatientCaseDocDTO>(patientCaseDocument);

                                patientcasecreateVM.PatientID = patientCaseVM.PatientID;
                                patientcasecreateVM.PHCUserId = patientCaseVM.PHCUserId;
                                patientcasecreateVM.PHCId = patientCaseVM.PHCId;
                                patientcasecreateVM.PatientID = patientCaseVM.PatientID;


                            }
                        }
                        catch (Exception ex)
                        {
                            string expMesg = ex.Message;
                            _logger.LogInformation($"Exception when update and add patient case, vital and report doc {ex.Message}");

                        }
                       
                        return patientcasecreateVM;
                    }
                    else
                    {
                        // Patient case is null
                        return patientcasecreateVM;
                    }
                }
                else
                {
                    //Patient Id is null
                    return patientcasecreateVM;
                }
            }
            else
            {
                // patient case model is null
                return patientcasecreateVM;
            }
        }
    }
}
