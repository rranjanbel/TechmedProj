using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        {
            Setting setting = new Setting();
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
            try
            {
                
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
                    patientCase.patientMaster.Age = UtilityMaster.GetAgeOfPatient(_teleMedecineContext.PatientMasters.FirstOrDefault(p => p.Id == PatientID && p.Phcid == PHCID).Dob);
                }

            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
                
            }          

            return patientCase;
        }

        public async Task<PatientCaseWithDoctorVM> GetPatientQueueDetails(int PHCID, int PatientID)
        {
            PatientCaseWithDoctorVM patientCaseQueue = new PatientCaseWithDoctorVM();
            List<PatientCaseMedicineDTO> patientCaseMedicineDTOs = new List<PatientCaseMedicineDTO>();
            List<PatientCaseMedicine> patientCaseMedicines = new List<PatientCaseMedicine>();
            PatientCaseMedicineDTO patientCaseMedicine;
            try
            {
                if (PHCID > 0 && PatientID > 0)
                {

                    var patientCaseQueueresult = await (from pm in _teleMedecineContext.PatientMasters
                                                        where pm.Id == PatientID && pm.Phcid == PHCID
                                                        join phc in _teleMedecineContext.Phcmasters on pm.Phcid equals phc.Id
                                                        //join um in _teleMedecineContext.UserMasters on pm.CreatedBy equals um.Id
                                                        join pc in _teleMedecineContext.PatientCases on pm.Id equals pc.PatientId into pcase
                                                        from pcdet in pcase.DefaultIfEmpty()
                                                        join pq in _teleMedecineContext.PatientQueues on pcdet.Id equals pq.PatientCaseId into pque
                                                        from pques in pque.DefaultIfEmpty()
                                                        join dm in _teleMedecineContext.DoctorMasters on pques.AssignedDoctorId equals dm.Id into doc
                                                        from doct in doc.DefaultIfEmpty()
                                                        join du in _teleMedecineContext.UserMasters on doct.UserId equals du.Id into user 
                                                        from usr in user.DefaultIfEmpty()
                                                        join ud in _teleMedecineContext.UserDetails on usr.Id equals ud.UserId into udet
                                                        from userdet in udet.DefaultIfEmpty()
                                                        join cs in _teleMedecineContext.CaseFileStatusMasters on pques.CaseFileStatusId equals cs.Id into cfs
                                                        from cfsm in cfs.DefaultIfEmpty()
                                                        join sp in _teleMedecineContext.SpecializationMasters on doct.SpecializationId equals sp.Id into spm
                                                        from sepm in spm.DefaultIfEmpty()
                                                        //join pcm in _teleMedecineContext.PatientCaseMedicines on pcdet.Id equals pcm.PatientCaseId into pcmed
                                                        //from pcmedicine in pcmed.DefaultIfEmpty()
                                                        select new PatientCaseWithDoctorVM
                                                        {
                                                            PatientID = pm.Id,
                                                            PHCId = PHCID,
                                                            PHCUserId = phc.UserId,
                                                            DoctorID = doct.UserId,
                                                            PatientCaseID = pcdet.Id,
                                                            patientMaster = _mapper.Map<PatientMasterDTO>(pm),
                                                            patientCase = _mapper.Map<PatientCaseDTO>(pcdet),
                                                            patientCaseQueueVM = new PatientCaseQueueVM
                                                            {
                                                                PatientCaseID = pcdet.Id,
                                                                DoctorID = pques.AssignedDoctorId,
                                                                DocterName = userdet.FirstName + " " + userdet.LastName,
                                                                CaseFileStatusID = pques.CaseFileStatusId,
                                                                CaseStatus = cfsm.FileStatus,
                                                                AssignedBy = pques.AssignedBy,
                                                                AssigneeName = phc.Phcname,
                                                                AssignedOn = pques.AssignedOn,
                                                                Qualification = doct.Qualification,
                                                                Specialization = sepm.Specialization,
                                                                StatusOn = pques.StatusOn,
                                                                PhoneNo = doct.PhoneNumber,
                                                                DrImagePath = userdet.Photo
                                                            },
                                                            patientCaseMedicines = patientCaseMedicineDTOs

                                                        }).FirstOrDefaultAsync();

                    patientCaseQueue = (PatientCaseWithDoctorVM)patientCaseQueueresult;

                    patientCaseQueue.patientMaster.Age = UtilityMaster.GetAgeOfPatient(_teleMedecineContext.PatientMasters.FirstOrDefault(p => p.Id == PatientID && p.Phcid == PHCID).Dob);

                    patientCaseMedicines = _teleMedecineContext.PatientCaseMedicines.Where(a => a.PatientCaseId == patientCaseQueue.PatientCaseID).ToList();
                    foreach (var item in patientCaseMedicines)
                    {
                        patientCaseMedicine = new PatientCaseMedicineDTO();
                        patientCaseMedicine = _mapper.Map<PatientCaseMedicineDTO>(item);
                        patientCaseMedicineDTOs.Add(patientCaseMedicine);
                    }
                    patientCaseQueue.patientCaseMedicines = patientCaseMedicineDTOs;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                //throw;
            }


            return patientCaseQueue;
        }

        public bool IsPatientCaseExist(PatientCaseCreateVM patientCase)
        {
            // We will dicide and complete the function later
            if (patientCase == null)
            {
                return true;
            }
            //       _teleMedecineContext.PatientCases.Any( a => a.PatientId == patientCase.PatientID && a.CreatedOn)
            return false;

        }

        public async Task<PatientCaseDetailsVM> PostPatientCaseDetails(PatientCaseDetailsVM patientCaseVM)
        {
            List<PatientCaseDocDTO> caseDocumentsList = new List<PatientCaseDocDTO>();
            PatientCaseDetailsVM patientcasecreateVM = new PatientCaseDetailsVM();
            PatientCase patientCase;
            PatientCaseVital patientCaseVital;
            //PatientCaseDocument patientCaseDocument;
            int i = 0;
            int j = 0;
            int k = 0;
            int l = 0;
            if (patientCaseVM != null)
            {
                if (patientCaseVM.PatientID != 0)
                {
                    if (patientCaseVM.patientCase != null)
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
                                patientCase.Test = "";
                                patientCase.Instruction = patientCaseVM.patientCase.Instruction;
                                patientCase.CaseHeading = patientCaseVM.patientCase.CaseHeading;
                                patientCase.Symptom = patientCaseVM.patientCase.Symptom;
                                patientCase.Prescription = "";
                                patientCase.Observation = patientCaseVM.patientCase.Observation;
                                patientCase.FamilyHistory = patientCaseVM.patientCase.FamilyHistory;
                                patientCase.SuggestedDiagnosis = patientCaseVM.patientCase.SuggestedDiagnosis;
                                patientCase.ProvisionalDiagnosis = patientCaseVM.patientCase.ProvisionalDiagnosis;
                                patientCase.ReferredTo = patientCaseVM.patientCase.ReferredTo;
                               

                                this._teleMedecineContext.Entry(patientCase).State = EntityState.Modified;
                                i = await this.Context.SaveChangesAsync();
                                patientcasecreateVM.patientCase = patientCaseVM.patientCase;
                                if (i > 0)
                                {
                                    _logger.LogInformation($"update Patient case : sucessfully {patientCase.Id}");
                                }


                            }
                            //else
                            //{
                            //    patientCase = new PatientCase();
                            //    patientCase.Id = 0;
                            //    patientCase.PatientId = patientCaseVM.patientCase.PatientId;
                            //    patientCase.Allergies = patientCaseVM.patientCase.Allergies;
                            //    patientCase.CaseFileNumber = patientCaseVM.patientCase.CaseFileNumber;
                            //    patientCase.Test = patientCaseVM.patientCase.Test;
                            //    patientCase.Instruction = patientCaseVM.patientCase.Instruction;
                            //    patientCase.CaseHeading = patientCaseVM.patientCase.CaseHeading;
                            //    patientCase.Symptom = patientCaseVM.patientCase.Symptom;
                            //    patientCase.Prescription = "";
                            //    patientCase.Observation = patientCaseVM.patientCase.Observation;
                            //    patientCase.FamilyHistory = patientCaseVM.patientCase.FamilyHistory;
                            //    patientCase.SuggestedDiagnosis = patientCaseVM.patientCase.SuggestedDiagnosis;                               
                            //    patientCase.ProvisionalDiagnosis = patientCaseVM.patientCase.ProvisionalDiagnosis;
                            //    patientCase.ReferredTo = patientCaseVM.patientCase.ReferredTo;
                            //    patientCase.Opdno = patientCaseVM.patientCase.Opdno;
                            //    patientCase.UpdatedBy = patientCaseVM.patientCase.UpdatedBy;
                            //    patientCase.UpdatedOn = DateTime.Now;
                            //    patientCase.CreatedBy = patientCaseVM.patientCase.CreatedBy;
                            //    patientCase.CreatedOn = DateTime.Now;

                            //    this._teleMedecineContext.Entry(patientCase).State = EntityState.Added;
                            //    j = await this.Context.SaveChangesAsync();
                            //    patientcasecreateVM.patientCase = _mapper.Map<PatientCaseDTO>(patientCase);
                            //    if (j > 0)
                            //    {
                            //        _logger.LogInformation($"Patient case added : sucessfully {patientCase.Id}");
                            //    }
                            //}

                            if (i > 0 || j > 0)
                            {

                                foreach (var vital in patientCaseVM.vitals)
                                {
                                    k = 0;
                                    patientCaseVital = new PatientCaseVital();
                                    patientCaseVital.Date = DateTime.Now;
                                    patientCaseVital.PatientCaseId = vital.PatientCaseId;
                                    patientCaseVital.VitalId = vital.VitalId;
                                    patientCaseVital.Value = vital.Value;
                                    this._teleMedecineContext.Entry(patientCaseVital).State = EntityState.Added;
                                    k = await this.Context.SaveChangesAsync();
                                    //if (k > 0)
                                    //{
                                    //    _logger.LogInformation($"Patient vital added : sucessfully {patientCase.Id}");
                                    //}
                                }

                                patientcasecreateVM.vitals = patientCaseVM.vitals;
                                //foreach (var doc in patientCaseVM.caseDocuments)
                                //{
                                //    l = 0;
                                //    patientCaseDocument = new PatientCaseDocument();
                                //    patientCaseDocument = _mapper.Map<PatientCaseDocument>(doc);
                                //    this._teleMedecineContext.Entry(patientCaseDocument).State = EntityState.Added;
                                //    l = await this.Context.SaveChangesAsync();

                                //    PatientCaseDocDTO docDTO = _mapper.Map<PatientCaseDocDTO>(patientCaseDocument);
                                //    caseDocumentsList.Add(docDTO);
                                //}
                                //if (l > 0)
                                //{
                                //    _logger.LogInformation($"Patient case document added : sucessfully {patientCase.Id}");
                                //}
                                //patientcasecreateVM.caseDocuments = caseDocumentsList;

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

        public async Task<PatientFeedbackDTO> PostPatientFeedBack(PatientFeedbackDTO patientFeedback)
        {
            PatientFeedbackDTO updatedFeedback = new PatientFeedbackDTO();
            PatientCaseFeedback feedback = new PatientCaseFeedback();
            if (patientFeedback != null)
            {
                bool isPatientCaseInSystem = await IsPatientCaseExist(patientFeedback.PatientCaseId);

                if (isPatientCaseInSystem)
                {
                    feedback = _mapper.Map<PatientCaseFeedback>(patientFeedback);
                    feedback.Question = "NA";
                    feedback.Datetime = DateTime.Now;
                    _teleMedecineContext.PatientCaseFeedbacks.Add(feedback);
                    int i = _teleMedecineContext.SaveChanges();
                    if (i > 0)
                    {
                        updatedFeedback = _mapper.Map<PatientFeedbackDTO>(feedback);
                        return updatedFeedback;
                    }
                    else
                    {
                        return updatedFeedback;
                    }

                }
                else
                {
                    return updatedFeedback;
                }
            }
            else
            {
                return updatedFeedback;
            }
        }

        public async Task<PatientReferToDoctorVM> PostPatientReferToDoctor(PatientReferToDoctorVM patientReferToDoctorVM)
        {
            PatientReferToDoctorVM outPatientReferToDoctorVM = new PatientReferToDoctorVM();
            PatientQueue patientQueue = new PatientQueue();
            string message = string.Empty;
            try
            {
                if (patientReferToDoctorVM != null)
                {
                    patientQueue.PatientCaseId = patientReferToDoctorVM.PatientCaseID;
                    patientQueue.AssignedDoctorId = patientReferToDoctorVM.AssignedDocterID;
                    patientQueue.AssignedBy = patientReferToDoctorVM.PHCID;
                    patientQueue.CaseFileStatusId = await GetCaseFileStatus();
                    patientQueue.StatusOn = DateTime.Now;
                    patientQueue.Comment = "Assigned by PHC";
                    patientQueue.AssignedOn = DateTime.Now;

                    _teleMedecineContext.PatientQueues.Add(patientQueue);
                    int i = _teleMedecineContext.SaveChanges();

                    if (i > 0 && patientQueue.Id > 0)
                    {
                        outPatientReferToDoctorVM.AssignedDocterID = patientQueue.AssignedDoctorId;
                        outPatientReferToDoctorVM.PatientCaseID = patientQueue.PatientCaseId;
                        outPatientReferToDoctorVM.PHCID = patientQueue.AssignedBy;

                        PatientCaseQueDetail patientCaseQue = GetPatientInfo(patientQueue.PatientCaseId);
                        message = patientCaseQue.PHCName + "  PHC center has an updated information for Patient Name :" + patientCaseQue.PatientName + "(" + patientCaseQue.PatientID + ")";
                       // message = "PHC center has an updated information for Patient case ID : (" + patientQueue.PatientCaseId + ")";
                        Notification notification = new Notification();
                        notification.ToUser = patientCaseQue.DoctorUserID;
                        notification.FromUser = patientQueue.AssignedBy;
                        notification.Message = message;
                        notification.CreatedOn = patientQueue.AssignedOn;
                        notification.SeenOn = patientQueue.AssignedOn;
                        notification.IsSeen = false;
                        _teleMedecineContext.Notifications.Add(notification);
                        int j = _teleMedecineContext.SaveChanges();

                        return outPatientReferToDoctorVM;
                    }
                    else
                        return outPatientReferToDoctorVM;

                }
                else
                    return outPatientReferToDoctorVM;
            }
            catch (Exception ex)
            {
                string messageExp = ex.Message;
                throw;
            }
           
        }

        private async Task<int> GetCaseFileStatus()
        {
            int caseFileStatus = 0;
            caseFileStatus = await _teleMedecineContext.CaseFileStatusMasters.Where(a => a.FileStatus.Contains("Queued")).Select(a => a.Id).FirstOrDefaultAsync();          
            return caseFileStatus;
        }

        private async Task<bool> IsPatientCaseExist(long patientCaseID)
        {
            bool isPatientCaseCreated = false;
            isPatientCaseCreated = await _teleMedecineContext.PatientCases.AnyAsync(a => a.Id == patientCaseID);
            return isPatientCaseCreated;
        }

        public PatientCaseQueDetail GetPatientInfo(long patientCaseID)
        {
            PatientCaseQueDetail patientCaseQue = new PatientCaseQueDetail();
            //patientCaseQue = _teleMedecineContext.PatientCaseQueDetails.FromSqlRaw("PatientCaseQueDetails", patientCaseID).FirstOrDefault();
            var Results = _teleMedecineContext.PatientCaseQueDetails.FromSqlInterpolated($"EXEC [dbo].[GetPatientCaseDetails] @PatientCaseID ={patientCaseID}");
            foreach (var item in Results)
            {
                patientCaseQue.PHCName = item.PHCName;
                patientCaseQue.PatientName = item.PatientName;
                patientCaseQue.Docter = item.Docter;
                patientCaseQue.PHCID =item.PHCID;
                patientCaseQue.AssignedOn = item.AssignedOn;
                patientCaseQue.Cluster = item.Cluster;
                patientCaseQue.BlockName = item.BlockName;
                patientCaseQue.PatientID = item.PatientID;
                patientCaseQue.PatientQueueId = item.PatientQueueId;
                patientCaseQue.PatientCaseID = item.PatientCaseID;
                patientCaseQue.PatientCreatedBy = item.PatientCreatedBy;
                patientCaseQue.DoctorUserID = item.DoctorUserID;
            }

            return patientCaseQue;
        }

        public string SaveDocument(IFormFile file, string rootPath, string uniqeFileName)
        {
            try
            {
                string contentRootPath = rootPath;
                string path = @"\\MyStaticFiles\\CaseDocuments\\";
                path = contentRootPath + path;

                //Generate unique filename  

                var filePath = Path.Combine(path, uniqeFileName);


                var fileType = Path.GetExtension(file.FileName);

                //if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
                //{
                //    //var filePath = Path.Combine(path, file.FileName);

                //}

                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }
                return file.FileName;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return "";
            }
            


            

        }

        public bool SaveCaseDocument(List<CaseDocumentVM> caseDocuments, string contentRootPath)
        {
            PatientCaseDocument patientCaseDocument  ;       
            int l = 0;           
            string path = @"/MyFiles/CaseDocuments/";
            

           
            if (caseDocuments != null)
            {              
                foreach (var doc in caseDocuments)
                {
                    if(doc.file.Length >0)
                    {
                        l = 0;
                        var myfilename = string.Format(@"{0}", Guid.NewGuid());
                        myfilename = myfilename +"_"+ doc.file.FileName;
                        string fileName = SaveDocument(doc.file, contentRootPath, myfilename);
                        if(fileName != null)
                        {
                            string fullPath = Path.Combine(path, myfilename);
                            patientCaseDocument = new PatientCaseDocument();
                            patientCaseDocument.PatientCaseId = doc.patientCaseId;
                            patientCaseDocument.DocumentPath = fullPath;
                            patientCaseDocument.DocumentName = doc.name;
                            patientCaseDocument.Description = doc.file.FileName + " , " + doc.file.Length;
                            patientCaseDocument.DocumentTypeId = doc.DocumentTypeId;


                            this._teleMedecineContext.Entry(patientCaseDocument).State = EntityState.Added;

                            if(doc.DocumentTypeId == 2)
                            {
                                PatientCase patientCase = this._teleMedecineContext.PatientCases.FirstOrDefault(a => a.Id == doc.patientCaseId);
                                patientCase.Prescription = fullPath;
                                this._teleMedecineContext.Entry(patientCase).State = EntityState.Modified;

                            }
                            l = this.Context.SaveChanges();
                        }
                                          
                    }
                   
                }
                if (l > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            else
            {
                return false;
            }

        }

        public async Task<PatientCaseVM> GetPatientCaseDetailsByCaseID(int PatientCaseID)
        {
            PatientCaseVM patientCase = new PatientCaseVM();
            try
            {

                if (PatientCaseID > 0)
                {
                    List<PatientCaseVitalsVM> vitals = new List<PatientCaseVitalsVM>();
                    PatientCaseMedicineDTO pcaseMedicine;                   
                    List<PatientCaseMedicineDTO> pcaseMedicineList = new List<PatientCaseMedicineDTO>();
                    PatientCaseDiagnosisTestsVM patientCaseDiagnosis = new PatientCaseDiagnosisTestsVM();
                    List<PatientCaseDiagnosisTestsVM> patientCaseDiagnosisList = new List<PatientCaseDiagnosisTestsVM>();
                    PatientCaseVitalsVM vitalvm;
                    var patientCaseDetails = _teleMedecineContext.PatientCases.Include(a => a.Patient).ThenInclude(a => a.Phc).Include(a => a.PatientCaseDocuments).Include(a => a.PatientCaseVitals).ThenInclude(a => a.Vital).FirstOrDefault(a => a.Id == PatientCaseID);
                    var patientQuue = _teleMedecineContext.PatientQueues.Include(a => a.AssignedDoctor).ThenInclude(s => s.User).ThenInclude(c => c.UserDetailUsers).Include(f => f.AssignedDoctor.Specialization).FirstOrDefault(x => x.PatientCaseId == PatientCaseID);
                    var patientCaseMedicine = await _teleMedecineContext.PatientCases.Include(a => a.PatientCaseMedicines).ThenInclude(d => d.DrugMaster).Where(x => x.Id == PatientCaseID).ToListAsync();
                    var patientCaseDiagonisis = await _teleMedecineContext.PatientCases.Include(b => b.PatientCaseDiagonosticTests).ThenInclude(e => e.DiagonosticTest).Where(x => x.Id == PatientCaseID).ToListAsync();



                    if (patientCaseDetails == null)
                    {
                        return null;
                    }

                    if(patientCaseMedicine != null)
                    {
                        foreach (var item in patientCaseMedicine)
                        {
                            foreach (var med in item.PatientCaseMedicines.ToList())
                            {
                                pcaseMedicine = new PatientCaseMedicineDTO();
                                pcaseMedicine.Id = med.Id;
                                pcaseMedicine.PatientCaseId = med.PatientCaseId;
                                pcaseMedicine.DrugMasterID = med.DrugMasterId;
                                pcaseMedicine.DrugName = med.DrugMaster.NameOfDrug;
                                pcaseMedicine.DrugFormAndVolume = med.DrugMaster.DrugformAndStrength;
                                pcaseMedicine.Morning = med.Morning;
                                pcaseMedicine.Night = med.Night;
                                pcaseMedicine.AfterMeal = med.AfterMeal;
                                pcaseMedicine.EmptyStomach = med.EmptyStomach;
                                pcaseMedicine.Od = med.Od;
                                pcaseMedicine.Bd = med.Bd;
                                pcaseMedicine.Td = med.Td;
                                pcaseMedicineList.Add(pcaseMedicine);
                            }                         

                        }

                        patientCase.caseMedicineList = pcaseMedicineList;
                       

                    }
                    if (patientCaseDiagonisis != null)
                    {
                        foreach (var item in patientCaseDiagonisis)
                        {
                            foreach (var diagno in item.PatientCaseDiagonosticTests.ToList())
                            {
                                patientCaseDiagnosis = new PatientCaseDiagnosisTestsVM();
                                patientCaseDiagnosis.Id = Convert.ToInt32(diagno.Id);
                                patientCaseDiagnosis.PatientCaseID = diagno.PatientCaseId;
                                patientCaseDiagnosis.DiagonosticTestID = diagno.DiagonosticTestId;
                                patientCaseDiagnosis.CreatedOn = diagno.CreatedOn;
                                patientCaseDiagnosisList.Add(patientCaseDiagnosis);
                            }
                        }
                        patientCase.caseDiagnosisTestList = patientCaseDiagnosisList;
                    }

                    patientCase.PatientID = patientCaseDetails.Patient.Id;
                    patientCase.PHCId = patientCaseDetails.Patient.Phc.Id;
                    patientCase.PHCUserId = patientCaseDetails.Patient.CreatedBy;
                    patientCase.PHCName = patientCaseDetails.Patient.Phc.Phcname;
                    patientCase.PHCMoname = patientCaseDetails.Patient.Phc.Moname;
                    patientCase.patientMaster = _mapper.Map<PatientMasterDTO>(patientCaseDetails.Patient);
                    patientCase.patientCase = _mapper.Map<PatientCaseDTO>(patientCaseDetails);
                    if(patientQuue != null)
                    {
                        patientCase.DoctorName = patientQuue.AssignedDoctor.User.Name;
                        patientCase.DoctorMobileNo = patientQuue.AssignedDoctor.PhoneNumber;
                        patientCase.CaseFileStatusID = patientQuue.CaseFileStatusId;
                       patientCase.DoctorSpecialization = patientQuue.AssignedDoctor.Specialization.Specialization;
                    }
                    else
                    {
                        patientCase.DoctorName = String.Empty;
                        patientCase.DoctorMobileNo = String.Empty;
                        patientCase.DoctorSpecialization = String.Empty;
                    }
                    

                    patientCase.vitals = patientCaseDetails.PatientCaseVitals.Select(vitals => new PatientCaseVitalsVM()
                    {
                        PatientCaseId = vitals.PatientCaseId,
                        VitalId = vitals.VitalId,
                        Value = vitals.Value,
                        VitalName = vitals.Vital.Vital,
                        Date = vitals.Date,
                        Id = vitals.Vital.Id
                    }).ToList();
                    patientCase.caseDocumentList = patientCaseDetails.PatientCaseDocuments.Select(doc => new PatientCaseDocDTO()
                    {
                        DocumentName = doc.DocumentName,
                        DocumentPath = doc.DocumentPath,
                        Description = doc.Description,
                        Id = doc.Id,
                        PatientCaseId = doc.PatientCaseId
                    }).ToList();
                  
                    patientCase.patientMaster.Age = UtilityMaster.GetAgeOfPatient(patientCase.patientMaster.Dob);

                }

            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;

            }

            return patientCase;
        }

        public async Task<PatientCaseLevelDTO> GetPatientCaseLevels(int patientID)
        {
            PatientCaseLevelDTO patientCaseLevel = new PatientCaseLevelDTO();
            List<GetCaseLabelDTO> getCaseLabelDTOs = new List<GetCaseLabelDTO>();
            var patientCaseLevels = await _teleMedecineContext.PatientCases.Where(a => a.PatientId == patientID).ToListAsync();

            foreach (var item in patientCaseLevels)
            {
                getCaseLabelDTOs.Add(new GetCaseLabelDTO
                {
                    CaseDateTime = item.CreatedOn,
                    CaseLabel = item.CaseHeading,
                    CaseID = item.Id
                });
            }

            patientCaseLevel.caseLabelDTOs = getCaseLabelDTOs;
            var patientMaster = _teleMedecineContext.PatientMasters.FirstOrDefault(a => a.Id == patientID);
            patientCaseLevel.patientMaster = _mapper.Map<PatientMasterDTO>(patientMaster) ;
            patientCaseLevel.patientMaster.Age = UtilityMaster.GetAgeOfPatient(patientCaseLevel.patientMaster.Dob);

            return patientCaseLevel;

        }

        public async Task<List<PatientCaseDocDTO>> GetPatientCaseDocList(int PatientCaseID,string rootUrl)
        {
            PatientCaseDocDTO patientCaseDoc;
            List<PatientCaseDocDTO> patientCaseDocs = new List<PatientCaseDocDTO>();
            var resultDocList = await _teleMedecineContext.PatientCaseDocuments.Where(a => a.PatientCaseId == PatientCaseID).ToListAsync();
            foreach (var item in resultDocList)
            {
               
                patientCaseDoc = new PatientCaseDocDTO();
                patientCaseDoc = _mapper.Map<PatientCaseDocDTO>(item);
                //string absolutePath = rootUrl + patientCaseDoc.DocumentPath;
                //patientCaseDoc.DocumentPath = absolutePath;
                patientCaseDocs.Add(patientCaseDoc);

            }
            return patientCaseDocs;
        }
    }

}