using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        //private readonly IDoctorRepository _doctorRepository;
        private readonly SMSSetting _smsSettings;
        private readonly IUserRepository _userRepository;
        public PatientCaseRepository(IUserRepository userRepository, ILogger<PatientCaseRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper, IOptions<SMSSetting> smsSettings) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;
            //this._doctorRepository = doctorRepository;
            this._smsSettings = smsSettings.Value;
            _userRepository = userRepository;
        }

        public async Task<PatientCase> CreateAsync(PatientCase patientCase)
        {
            PatientCase patientcasenew = new PatientCase();
            patientcasenew = await Create(patientCase);
            return patientcasenew;
        }

        public async Task<PatientCase> GetByID(long id)
        {
            PatientCase patientCase = new PatientCase();
            patientCase = await _teleMedecineContext.PatientCases.FirstOrDefaultAsync(a => a.Id == id);
            return patientCase;
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

                                           join pc in _teleMedecineContext.PatientCases on pm.Id equals pc.PatientId into pcase
                                           from pcdet in pcase.DefaultIfEmpty()
                                           join pd in _teleMedecineContext.PatientCaseDocuments on pcdet.Id equals pd.PatientCaseId into pdoc
                                           from pcdoc in pdoc.DefaultIfEmpty()
                                           where pm.Id == PatientID && pcdet.CreatedBy == PHCID
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
                                                        where pm.Id == PatientID && pcdet.CreatedBy == PHCID
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
                                patientCase.UpdatedOn = UtilityMaster.GetLocalDateTime();
                                patientCase.PatientId = patientCaseVM.patientCase.PatientId;
                                patientCase.Allergies = patientCaseVM.patientCase.Allergies;
                                patientCase.CaseFileNumber = patientCaseVM.patientCase.CaseFileNumber;
                                patientCase.Comment = patientCaseVM.patientCase.Comment;
                                patientCase.Instruction = patientCaseVM.patientCase.Instruction;
                                patientCase.CaseHeading = patientCaseVM.patientCase.CaseHeading;
                                patientCase.Symptom = patientCaseVM.patientCase.Symptom;
                                patientCase.Prescription = "";
                                patientCase.Observation = patientCaseVM.patientCase.Observation;
                                patientCase.FamilyHistory = patientCaseVM.patientCase.FamilyHistory;
                                patientCase.SuggestedDiagnosis = patientCaseVM.patientCase.SuggestedDiagnosis;
                                patientCase.ProvisionalDiagnosis = patientCaseVM.patientCase.ProvisionalDiagnosis;
                                patientCase.ReferredTo = patientCaseVM.patientCase.ReferredTo;
                                patientCase.CaseStatusID = 2;


                                this._teleMedecineContext.Entry(patientCase).State = EntityState.Modified;
                                i = await this.Context.SaveChangesAsync();
                                patientcasecreateVM.patientCase = patientCaseVM.patientCase;
                                if (i > 0)
                                {
                                    _logger.LogInformation($"update Patient case : sucessfully {patientCase.Id}");
                                }


                            }


                            if (i > 0)
                            {
                                if (patientCaseVM.vitals.Count > 0)
                                {
                                    foreach (var vital in patientCaseVM.vitals)
                                    {
                                        k = 0;
                                        patientCaseVital = new PatientCaseVital();
                                        patientCaseVital.Date = UtilityMaster.GetLocalDateTime();
                                        patientCaseVital.PatientCaseId = vital.PatientCaseId;
                                        patientCaseVital.VitalId = vital.VitalId;
                                        patientCaseVital.Value = vital.Value;
                                        this._teleMedecineContext.Entry(patientCaseVital).State = EntityState.Added;
                                        k = await this.Context.SaveChangesAsync();
                                        if (k > 0)
                                        {
                                            _logger.LogInformation($"Patient vital added : sucessfully");
                                        }

                                    }


                                    patientcasecreateVM.vitals = patientCaseVM.vitals;

                                }
                                patientcasecreateVM.PatientID = patientCaseVM.PatientID;
                                patientcasecreateVM.PHCUserId = patientCaseVM.PHCUserId;
                                patientcasecreateVM.PHCId = patientCaseVM.PHCId;
                                patientcasecreateVM.PatientID = patientCaseVM.PatientID;


                            }
                        }
                        catch (Exception ex)
                        {
                            string expMesg = ex.Message;
                            _logger.LogError($"Exception when update and add patient case, vital and report doc" + ex);
                            throw;

                        }

                        return patientcasecreateVM;
                    }
                    else
                    {
                        // Patient case is null
                        _logger.LogError($"Patient case is null");
                        return patientcasecreateVM;
                    }
                }
                else
                {
                    //Patient Id is null
                    _logger.LogError($"Patient Id is null");
                    return patientcasecreateVM;
                }
            }
            else
            {
                // patient case model is null
                _logger.LogError($"patient case model is null");
                return patientcasecreateVM;
            }
        }

        public async Task<PatientFeedbackDTO> PostPatientFeedBack(PatientFeedbackDTO patientFeedback)
        {
            PatientFeedbackDTO updatedFeedback = new PatientFeedbackDTO();
            PatientCaseFeedback feedback = new PatientCaseFeedback();
            string message = string.Empty;
            string mobileNumber = string.Empty;
            if (patientFeedback != null)
            {
                bool isPatientCaseInSystem = await IsPatientCaseExist(patientFeedback.PatientCaseId);

                if (isPatientCaseInSystem)
                {
                    feedback = _mapper.Map<PatientCaseFeedback>(patientFeedback);
                    feedback.Question = "NA";
                    feedback.Datetime = UtilityMaster.GetLocalDateTime();
                    _teleMedecineContext.PatientCaseFeedbacks.Add(feedback);
                    int i = _teleMedecineContext.SaveChanges();
                    if (i > 0)
                    {
                        _logger.LogInformation("Feedback received from pateint case id :" + patientFeedback.PatientCaseId);
                        updatedFeedback = _mapper.Map<PatientFeedbackDTO>(feedback);
                        //Send SMS Line Item 168

                        //var patientinfo = _teleMedecineContext.PatientCases.Include(s => s.Patient).FirstOrDefault(a => a.Id == patientFeedback.PatientCaseId);
                        //if(patientinfo != null)
                        //{
                        //    message =  "Hi "+patientinfo.Patient.FirstName +" "+ _smsSettings.message;
                        //    mobileNumber = patientinfo.Patient.MobileNo;
                        //    bool response = UtilityMaster.SendSMS(mobileNumber, message, _smsSettings.apikey, _smsSettings.sender, _smsSettings.url);
                        //    if(response)
                        //    {
                        //        _logger.LogInformation("SMS sent to patient Id :" + patientinfo.Patient.Id);
                        //    }
                        //    else
                        //    {
                        //        _logger.LogError("SMS did not send to patient Id :" + patientinfo.Patient.Id); 
                        //    }

                        //}

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
            int autoAssignDoctorID = 0;
            int i = 0;
            int loggedUserId = 0;
            int caseCreatedBy = 0;
            DateTime? callInitiatedTime = UtilityMaster.GetLocalDateTime();
            DateTime currentTime = UtilityMaster.GetLocalDateTime();
            caseCreatedBy = _teleMedecineContext.PatientCases.Where(a => a.Id == patientReferToDoctorVM.PatientCaseID).Select(s => s.CreatedBy).FirstOrDefault();

            try
            {
                //if(token !=null || token !="")
                //{
                //    LoginHistory loginHistory = _teleMedecineContext.LoginHistories.FirstOrDefault(a => a.UserToken == token);
                //    caseCreatedBy = _teleMedecineContext.PatientCases.Where(a => a.Id == patientReferToDoctorVM.PatientCaseID).Select(s => s.CreatedBy).FirstOrDefault(); ;
                //    if (loginHistory.UserId != caseCreatedBy && loginHistory.UserTypeId == 3)
                //    {
                //        outPatientReferToDoctorVM.AssignedDocterID = 0;
                //        outPatientReferToDoctorVM.PatientCaseID = patientReferToDoctorVM.PatientCaseID;
                //        outPatientReferToDoctorVM.PHCID = patientReferToDoctorVM.PHCID;
                //        outPatientReferToDoctorVM.Status = "Fail";
                //        outPatientReferToDoctorVM.Message = "Patient case created on other PHC";
                //        return outPatientReferToDoctorVM;
                //    }

                //}


                if (patientReferToDoctorVM != null)
                {
                    //int Busydoctors = _teleMedecineContext.TwilioMeetingRoomInfos
                    //                .Where(a => a.IsClosed == false && a.TwilioRoomStatus == "in-progress" && a.AssignedDoctorId == patientReferToDoctorVM.AssignedDocterID).Count();

                    TwilioMeetingRoomInfo videoCallStatus = _teleMedecineContext.TwilioMeetingRoomInfos
                                    .Where(a => a.IsClosed == false && a.TwilioRoomStatus == "in-progress" && a.AssignedDoctorId == patientReferToDoctorVM.AssignedDocterID).OrderByDescending(x => x.CloseDate).FirstOrDefault();

                    // TwilioMeetingRoomInfo videoCallStatus = await _teleMedecineContext.TwilioMeetingRoomInfos.Include(d => d.AssignedDoctor)
                    //.Where(a => a.IsClosed == false && a.TwilioRoomStatus == "in-progress" && a.AssignedDoctorId == patientReferToDoctorVM.AssignedDocterID && a.AssignedDoctor.IsOnline == true).ToListAsync();

                    if (videoCallStatus != null)
                    {
                        //TwilioMeetingRoomInfo twilioMeeting = videoCallStatus.OrderByDescending(a => a.CloseDate).FirstOrDefault();
                        callInitiatedTime = videoCallStatus != null ? videoCallStatus.CloseDate : UtilityMaster.GetLocalDateTime();
                        double diffInMin = UtilityMaster.TimeDifferenceInMin(callInitiatedTime.Value, currentTime);

                        if (diffInMin < 30)
                        {
                            outPatientReferToDoctorVM.AssignedDocterID = 0;
                            outPatientReferToDoctorVM.PatientCaseID = patientReferToDoctorVM.PatientCaseID;
                            outPatientReferToDoctorVM.PHCID = patientReferToDoctorVM.PHCID;
                            outPatientReferToDoctorVM.Status = "Fail";
                            outPatientReferToDoctorVM.Message = "Doctor is busy, please call other Doctor !";
                            return outPatientReferToDoctorVM;
                        }
                    }


                    if (patientReferToDoctorVM.AssignedDocterID > 0)
                    {
                        patientQueue.AssignedDoctorId = patientReferToDoctorVM.AssignedDocterID;
                        patientQueue.Comment = "Manually assign doctor";
                    }
                    else
                    {
                        autoAssignDoctorID = AutoAssignDoctor(patientReferToDoctorVM.PatientCaseID);
                        if (autoAssignDoctorID > 0)
                        {
                            patientQueue.AssignedDoctorId = autoAssignDoctorID;
                            patientQueue.Comment = "Auto assign doctor";
                        }
                        else
                        {
                            outPatientReferToDoctorVM.AssignedDocterID = 0;
                            outPatientReferToDoctorVM.PatientCaseID = patientQueue.PatientCaseId;
                            outPatientReferToDoctorVM.PHCID = patientQueue.AssignedBy;
                            outPatientReferToDoctorVM.Status = "Fail";
                            outPatientReferToDoctorVM.Message = "Assigned doctor is not avialble !";
                            return outPatientReferToDoctorVM;
                        }

                    }
                    patientQueue.PatientCaseId = patientReferToDoctorVM.PatientCaseID;
                    patientQueue.AssignedBy = caseCreatedBy;
                    //patientQueue.AssignedBy = _teleMedecineContext.PatientCases.Where(a => a.Id == patientReferToDoctorVM.PatientCaseID).Select(s => s.CreatedBy).FirstOrDefault();
                    patientQueue.CaseFileStatusId = await GetCaseFileStatus();
                    patientQueue.StatusOn = UtilityMaster.GetLocalDateTime();
                    patientQueue.AssignedOn = UtilityMaster.GetLocalDateTime();
                    patientQueue.UpdatedOn = UtilityMaster.GetLocalDateTime();
                    patientQueue.IsQueueChanged = false;

                    //patientQueue.AssignedDoctorId = patientReferToDoctorVM.AssignedDocterID;
                    //patientQueue.Comment = "Assigned by PHC";

                    PatientQueue existingpatientQueue = _teleMedecineContext.PatientQueues.FirstOrDefault(a => a.PatientCaseId == patientReferToDoctorVM.PatientCaseID && a.CaseFileStatusId != 5);
                    if (existingpatientQueue == null)
                    {
                        //_teleMedecineContext.PatientQueues.Add(patientQueue);
                        _teleMedecineContext.Entry(patientQueue).State = EntityState.Added;

                    }
                    else
                    {
                        existingpatientQueue.AssignedDoctorId = patientQueue.AssignedDoctorId;
                        //existingpatientQueue.StatusOn = UtilityMaster.GetLocalDateTime();
                        existingpatientQueue.AssignedOn = UtilityMaster.GetLocalDateTime();
                        existingpatientQueue.AssignedBy = caseCreatedBy;
                        existingpatientQueue.CaseFileStatusId = await GetCaseFileStatus();
                        existingpatientQueue.Comment = "Reassign the doctor";
                        existingpatientQueue.UpdatedOn = UtilityMaster.GetLocalDateTime();
                        existingpatientQueue.IsQueueChanged = false;
                        _teleMedecineContext.Entry(existingpatientQueue).State = EntityState.Modified;
                    }

                    PatientCase patientCase = _teleMedecineContext.PatientCases.FirstOrDefault(a => a.Id == patientReferToDoctorVM.PatientCaseID);
                    if (patientCase != null)
                    {
                        patientCase.CaseStatusID = 3;
                        _teleMedecineContext.Entry(patientCase).State = EntityState.Modified;
                    }
                    i = _teleMedecineContext.SaveChanges();


                    if (i > 0)
                    {
                        outPatientReferToDoctorVM.AssignedDocterID = patientQueue.AssignedDoctorId;
                        outPatientReferToDoctorVM.PatientCaseID = patientQueue.PatientCaseId;
                        outPatientReferToDoctorVM.PHCID = patientQueue.AssignedBy;
                        outPatientReferToDoctorVM.Status = "Success";
                        outPatientReferToDoctorVM.Message = "Assigned to doctor sucessfully !";
                        try
                        {
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
                        }
                        catch (Exception ex)
                        {
                            outPatientReferToDoctorVM.Status = "Success";
                            outPatientReferToDoctorVM.Message = "Assigned to doctor sucessfully ! but notification did not add";
                            _logger.LogError("Exception generate when going to add notification for patient case Id :" + patientQueue.PatientCaseId, ex);
                        }


                        return outPatientReferToDoctorVM;
                    }
                    else
                    {
                        outPatientReferToDoctorVM.AssignedDocterID = 0;
                        outPatientReferToDoctorVM.PatientCaseID = patientQueue.PatientCaseId;
                        outPatientReferToDoctorVM.PHCID = patientQueue.AssignedBy;
                        outPatientReferToDoctorVM.Status = "Fail";
                        outPatientReferToDoctorVM.Message = "Error: When assigned to doctor!";
                        return outPatientReferToDoctorVM;
                    }


                }
                else
                {
                    outPatientReferToDoctorVM.AssignedDocterID = 0;
                    outPatientReferToDoctorVM.PatientCaseID = patientQueue.PatientCaseId;
                    outPatientReferToDoctorVM.PHCID = patientQueue.AssignedBy;
                    outPatientReferToDoctorVM.Status = "Fail";
                    outPatientReferToDoctorVM.Message = "Error: View model has no data!";
                    return outPatientReferToDoctorVM;
                }
            }
            catch (Exception ex)
            {
                string messageExp = ex.Message;
                _logger.LogError("Exception generate when going to assign doctor for patient case Id :" + patientQueue.PatientCaseId, ex);
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
                patientCaseQue.PHCID = item.PHCID;
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

        //public string SaveDocument(IFormFile file, string rootPath, string uniqeFileName)
        //{
        //    try
        //    {
        //        string contentRootPath = rootPath;
        //        string path = @"\\MyStaticFiles\\CaseDocuments\\";
        //        path = contentRootPath + path;

        //        //Generate unique filename  

        //        var filePath = Path.Combine(path, uniqeFileName);


        //        var fileType = Path.GetExtension(file.FileName);

        //        //if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
        //        //{
        //        //    //var filePath = Path.Combine(path, file.FileName);

        //        //}

        //        using (Stream stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            file.CopyToAsync(stream);
        //        }
        //        return file.FileName;
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = ex.Message;
        //        return "";
        //    }





        //}

        //public bool SaveCaseDocument(List<CaseDocumentVM> caseDocuments, string contentRootPath)
        //{
        //    PatientCaseDocument patientCaseDocument  ;       
        //    int l = 0;           
        //    string path = @"/MyFiles/CaseDocuments/";



        //    if (caseDocuments != null)
        //    {              
        //        foreach (var doc in caseDocuments)
        //        {
        //            if(doc.file.Length >0)
        //            {
        //                l = 0;
        //                var myfilename = string.Format(@"{0}", Guid.NewGuid());
        //                myfilename = myfilename +"_"+ doc.file.FileName;
        //                string fileName = SaveDocument(doc.file, contentRootPath, myfilename);
        //                if(fileName != null)
        //                {
        //                    string fullPath = Path.Combine(path, myfilename);
        //                    patientCaseDocument = new PatientCaseDocument();
        //                    patientCaseDocument.PatientCaseId = doc.patientCaseId;
        //                    patientCaseDocument.DocumentPath = fullPath;
        //                    patientCaseDocument.DocumentName = doc.name;
        //                    patientCaseDocument.Description = doc.file.FileName + " , " + doc.file.Length;
        //                    patientCaseDocument.DocumentTypeId = doc.DocumentTypeId;


        //                    this._teleMedecineContext.Entry(patientCaseDocument).State = EntityState.Added;

        //                    if(doc.DocumentTypeId == 2)
        //                    {
        //                        PatientCase patientCase = this._teleMedecineContext.PatientCases.FirstOrDefault(a => a.Id == doc.patientCaseId);
        //                        patientCase.Prescription = fullPath;
        //                        this._teleMedecineContext.Entry(patientCase).State = EntityState.Modified;

        //                    }
        //                    l = this.Context.SaveChanges();
        //                }

        //            }

        //        }
        //        if (l > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }


        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}
        public async Task<PatientCaseVM> GetPatientCaseDetailsByCaseID(Int64 PatientCaseID, string contentRootPath)
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
                    var IsVideoCallClosed = _teleMedecineContext.TwilioMeetingRoomInfos.Where(a => a.PatientCaseId == PatientCaseID).OrderByDescending(x => x.Id).Take(1);
                    var patientDetails = _teleMedecineContext.PatientCases.Include(p => p.Patient.Phc.Block).Include(p => p.Patient).ThenInclude(s => s.State).ThenInclude(d => d.DistrictMasters).FirstOrDefault(a => a.Id == PatientCaseID);

                    if (patientCaseDetails == null)
                    {
                        return null;
                    }

                    if (patientCaseMedicine != null)
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
                                pcaseMedicine.DrugformAndStrength = med.DrugMaster.DrugformAndStrength;
                                pcaseMedicine.Morning = med.Morning;
                                pcaseMedicine.Night = med.Night;
                                pcaseMedicine.AfterMeal = med.AfterMeal;
                                pcaseMedicine.EmptyStomach = med.EmptyStomach;
                                pcaseMedicine.Od = med.Od;
                                pcaseMedicine.Bd = med.Bd;
                                pcaseMedicine.Td = med.Td;
                                pcaseMedicine.Qid = med.Qid;
                                pcaseMedicine.Duration = med.Duration;
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
                                patientCaseDiagnosis.DiagonosticTestName = diagno.DiagonosticTest.Name;
                                patientCaseDiagnosisList.Add(patientCaseDiagnosis);
                            }
                        }
                        patientCase.caseDiagnosisTestList = patientCaseDiagnosisList;
                    }
                    if (IsVideoCallClosed != null)
                    {
                        //IsVideoCallClosed = IsVideoCallClosed !=null? IsVideoCallClosed.FirstOrDefault();
                        foreach (var item in IsVideoCallClosed)
                        {
                            patientCase.VideoCallStatus = item.TwilioRoomStatus != null ? item.TwilioRoomStatus : "status not avialble";
                        }

                    }
                    else
                    {
                        patientCase.VideoCallStatus = "data not found";
                    }

                    patientCase.PatientID = patientCaseDetails.Patient.Id;
                    patientCase.PHCId = patientCaseDetails.Patient.Phc.Id;
                    patientCase.PHCUserId = patientCaseDetails.Patient.CreatedBy;
                    patientCase.PHCName = patientCaseDetails.Patient.Phc.Phcname;
                    patientCase.PHCMoname = patientCaseDetails.Patient.Phc.Moname;
                    patientCase.patientMaster = _mapper.Map<PatientMasterDTO>(patientCaseDetails.Patient);
                    patientCase.patientCase = _mapper.Map<PatientCaseDTO>(patientCaseDetails);
                    if (patientDetails != null)
                    {
                        patientCase.patientMaster.District = patientDetails.Patient.District.DistrictName;
                        patientCase.patientMaster.State = patientDetails.Patient.State.StateName;
                        patientCase.patientMaster.Block = patientDetails.Patient.Phc.Block.BlockName;
                    }
                    if (patientQuue != null)
                    {
                        patientCase.DoctorName = patientQuue.AssignedDoctor.User.Name;
                        patientCase.DoctorMobileNo = patientQuue.AssignedDoctor.PhoneNumber;
                        patientCase.CaseFileStatusID = patientQuue.CaseFileStatusId;
                        patientCase.DoctorSpecialization = patientQuue.AssignedDoctor.Specialization.Specialization;
                        patientCase.DoctorMCINo = patientQuue.AssignedDoctor.Mciid;
                        patientCase.DoctorQalification = patientQuue.AssignedDoctor.Qualification;
                        patientCase.ReviewDate = patientCase.patientCase.ReviewDate;
                        string relativePath = patientQuue.AssignedDoctor.DigitalSignature;
                        try
                        {
                            patientCase.DoctorSignature = UtilityMaster.DownloadFile(relativePath, contentRootPath, Path.GetFileName(patientQuue.AssignedDoctor.DigitalSignature));
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Exception in GetPatientCaseDetailsByCaseID=> UtilityMaster.DownloadFile function: " + ex);
                        }
                    }
                    else
                    {
                        patientCase.DoctorName = String.Empty;
                        patientCase.DoctorMobileNo = String.Empty;
                        patientCase.DoctorSpecialization = String.Empty;
                        patientCase.CaseFileStatusID = 0;
                    }


                    patientCase.vitals = patientCaseDetails.PatientCaseVitals.Select(vitals => new PatientCaseVitalsVM()
                    {
                        PatientCaseId = vitals.PatientCaseId,
                        VitalId = vitals.VitalId,
                        Value = vitals.Value,
                        VitalName = vitals.Vital.Vital,
                        Date = vitals.Date,
                        Unit = vitals.Vital.Unit,
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
                _logger.LogError("Exception in GetPatientCaseDetailsByCaseID function: " + ex);
                throw;

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
            patientCaseLevel.patientMaster = _mapper.Map<PatientMasterDTO>(patientMaster);
            patientCaseLevel.patientMaster.Age = UtilityMaster.GetAgeOfPatient(patientCaseLevel.patientMaster.Dob);

            return patientCaseLevel;

        }

        public async Task<List<PatientCaseDocDTO>> GetPatientCaseDocList(int PatientCaseID, string rootUrl)
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

        public bool UploadCaseDoc(List<CaseDocumentVM> caseDocuments, string contentRootPath)
        {
            PatientCaseDocument patientCaseDocument;
            int l = 0;
            string path = @"/MyFiles/CaseDocuments/";

            string relativePathSaveFile = @"\\MyStaticFiles\\CaseDocuments\\";

            if (caseDocuments != null)
            {
                foreach (var doc in caseDocuments)
                {
                    if (doc.file.Length > 0)
                    {
                        string saveFilename = String.Empty;
                        try
                        {

                            var fileType = Path.GetExtension(doc.file.FileName);
                            //Convert to base64
                            string base64Value = UtilityMaster.ConvertToBase64(doc.file);
                            //Save File in disk
                            saveFilename = UtilityMaster.SaveFileFromBase64(base64Value, contentRootPath, relativePathSaveFile, fileType.ToLower(), doc.DocumentTypeId);

                        }
                        catch (Exception)
                        {

                            throw;
                        }

                        //Save file in database
                        l = 0;

                        if (saveFilename != null)
                        {
                            string fullPath = Path.Combine(path, saveFilename);
                            patientCaseDocument = new PatientCaseDocument();
                            patientCaseDocument.PatientCaseId = doc.patientCaseId;
                            patientCaseDocument.DocumentPath = fullPath;
                            patientCaseDocument.DocumentName = doc.name;
                            patientCaseDocument.Description = doc.file.FileName + " , " + doc.file.Length;
                            patientCaseDocument.DocumentTypeId = doc.DocumentTypeId;

                            this._teleMedecineContext.Entry(patientCaseDocument).State = EntityState.Added;

                            // update case file for prescription document
                            if (doc.DocumentTypeId == 2)
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
        public bool UploadCaseDocFromByte(List<CaseDocumentBase64VM> caseDocuments, string contentRootPath)
        {
            PatientCaseDocument patientCaseDocument;
            int l = 0;
            string path = @"/MyFiles/CaseDocuments/";

            string relativePathSaveFile = @"\\MyStaticFiles\\CaseDocuments\\";

            if (caseDocuments != null)
            {
                foreach (var doc in caseDocuments)
                {
                    if (doc.file.Length > 0)
                    {
                        string saveFilename = String.Empty;
                        try
                        {

                            var fileType = ".pdf";
                            //Convert to base64
                            string base64Value = doc.file;
                            //Save File in disk
                            saveFilename = UtilityMaster.SaveFileFromBase64(base64Value, contentRootPath, relativePathSaveFile, fileType.ToLower(), doc.DocumentTypeId);

                        }
                        catch (Exception)
                        {

                            throw;
                        }

                        //Save file in database
                        l = 0;

                        if (saveFilename != null)
                        {
                            string fullPath = Path.Combine(path, saveFilename);
                            patientCaseDocument = new PatientCaseDocument();
                            patientCaseDocument.PatientCaseId = doc.patientCaseId;
                            patientCaseDocument.DocumentPath = fullPath;
                            patientCaseDocument.DocumentName = doc.name;
                            patientCaseDocument.Description = saveFilename + " , " + doc.file.Length;
                            patientCaseDocument.DocumentTypeId = doc.DocumentTypeId;

                            this._teleMedecineContext.Entry(patientCaseDocument).State = EntityState.Added;

                            // update case file for prescription document
                            if (doc.DocumentTypeId == 2)
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

        public int AutoAssignDoctor(long PatientCaseID)
        {
            int doctorId = 0;
            int[] drIDs;
            List<DoctorQueues> doctorQueues = new List<DoctorQueues>();
            DoctorQueues doctorque = new DoctorQueues();

            //1. Get online doctorlist
            int specializationID = _teleMedecineContext.PatientCases.Where(p => p.Id == PatientCaseID).Select(s => s.SpecializationId).FirstOrDefault();
            drIDs = _teleMedecineContext.DoctorMasters.Where(a => a.IsOnline == true && a.SpecializationId == specializationID).Select(d => d.Id).ToArray();

            //2. Get Doctor id who has minimum patient in Queue
            if (drIDs.Length > 0)
            {
                if (drIDs.Length > 1)
                {
                    var queuePattients = _teleMedecineContext.PatientQueues.Where(a => drIDs.Contains(a.AssignedDoctorId) && a.CaseFileStatusId == 4).ToList();
                    // var queuePatients = _teleMedecineContext.DoctorMasters.Include(p => p.PatientQueues).Where(a => drIDs.Contains(a.Id) && a.PatientQueues.Any( s => s.CaseFileStatusId == 4)).ToList();
                    var queuePatients = _teleMedecineContext.DoctorMasters.Include(p => p.PatientQueues).Where(a => drIDs.Contains(a.Id)).ToList();
                    foreach (var queuePatient in queuePatients)
                    {
                        doctorque = new DoctorQueues();
                        doctorque.DoctorId = queuePatient.Id;
                        if (queuePatient.PatientQueues.Count > 0)
                        {
                            doctorque.PatientCount = queuePatient.PatientQueues.Where(q => q.CaseFileStatusId == 4).Count();
                        }
                        else
                        {
                            doctorque.PatientCount = 0;
                        }
                        doctorQueues.Add(doctorque);
                    }

                    //3. Assign patient to the doctor who has minimum queue
                    var minimumCount = doctorQueues.Min(m => m.PatientCount);
                    doctorId = doctorQueues.Where(a => a.PatientCount == minimumCount).Select(s => s.DoctorId).FirstOrDefault();

                }
                else
                {
                    doctorId = drIDs.FirstOrDefault();
                }

            }
            return doctorId;
        }

        public bool UpdatePatientCase(PatientCase patientCase, PatientCaseVital patientCaseVital)
        {
            int i = 0;
            int j = 0;


            using (TeleMedecineContext context = new TeleMedecineContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Entry(patientCase).State = EntityState.Modified;
                        i = context.SaveChanges();
                        if (i > 0)
                        {
                            context.Entry(patientCaseVital).State = EntityState.Added;
                            j = context.SaveChanges(); ;
                        }
                        if (i > 0 && j > 0)
                        {
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        string excp = ex.Message;
                        transaction.Rollback();
                        return false;
                    }
                }
            }

        }

        public int GetLoggedPHCID(string userId)
        {
            int PHCId = 0;
            var userDetail = _teleMedecineContext.Phcmasters.Include(u => u.User).Where(a => a.User.Email == userId).FirstOrDefault();
            if (userDetail != null)
            {
                PHCId = userDetail.Id;
            }
            return PHCId;
        }

        public async Task<OnlineDoctorListVM> GetSelectedOnlineDoctors(long patientCaseID)
        {
            OnlineDoctorListVM onlineDoctorList = new OnlineDoctorListVM();
            List<OnlineDoctorVM> onlineDrLists = new List<OnlineDoctorVM>();
            OnlineDoctorVM drListDTO = new OnlineDoctorVM();
            DateTime currentDT = UtilityMaster.GetLocalDateTime();
            try
            {
                int specilizationID = await _teleMedecineContext.PatientCases.Where(a => a.Id == patientCaseID).Select(s => s.SpecializationId).FirstOrDefaultAsync();
                var doctors = await _teleMedecineContext.DoctorMasters.Include(d => d.Specialization).Where(a => a.IsOnline == true && a.SpecializationId == specilizationID).ToListAsync();

                //filter busy

                if (doctors.Count > 0)
                {
                    var Busydoctors = await _teleMedecineContext.TwilioMeetingRoomInfos.Include(d => d.AssignedDoctor)
                   .Where(a => a.IsClosed == false && a.TwilioRoomStatus == "in-progress" && a.AssignedDoctorId != null && a.AssignedDoctor.SpecializationId == specilizationID && a.AssignedDoctor.IsOnline == true).ToListAsync();
                    foreach (var item in doctors)
                    {
                        UserDetail userDetail = _teleMedecineContext.UserDetails.Include(s => s.Gender).Where(a => a.UserId == item.UserId).FirstOrDefault();
                        if (Busydoctors.Any(a => a.AssignedDoctorId == item.Id))
                        {
                            continue;
                        }
                        onlineDrLists.Add(new OnlineDoctorVM
                        {
                            DoctorID = item.Id,
                            DoctorFName = userDetail.FirstName,
                            DoctorMName = userDetail.MiddleName == null ? "" : userDetail.MiddleName,
                            DoctorLName = userDetail.LastName,
                            Photo = userDetail.Photo,
                            Gender = userDetail.Gender.Gender,
                            Specialty = item.Specialization.Specialization

                        });

                    }

                    if (onlineDrLists.Count == 0)
                    {
                        onlineDoctorList.OnlineDoctors = onlineDrLists;
                        onlineDoctorList.Status = "Fail";
                        onlineDoctorList.StatusID = 1;
                        onlineDoctorList.Message = "All the doctors are busy, please add patient to waitlist.";
                    }
                    else
                    {

                        onlineDoctorList.OnlineDoctors = onlineDrLists;
                        onlineDoctorList.Status = "Success";
                        onlineDoctorList.StatusID = 0;
                        onlineDoctorList.Message = "These docoters are avilable to see the patients.";
                    }
                }
                else
                {
                    onlineDoctorList.OnlineDoctors = onlineDrLists;
                    onlineDoctorList.Status = "Fail";
                    onlineDoctorList.StatusID = 2;
                    onlineDoctorList.Message = "No doctor is available, please try after sometime.";
                }
            }
            catch (Exception ex)
            {
                string strMesg = ex.Message;
                onlineDoctorList.OnlineDoctors = onlineDrLists;
                onlineDoctorList.Status = "Fail";
                onlineDoctorList.StatusID = 3;
                onlineDoctorList.Message = "Something went wrong when get online doctor list.";

            }


            return onlineDoctorList;

        }

        public async Task<List<PatientQueueByDoctor>> GetPatientQueueByDoctor(int specializationID)
        {            
            List<PatientQueueByDoctor> queueByDoctors = new List<PatientQueueByDoctor>();
            PatientQueueByDoctor patientQueue;
            var Results = _teleMedecineContext.PatientQueueByDoctorList.FromSqlInterpolated($"EXEC [dbo].[GetPatientQueueByDoctor] @SpecializationID={specializationID}");

            foreach (var item in Results)
            {
                patientQueue = new PatientQueueByDoctor();
                patientQueue.SrNo = item.SrNo;
                patientQueue.NoOfPatientInQueue = item.NoOfPatientInQueue;
                patientQueue.Doctor = item.Doctor;
                patientQueue.DoctorID = item.DoctorID;
                patientQueue.Gender = item.Gender;
                patientQueue.AddToQueue = item.AddToQueue;

                queueByDoctors.Add(patientQueue);
            };
            return queueByDoctors;
        }
        public async Task<List<PatientQueueVM>> GetPatientQueue(int PHCID = 0)
        {
           // await _userRepository.UpdateUserLastAliveUpdate();
            List<PatientQueueVM> queueByDoctors = new List<PatientQueueVM>();
            PatientQueueVM patientQueue;
            var Results = await _teleMedecineContext.PatientQueuesList.FromSqlInterpolated($"EXEC [dbo].[GetAllPatientQueues]").ToListAsync();
            foreach (var item in Results)
            {
                patientQueue = new PatientQueueVM();
                patientQueue.SrNo = item.SrNo;
                patientQueue.Patient = item.Patient;
                patientQueue.CaseHeading = item.CaseHeading;
                patientQueue.PatientID = item.PatientID;
                patientQueue.Doctor = item.Doctor;
                patientQueue.Specialization = item.Specialization;
                patientQueue.Gender = item.Gender;
                patientQueue.PatientCaseID = item.PatientCaseID;
                patientQueue.AssignedDoctorID = item.AssignedDoctorID;
                patientQueue.PHCID = item.PHCID;
                patientQueue.WaitList = item.WaitList;
                patientQueue.AssignedOn = item.AssignedOn;
                patientQueue.StatusOn = item.StatusOn;
                patientQueue.DoctorID = item.DoctorID;
                patientQueue.RegID = item.RegID;
                patientQueue.PHCName = item.PHCName;

                queueByDoctors.Add(patientQueue);
            };
            //Check patient queue status
            bool IsChangeRequire = false;
            foreach (var item in queueByDoctors)
            {
                if (item.WaitList == 0)
                {
                    DateTime currentTime = UtilityMaster.GetLocalDateTime();
                    DateTime statusTime = item.StatusOn;
                    double totalMin = UtilityMaster.TimeDifferenceInMin(currentTime, statusTime);
                    if (totalMin > 3)
                    {
                        IsChangeRequire = true;
                        PatientQueue patientQueueChange = _teleMedecineContext.PatientQueues.FirstOrDefault(a => a.PatientCaseId == item.PatientCaseID && a.CaseFileStatusId == 4);
                        TwilioMeetingRoomInfo videoCallStatus = _teleMedecineContext.TwilioMeetingRoomInfos
                                    .Where(a => a.IsClosed == false && a.TwilioRoomStatus == "in-progress" && a.PatientCaseId == item.PatientCaseID).OrderByDescending(x => x.CloseDate).FirstOrDefault();

                        if (patientQueueChange != null && videoCallStatus == null)
                        {
                            patientQueueChange.AssignedOn = UtilityMaster.GetLocalDateTime();
                            patientQueueChange.UpdatedOn = UtilityMaster.GetLocalDateTime();
                            patientQueueChange.StatusOn = UtilityMaster.GetLocalDateTime();
                            //patientQueueChange.IsQueueChanged = true;
                            _teleMedecineContext.Entry(patientQueueChange).State = EntityState.Modified;
                            int res = _teleMedecineContext.SaveChanges();
                        }
                    }
                    else
                    {
                        IsChangeRequire = false;
                        //When patient queue come at 0.
                        PatientQueue patientQueueValue = _teleMedecineContext.PatientQueues.FirstOrDefault(a => a.PatientCaseId == item.PatientCaseID);
                        if (patientQueueValue != null && patientQueueValue.IsQueueChanged == false)
                        {
                            //patientQueueValue.AssignedOn = UtilityMaster.GetLocalDateTime();
                            patientQueueValue.UpdatedOn = UtilityMaster.GetLocalDateTime();
                            patientQueueValue.StatusOn = UtilityMaster.GetLocalDateTime();
                            patientQueueValue.IsQueueChanged = true;
                            _teleMedecineContext.Entry(patientQueueValue).State = EntityState.Modified;
                            int res = _teleMedecineContext.SaveChanges();
                        }
                    }

                }
                else if (item.WaitList == 1 && IsChangeRequire)
                {
                    PatientQueue patientQueueValue = _teleMedecineContext.PatientQueues.FirstOrDefault(a => a.PatientCaseId == item.PatientCaseID);

                    TwilioMeetingRoomInfo videoCallStatus = _teleMedecineContext.TwilioMeetingRoomInfos
                                   .Where(a => a.IsClosed == false && a.TwilioRoomStatus == "in-progress" && a.AssignedDoctorId == patientQueueValue.AssignedDoctorId).OrderByDescending(x => x.CloseDate).FirstOrDefault();

                    if (patientQueueValue != null && videoCallStatus == null)
                    {
                        //patientQueueValue.AssignedOn = UtilityMaster.GetLocalDateTime();
                        patientQueueValue.UpdatedOn = UtilityMaster.GetLocalDateTime();
                        patientQueueValue.StatusOn = UtilityMaster.GetLocalDateTime();
                        //patientQueueValue.IsQueueChanged = true;
                        _teleMedecineContext.Entry(patientQueueValue).State = EntityState.Modified;
                        int res = _teleMedecineContext.SaveChanges();
                    }
                }
            }
            queueByDoctors = new List<PatientQueueVM>();
            var Results2 = await _teleMedecineContext.PatientQueuesList.FromSqlInterpolated($"EXEC [dbo].[GetAllPatientQueues]").ToListAsync();
            foreach (var item in Results2)
            {
                patientQueue = new PatientQueueVM();
                patientQueue.SrNo = item.SrNo;
                patientQueue.Patient = item.Patient;
                patientQueue.CaseHeading = item.CaseHeading;
                patientQueue.PatientID = item.PatientID;
                patientQueue.Doctor = item.Doctor;
                patientQueue.Specialization = item.Specialization;
                patientQueue.Gender = item.Gender;
                patientQueue.PatientCaseID = item.PatientCaseID;
                patientQueue.AssignedDoctorID = item.AssignedDoctorID;
                patientQueue.PHCID = item.PHCID;
                patientQueue.WaitList = item.WaitList;
                patientQueue.AssignedOn = item.AssignedOn;
                patientQueue.StatusOn = item.StatusOn;
                patientQueue.DoctorID = item.DoctorID;
                patientQueue.RegID = item.RegID;
                patientQueue.PHCName = item.PHCName;

                queueByDoctors.Add(patientQueue);
            };

            if (PHCID == 0)
            {
                return queueByDoctors.ToList();
            }
            else
            {
                return queueByDoctors.Where(a => a.PHCID == PHCID).ToList();
            }
           


        }

        public async Task<List<PatientQueueVM>> GetPatientQueueByDocotorID(int doctorID = 0)
        {            
            List<PatientQueueVM> queueByDoctors = new List<PatientQueueVM>();
            PatientQueueVM patientQueue;
            var Results = await _teleMedecineContext.PatientQueuesList.FromSqlInterpolated($"EXEC [dbo].[GetAllPatientQueues]").ToListAsync();
            foreach (var item in Results)
            {
                patientQueue = new PatientQueueVM();
                patientQueue.SrNo = item.SrNo;
                patientQueue.Patient = item.Patient;
                patientQueue.CaseHeading = item.CaseHeading;
                patientQueue.PatientID = item.PatientID;
                patientQueue.Doctor = item.Doctor;
                patientQueue.Specialization = item.Specialization;
                patientQueue.Gender = item.Gender;
                patientQueue.PatientCaseID = item.PatientCaseID;
                patientQueue.AssignedDoctorID = item.AssignedDoctorID;
                patientQueue.PHCID = item.PHCID;
                patientQueue.WaitList = item.WaitList;
                patientQueue.AssignedOn = item.AssignedOn;
                patientQueue.StatusOn = item.StatusOn;
                patientQueue.DoctorID = item.DoctorID;
                patientQueue.RegID = item.RegID;
                patientQueue.PHCName = item.PHCName;

                queueByDoctors.Add(patientQueue);
            };
            //Check patient queue status
            bool IsChangeRequire = false;
            foreach (var item in queueByDoctors)
            {
                if (item.WaitList == 0)
                {
                    DateTime currentTime = UtilityMaster.GetLocalDateTime();
                    DateTime statusTime = item.StatusOn;
                    double totalMin = UtilityMaster.TimeDifferenceInMin(currentTime, statusTime);
                    if (totalMin > 3)
                    {
                        IsChangeRequire = true;
                        PatientQueue patientQueueChange = _teleMedecineContext.PatientQueues.FirstOrDefault(a => a.PatientCaseId == item.PatientCaseID && a.CaseFileStatusId == 4);
                        TwilioMeetingRoomInfo videoCallStatus = _teleMedecineContext.TwilioMeetingRoomInfos
                                    .Where(a => a.IsClosed == false && a.TwilioRoomStatus == "in-progress" && a.PatientCaseId == item.PatientCaseID).OrderByDescending(x => x.CloseDate).FirstOrDefault();
                        if (videoCallStatus != null)
                        {
                            videoCallStatus.CloseDate = currentTime;
                        }
                        if (patientQueueChange != null && videoCallStatus == null)
                        {
                            patientQueueChange.AssignedOn = UtilityMaster.GetLocalDateTime();
                            patientQueueChange.UpdatedOn = UtilityMaster.GetLocalDateTime();
                            patientQueueChange.StatusOn = UtilityMaster.GetLocalDateTime();
                            //patientQueueChange.IsQueueChanged = true;
                            _teleMedecineContext.Entry(patientQueueChange).State = EntityState.Modified;
                            int res = _teleMedecineContext.SaveChanges();
                        }
                    }
                    else
                    {
                        IsChangeRequire = false;
                        //When patient queue come at 0.
                        PatientQueue patientQueueValue = _teleMedecineContext.PatientQueues.FirstOrDefault(a => a.PatientCaseId == item.PatientCaseID);
                        if (patientQueueValue != null && patientQueueValue.IsQueueChanged == false)
                        {
                            //patientQueueValue.AssignedOn = UtilityMaster.GetLocalDateTime();
                            patientQueueValue.UpdatedOn = UtilityMaster.GetLocalDateTime();
                            patientQueueValue.StatusOn = UtilityMaster.GetLocalDateTime();
                            patientQueueValue.IsQueueChanged = true;
                            _teleMedecineContext.Entry(patientQueueValue).State = EntityState.Modified;
                            int res = _teleMedecineContext.SaveChanges();
                        }
                    }

                }
                else if (item.WaitList == 1 && IsChangeRequire)
                {
                    PatientQueue patientQueueValue = _teleMedecineContext.PatientQueues.FirstOrDefault(a => a.PatientCaseId == item.PatientCaseID);
                    if (patientQueueValue != null)
                    {
                        //patientQueueValue.AssignedOn = UtilityMaster.GetLocalDateTime();
                        patientQueueValue.UpdatedOn = UtilityMaster.GetLocalDateTime();
                        patientQueueValue.StatusOn = UtilityMaster.GetLocalDateTime();
                        //patientQueueValue.IsQueueChanged = true;
                        _teleMedecineContext.Entry(patientQueueValue).State = EntityState.Modified;
                        int res = _teleMedecineContext.SaveChanges();
                    }
                }
            }
            queueByDoctors = new List<PatientQueueVM>();
            var Results2 = await _teleMedecineContext.PatientQueuesList.FromSqlInterpolated($"EXEC [dbo].[GetAllPatientQueues]").ToListAsync();
            foreach (var item in Results2)
            {
                patientQueue = new PatientQueueVM();
                patientQueue.SrNo = item.SrNo;
                patientQueue.Patient = item.Patient;
                patientQueue.CaseHeading = item.CaseHeading;
                patientQueue.PatientID = item.PatientID;
                patientQueue.Doctor = item.Doctor;
                patientQueue.Specialization = item.Specialization;
                patientQueue.Gender = item.Gender;
                patientQueue.PatientCaseID = item.PatientCaseID;
                patientQueue.AssignedDoctorID = item.AssignedDoctorID;
                patientQueue.PHCID = item.PHCID;
                patientQueue.WaitList = item.WaitList;
                patientQueue.AssignedOn = item.AssignedOn;
                patientQueue.StatusOn = item.StatusOn;
                patientQueue.DoctorID = item.DoctorID;
                patientQueue.RegID = item.RegID;
                patientQueue.PHCName = item.PHCName;

                queueByDoctors.Add(patientQueue);
            };

            if (doctorID == 0)
            {
                return queueByDoctors.ToList();
            }
            else
            {
                return queueByDoctors.Where(a => a.DoctorID == doctorID).ToList();
            }


        }

        public async Task<PatientReferToDoctorVM> AddPatientInDoctorsQueue(PatientReferToDoctorVM patientReferToDoctorVM)
        {
            PatientReferToDoctorVM outPatientReferToDoctorVM = new PatientReferToDoctorVM();
            PatientQueue patientQueue = new PatientQueue();
            string message = string.Empty;
            int autoAssignDoctorID = 0;
            int i = 0;

            try
            {

                if (patientReferToDoctorVM != null)
                {

                    if (patientReferToDoctorVM.AssignedDocterID > 0)
                    {
                        patientQueue.AssignedDoctorId = patientReferToDoctorVM.AssignedDocterID;
                        patientQueue.Comment = "Manually assign doctor";
                    }
                    else
                    {
                        autoAssignDoctorID = AutoAssignDoctor(patientReferToDoctorVM.PatientCaseID);
                        if (autoAssignDoctorID > 0)
                        {
                            patientQueue.AssignedDoctorId = autoAssignDoctorID;
                            patientQueue.Comment = "Auto assign doctor";
                        }
                        else
                        {
                            outPatientReferToDoctorVM.AssignedDocterID = 0;
                            outPatientReferToDoctorVM.PatientCaseID = patientQueue.PatientCaseId;
                            outPatientReferToDoctorVM.PHCID = patientQueue.AssignedBy;
                            outPatientReferToDoctorVM.Status = "Fail";
                            outPatientReferToDoctorVM.Message = "Assigned doctor is not avialble !";
                            return outPatientReferToDoctorVM;
                        }

                    }
                    patientQueue.PatientCaseId = patientReferToDoctorVM.PatientCaseID;
                    patientQueue.AssignedBy = patientReferToDoctorVM.PHCID;
                    patientQueue.CaseFileStatusId = await GetCaseFileStatus();
                    patientQueue.StatusOn = UtilityMaster.GetLocalDateTime();
                    patientQueue.AssignedOn = UtilityMaster.GetLocalDateTime();
                    //patientQueue.AssignedDoctorId = patientReferToDoctorVM.AssignedDocterID;
                    //patientQueue.Comment = "Assigned by PHC";

                    PatientQueue existingpatientQueue = _teleMedecineContext.PatientQueues.FirstOrDefault(a => a.PatientCaseId == patientReferToDoctorVM.PatientCaseID && a.CaseFileStatusId != 5);
                    if (existingpatientQueue == null)
                    {
                        //_teleMedecineContext.PatientQueues.Add(patientQueue);
                        _teleMedecineContext.Entry(patientQueue).State = EntityState.Added;

                    }
                    else
                    {
                        existingpatientQueue.AssignedDoctorId = patientQueue.AssignedDoctorId;
                        existingpatientQueue.StatusOn = UtilityMaster.GetLocalDateTime();
                        existingpatientQueue.AssignedOn = UtilityMaster.GetLocalDateTime();
                        existingpatientQueue.AssignedBy = patientReferToDoctorVM.PHCID;
                        existingpatientQueue.CaseFileStatusId = await GetCaseFileStatus();
                        existingpatientQueue.Comment = "Reassign the doctor";
                        _teleMedecineContext.Entry(existingpatientQueue).State = EntityState.Modified;
                    }

                    PatientCase patientCase = _teleMedecineContext.PatientCases.FirstOrDefault(a => a.Id == patientReferToDoctorVM.PatientCaseID);
                    if (patientCase != null)
                    {
                        patientCase.CaseStatusID = 3;
                        _teleMedecineContext.Entry(patientCase).State = EntityState.Modified;
                    }
                    i = _teleMedecineContext.SaveChanges();


                    if (i > 0)
                    {
                        outPatientReferToDoctorVM.AssignedDocterID = patientQueue.AssignedDoctorId;
                        outPatientReferToDoctorVM.PatientCaseID = patientQueue.PatientCaseId;
                        outPatientReferToDoctorVM.PHCID = patientQueue.AssignedBy;
                        outPatientReferToDoctorVM.Status = "Success";
                        outPatientReferToDoctorVM.Message = "Assigned to doctor sucessfully !";
                        try
                        {
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
                        }
                        catch (Exception ex)
                        {
                            outPatientReferToDoctorVM.Status = "Success";
                            outPatientReferToDoctorVM.Message = "Assigned to doctor sucessfully ! but notification did not add";
                            _logger.LogError("Exception generate when going to add notification for patient case Id :" + patientQueue.PatientCaseId, ex);
                        }


                        return outPatientReferToDoctorVM;
                    }
                    else
                    {
                        outPatientReferToDoctorVM.AssignedDocterID = 0;
                        outPatientReferToDoctorVM.PatientCaseID = patientQueue.PatientCaseId;
                        outPatientReferToDoctorVM.PHCID = patientQueue.AssignedBy;
                        outPatientReferToDoctorVM.Status = "Fail";
                        outPatientReferToDoctorVM.Message = "Error: When assigned to doctor!";
                        return outPatientReferToDoctorVM;
                    }


                }
                else
                {
                    outPatientReferToDoctorVM.AssignedDocterID = 0;
                    outPatientReferToDoctorVM.PatientCaseID = patientQueue.PatientCaseId;
                    outPatientReferToDoctorVM.PHCID = patientQueue.AssignedBy;
                    outPatientReferToDoctorVM.Status = "Fail";
                    outPatientReferToDoctorVM.Message = "Error: View model has no data!";
                    return outPatientReferToDoctorVM;
                }
            }
            catch (Exception ex)
            {
                string messageExp = ex.Message;
                _logger.LogError("Exception generate when going to assign doctor for patient case Id :" + patientQueue.PatientCaseId, ex);
                throw;
            }

        }

        public async Task<RemovePatientFromQueueVM> RemovePatientFromDoctorsQueue(long patientCaseID)
        {
            int i = 0;
            RemovePatientFromQueueVM removePatientFromQueue = new RemovePatientFromQueueVM();
            PatientQueue patientQueue = await _teleMedecineContext.PatientQueues.FirstOrDefaultAsync(a => a.PatientCaseId == patientCaseID);
            if (patientQueue != null)
            {
                patientQueue.UpdatedOn = UtilityMaster.GetLocalDateTime();
                patientQueue.IsQueueChanged = true;
                patientQueue.CaseFileStatusId = 6;
                _teleMedecineContext.Entry(patientQueue).State = EntityState.Modified;
                PatientCase patientCase = _teleMedecineContext.PatientCases.FirstOrDefault(a => a.Id == patientCaseID);
                if (patientCase != null)
                {
                    patientCase.CaseStatusID = 2;
                    _teleMedecineContext.Entry(patientCase).State = EntityState.Modified;
                }
                try
                {
                    i = _teleMedecineContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }

                if (i > 0)
                {
                    removePatientFromQueue.Status = "success";
                    removePatientFromQueue.ErrorMessage = "";
                    removePatientFromQueue.PatientCaseID = patientCaseID;
                    removePatientFromQueue.PatientID = patientCase != null ? patientCase.PatientId : 0;
                }
                else
                {
                    removePatientFromQueue.Status = "fail";
                    removePatientFromQueue.ErrorMessage = "Patient queue did not updated.";
                    removePatientFromQueue.PatientCaseID = patientCaseID;
                    removePatientFromQueue.PatientID = patientCase != null ? patientCase.PatientId : 0;
                }

                return removePatientFromQueue;
            }
            else
            {
                removePatientFromQueue.Status = "fail";
                removePatientFromQueue.ErrorMessage = "PatientcaseID has null value";
                removePatientFromQueue.PatientCaseID = patientCaseID;
                removePatientFromQueue.PatientID = 0;
                return removePatientFromQueue;
            }
        }

        public async Task<List<ALLPatientsQueueVM>> GetAllPatientQueue()
        {
            List<ALLPatientsQueueVM> queueByDoctors = new List<ALLPatientsQueueVM>();
            ALLPatientsQueueVM patientQueue;
            var Results = await _teleMedecineContext.AllPatientQueueList.FromSqlInterpolated($"EXEC [dbo].[GetAllPHCPatientsQueue]").ToListAsync();
            foreach (var item in Results)
            {
                patientQueue = new ALLPatientsQueueVM();
                patientQueue.SrNo = item.SrNo;
                patientQueue.Patient = item.Patient;
                patientQueue.CaseHeading = item.CaseHeading;
                patientQueue.PatientID = item.PatientID;
                patientQueue.Doctor = item.Doctor;
                patientQueue.Specialization = item.Specialization;
                patientQueue.Gender = item.Gender;
                patientQueue.PatientCaseID = item.PatientCaseID;
                patientQueue.AssignedDoctorID = item.AssignedDoctorID;
                patientQueue.PHCID = item.PHCID;
                patientQueue.WaitList = item.WaitList;
                patientQueue.AssignedOn = item.AssignedOn;
                patientQueue.PHCName = item.PHCName;
                patientQueue.CreatedOn = item.CreatedOn;
                patientQueue.RegID = item.RegID;

                queueByDoctors.Add(patientQueue);
            };

            return queueByDoctors;
        }

        public async Task<List<AllPendingPatient>> GetAllPendingPatient()
        {
            List<AllPendingPatient> queueByDoctors = new List<AllPendingPatient>();
            AllPendingPatient patientQueue;
            var Results = await _teleMedecineContext.AllPendingPatientList.FromSqlInterpolated($"EXEC [dbo].[GetAllPandingPatientList]").ToListAsync();
            foreach (var item in Results)
            {
                patientQueue = new AllPendingPatient();
                patientQueue.SrNo = item.SrNo;
                patientQueue.Patient = item.Patient;
                patientQueue.CaseHeading = item.CaseHeading;
                patientQueue.PatientID = item.PatientID;
                patientQueue.Specialization = item.Specialization;
                patientQueue.PatientCaseID = item.PatientCaseID;
                patientQueue.PHCID = item.PHCID;
                patientQueue.PHCName = item.PHCName;
                patientQueue.CreatedOn = item.CreatedOn;
                patientQueue.RegID = item.RegID;
                patientQueue.CaseFileStatusID = item.CaseFileStatusID;
                patientQueue.CaseStatusID = item.CaseStatusID;

                queueByDoctors.Add(patientQueue);
            };

            return queueByDoctors;
        }

        public async Task<PatientReferToDoctorVM> PatientReferToDoctorByAdmin(PatientReferToDoctorVM patientReferToDoctorVM)
        {
            PatientReferToDoctorVM outPatientReferToDoctorVM = new PatientReferToDoctorVM();
            PatientQueue patientQueue = new PatientQueue();
            string message = string.Empty;
            int autoAssignDoctorID = 0;
            int i = 0;
            int loggedUserId = 0;
            int caseCreatedBy = 0;
            DateTime? callInitiatedTime = UtilityMaster.GetLocalDateTime();
            DateTime currentTime = UtilityMaster.GetLocalDateTime();
            caseCreatedBy = _teleMedecineContext.PatientCases.Where(a => a.Id == patientReferToDoctorVM.PatientCaseID).Select(s => s.CreatedBy).FirstOrDefault();

            try
            {

                if (patientReferToDoctorVM != null)
                {
                    //int Busydoctors = _teleMedecineContext.TwilioMeetingRoomInfos
                    //                .Where(a => a.IsClosed == false && a.TwilioRoomStatus == "in-progress" && a.AssignedDoctorId == patientReferToDoctorVM.AssignedDocterID).Count();

                    TwilioMeetingRoomInfo videoCallStatus = _teleMedecineContext.TwilioMeetingRoomInfos
                                    .Where(a => a.IsClosed == false && a.TwilioRoomStatus == "in-progress" && a.AssignedDoctorId == patientReferToDoctorVM.AssignedDocterID).OrderByDescending(x => x.CloseDate).FirstOrDefault();

                    if (videoCallStatus != null)
                    {
                        //TwilioMeetingRoomInfo twilioMeeting = videoCallStatus.OrderByDescending(a => a.CloseDate).FirstOrDefault();
                        callInitiatedTime = videoCallStatus != null ? videoCallStatus.CloseDate : UtilityMaster.GetLocalDateTime();
                        double diffInMin = UtilityMaster.TimeDifferenceInMin(callInitiatedTime.Value, currentTime);

                        if (diffInMin < 30)
                        {
                            outPatientReferToDoctorVM.AssignedDocterID = 0;
                            outPatientReferToDoctorVM.PatientCaseID = patientReferToDoctorVM.PatientCaseID;
                            outPatientReferToDoctorVM.PHCID = patientReferToDoctorVM.PHCID;
                            outPatientReferToDoctorVM.Status = "Fail";
                            outPatientReferToDoctorVM.Message = "Doctor is busy, please call other Doctor !";
                            return outPatientReferToDoctorVM;
                        }
                    }


                    if (patientReferToDoctorVM.AssignedDocterID > 0)
                    {
                        patientQueue.AssignedDoctorId = patientReferToDoctorVM.AssignedDocterID;
                        patientQueue.Comment = "Manually assign doctor";
                    }
                    else
                    {
                        autoAssignDoctorID = AutoAssignDoctor(patientReferToDoctorVM.PatientCaseID);
                        if (autoAssignDoctorID > 0)
                        {
                            patientQueue.AssignedDoctorId = autoAssignDoctorID;
                            patientQueue.Comment = "Auto assign doctor";
                        }
                        else
                        {
                            outPatientReferToDoctorVM.AssignedDocterID = 0;
                            outPatientReferToDoctorVM.PatientCaseID = patientQueue.PatientCaseId;
                            outPatientReferToDoctorVM.PHCID = patientQueue.AssignedBy;
                            outPatientReferToDoctorVM.Status = "Fail";
                            outPatientReferToDoctorVM.Message = "Assigned doctor is not avialble !";
                            return outPatientReferToDoctorVM;
                        }

                    }
                    patientQueue.PatientCaseId = patientReferToDoctorVM.PatientCaseID;
                    patientQueue.AssignedBy = caseCreatedBy;
                    //patientQueue.AssignedBy = _teleMedecineContext.PatientCases.Where(a => a.Id == patientReferToDoctorVM.PatientCaseID).Select(s => s.CreatedBy).FirstOrDefault();
                    patientQueue.CaseFileStatusId = await GetCaseFileStatus();
                    patientQueue.StatusOn = UtilityMaster.GetLocalDateTime();
                    patientQueue.AssignedOn = UtilityMaster.GetLocalDateTime();
                    patientQueue.UpdatedOn = UtilityMaster.GetLocalDateTime();
                    patientQueue.IsQueueChanged = false;

                    //patientQueue.AssignedDoctorId = patientReferToDoctorVM.AssignedDocterID;
                    //patientQueue.Comment = "Assigned by PHC";

                    PatientQueue existingpatientQueue = _teleMedecineContext.PatientQueues.FirstOrDefault(a => a.PatientCaseId == patientReferToDoctorVM.PatientCaseID && a.CaseFileStatusId != 5);
                    if (existingpatientQueue == null)
                    {
                        //_teleMedecineContext.PatientQueues.Add(patientQueue);
                        _teleMedecineContext.Entry(patientQueue).State = EntityState.Added;

                    }
                    else
                    {
                        existingpatientQueue.AssignedDoctorId = patientQueue.AssignedDoctorId;
                        //existingpatientQueue.StatusOn = UtilityMaster.GetLocalDateTime();
                        existingpatientQueue.AssignedOn = UtilityMaster.GetLocalDateTime();
                        existingpatientQueue.AssignedBy = caseCreatedBy;
                        existingpatientQueue.CaseFileStatusId = await GetCaseFileStatus();
                        existingpatientQueue.Comment = "Reassign the doctor";
                        existingpatientQueue.UpdatedOn = UtilityMaster.GetLocalDateTime();
                        existingpatientQueue.IsQueueChanged = false;
                        _teleMedecineContext.Entry(existingpatientQueue).State = EntityState.Modified;
                    }

                    PatientCase patientCase = _teleMedecineContext.PatientCases.FirstOrDefault(a => a.Id == patientReferToDoctorVM.PatientCaseID);
                    if (patientCase != null)
                    {
                        patientCase.CaseStatusID = 3;
                        _teleMedecineContext.Entry(patientCase).State = EntityState.Modified;
                    }
                    i = _teleMedecineContext.SaveChanges();


                    if (i > 0)
                    {
                        outPatientReferToDoctorVM.AssignedDocterID = patientQueue.AssignedDoctorId;
                        outPatientReferToDoctorVM.PatientCaseID = patientQueue.PatientCaseId;
                        outPatientReferToDoctorVM.PHCID = patientQueue.AssignedBy;
                        outPatientReferToDoctorVM.Status = "Success";
                        outPatientReferToDoctorVM.Message = "Assigned to doctor sucessfully !";
                        try
                        {
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
                        }
                        catch (Exception ex)
                        {
                            outPatientReferToDoctorVM.Status = "Success";
                            outPatientReferToDoctorVM.Message = "Assigned to doctor sucessfully ! but notification did not add";
                            _logger.LogError("Exception generate when going to add notification for patient case Id :" + patientQueue.PatientCaseId, ex);
                        }


                        return outPatientReferToDoctorVM;
                    }
                    else
                    {
                        outPatientReferToDoctorVM.AssignedDocterID = 0;
                        outPatientReferToDoctorVM.PatientCaseID = patientQueue.PatientCaseId;
                        outPatientReferToDoctorVM.PHCID = patientQueue.AssignedBy;
                        outPatientReferToDoctorVM.Status = "Fail";
                        outPatientReferToDoctorVM.Message = "Error: When assigned to doctor!";
                        return outPatientReferToDoctorVM;
                    }


                }
                else
                {
                    outPatientReferToDoctorVM.AssignedDocterID = 0;
                    outPatientReferToDoctorVM.PatientCaseID = patientQueue.PatientCaseId;
                    outPatientReferToDoctorVM.PHCID = patientQueue.AssignedBy;
                    outPatientReferToDoctorVM.Status = "Fail";
                    outPatientReferToDoctorVM.Message = "Error: View model has no data!";
                    return outPatientReferToDoctorVM;
                }
            }
            catch (Exception ex)
            {
                string messageExp = ex.Message;
                _logger.LogError("Exception generate when going to assign doctor for patient case Id :" + patientQueue.PatientCaseId, ex);
                throw;
            }
        }

        public async Task<bool> IsDoctorFreeToReceiveCall(long patientCaseID)
        {
            bool IsDoctorFree = false;
            DateTime localDate = UtilityMaster.GetLocalDateTime();
            int doctorId = 0;
            doctorId = await _teleMedecineContext.PatientQueues.Where(a => a.PatientCaseId == patientCaseID && a.CaseFileStatusId == 4 && a.AssignedOn.Day == localDate.Day && a.AssignedOn.Month == localDate.Month && a.AssignedOn.Year == localDate.Year)
                .OrderByDescending(b => b.AssignedOn)
                .Select(s => s.AssignedDoctorId)
                .FirstOrDefaultAsync();
            if (doctorId > 0)
            {
                IsDoctorFree = _teleMedecineContext.TwilioMeetingRoomInfos.Any(a => a.IsClosed == false && a.TwilioRoomStatus == "in-progress" && a.AssignedDoctorId == doctorId);
            }

            if (IsDoctorFree)
                return false;
            else
                return true;

        }

        public async Task<bool> UpdateCallStatusTime(long patientCaseID)
        {
            DateTime dateTime = UtilityMaster.GetLocalDateTime();
            int i = 0;
            PatientQueue patientQueue = await _teleMedecineContext.PatientQueues.FirstOrDefaultAsync(a => a.PatientCaseId == patientCaseID && a.CaseFileStatusId == 4);

            if (patientQueue != null)
            {
                patientQueue.StatusOn = dateTime;
                _teleMedecineContext.Entry(patientQueue).State = EntityState.Modified;
                i = _teleMedecineContext.SaveChanges();
            }
            if (i > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> IsPHCFreeToReceiveCall(long patientCaseID)
        {
            bool isPHConCall = false;
           DateTime localDate = UtilityMaster.GetLocalDateTime();
            int phcID = await _teleMedecineContext.PatientCases.Where(a => a.Id == patientCaseID).Select(s => s.CreatedBy).FirstOrDefaultAsync();
            var patientCaseIds = _teleMedecineContext.PatientQueues
                .Include(p => p.PatientCase)
                .Where(x => x.PatientCase.CreatedBy == phcID && x.AssignedOn.Year == localDate.Year && x.AssignedOn.Month == localDate.Month && x.AssignedOn.Day == localDate.Day && x.CaseFileStatusId == 4 ).Select(s => s.Id).ToList();
             isPHConCall = _teleMedecineContext.TwilioMeetingRoomInfos.Any(x => patientCaseIds.Contains(x.PatientCaseId.Value) && x.IsClosed ==false && x.TwilioRoomStatus == "in-progress");
            if (isPHConCall)
                return false;
            else
                return true;
        }
    }
    public class DoctorQueues
    {
        public int DoctorId { get; set; }
        public int PatientCount { get; set; }
    }

}