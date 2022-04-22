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
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.BaseClasses
{
    public class DoctorRepository : Repository<DoctorMaster>, IDoctorRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;
        public DoctorRepository(ILogger<UserRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public void AddDoctorDetails()
        {
            throw new NotImplementedException();
        }

        public void CallPHC()
        {
            throw new NotImplementedException();
        }


        public async Task<DoctorDTO> GetDoctorDetails(GetDoctorDetailVM getDoctorDetailVM)
        {
            //
            DoctorMaster doctorMaster = await _teleMedecineContext.DoctorMasters.Where(o => o.User.Email.ToLower() == getDoctorDetailVM.UserEmailID.ToLower()).FirstOrDefaultAsync();
            UserDetail userDetail = await _teleMedecineContext.UserDetails.Where(o => o.UserId == doctorMaster.UserId).FirstOrDefaultAsync();
            var DTO = new DoctorDTO();
            if (doctorMaster != null && userDetail != null)
            {
                DTO = _mapper.Map<DoctorDTO>(doctorMaster);
                DTO.detailsDTO = _mapper.Map<DetailsDTO>(userDetail);
            }
            return DTO;
        }
        public async Task<List<MedicineMasterDTO>> GetListOfMedicine()
        {
            List<MedicineMaster> masters = await _teleMedecineContext.MedicineMasters.ToListAsync();
            var DTOList = new List<MedicineMasterDTO>();
            foreach (var item in masters)
            {
                MedicineMasterDTO mapdata = _mapper.Map<MedicineMasterDTO>(item);
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<NotificationDTO>> GetListOfNotification(GetListOfNotificationVM getListOfNotificationVM)
        {
            List<Notification> notifications = await _teleMedecineContext.Notifications.Where(o => o.ToUserNavigation.Email.ToLower() == getListOfNotificationVM.UserEmail.ToLower()).ToListAsync();
            var DTOList = new List<NotificationDTO>();

            foreach (var Notification in notifications)
            {
                NotificationDTO mapdata = _mapper.Map<NotificationDTO>(Notification);
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<VitalMasterDTO>> GetListOfVital()
        {
            List<VitalMaster> masters = await _teleMedecineContext.VitalMasters.ToListAsync();
            var DTOList = new List<VitalMasterDTO>();
            foreach (var item in masters)
            {
                VitalMasterDTO mapdata = _mapper.Map<VitalMasterDTO>(item);
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<CdssguidelineMasterDTO> GetCDSSGuideLines()
        {
            CdssguidelineMaster cdssguidelineMaster = await _teleMedecineContext.CdssguidelineMasters.FirstOrDefaultAsync();
            var DTOList = new List<NotificationDTO>();
            CdssguidelineMasterDTO mapdata = new CdssguidelineMasterDTO();
            if (cdssguidelineMaster != null)
            {
                mapdata = _mapper.Map<CdssguidelineMasterDTO>(cdssguidelineMaster);
            }
            return mapdata;
        }
        public async Task<List<PHCHospitalDTO>> GetListOfPHCHospital()
        {
            List<Phcmaster> masters = await _teleMedecineContext.Phcmasters.ToListAsync();
            var DTOList = new List<PHCHospitalDTO>();
            foreach (var item in masters)
            {
                PHCHospitalDTO mapdata = _mapper.Map<PHCHospitalDTO>(item);
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<SpecializationDTO>> GetListOfSpecializationMaster()
        {
            List<SpecializationMaster> masters = await _teleMedecineContext.SpecializationMasters.ToListAsync();
            var DTOList = new List<SpecializationDTO>();
            foreach (var item in masters)
            {
                SpecializationDTO mapdata = _mapper.Map<SpecializationDTO>(item);
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<SubSpecializationDTO>> GetListOfSubSpecializationMaster(int SpecializationId)
        {
            List<SubSpecializationMaster> masters = await _teleMedecineContext.SubSpecializationMasters.Where(a => a.SpecializationId == SpecializationId).ToListAsync();
            var DTOList = new List<SubSpecializationDTO>();
            foreach (var item in masters)
            {
                SubSpecializationDTO mapdata = _mapper.Map<SubSpecializationDTO>(item);
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<bool> UpdateDoctorDetails(DoctorDTO doctorDTO)
        {
            if (doctorDTO != null)
            {
                DoctorMaster masters = await _teleMedecineContext.DoctorMasters.Where(a => a.Id == doctorDTO.Id).FirstOrDefaultAsync();
                //public int Id { get; set; }
                //public int ZoneId { get; set; }
                //public int ClusterId { get; set; }
                //masters.UserId { get; set; }
                masters.SpecializationId = doctorDTO.SpecializationId;
                masters.SubSpecializationId = doctorDTO.SpecializationId;
                masters.Mciid = doctorDTO.Mciid;
                masters.RegistrationNumber = doctorDTO.RegistrationNumber;
                masters.Qualification = doctorDTO.Qualification;
                masters.Designation = doctorDTO.Designation;
                masters.PhoneNumber = doctorDTO.PhoneNumber;
                masters.DigitalSignature = doctorDTO.DigitalSignature;
                masters.Panno = doctorDTO.PanNo;
                masters.BankName = doctorDTO.BankName;
                masters.BranchName = doctorDTO.BranchName;
                masters.AccountNumber = doctorDTO.AccountNumber;
                masters.Ifsccode = doctorDTO.Ifsccode;
                //masters.CreatedBy { get; set; }
                //masters.CreatedOn { get; set; }
                masters.UpdatedBy = doctorDTO.UpdatedBy;
                masters.UpdatedOn = DateTime.Now;
                await _teleMedecineContext.SaveChangesAsync();

                if (doctorDTO.detailsDTO != null)
                {
                    UserDetail userDetail = await _teleMedecineContext.UserDetails.Where(a => a.UserId == doctorDTO.UserId).FirstOrDefaultAsync();
                    userDetail.TitleId = doctorDTO.detailsDTO.TitleId;
                    userDetail.FirstName = doctorDTO.detailsDTO.FirstName;
                    userDetail.MiddleName = doctorDTO.detailsDTO.MiddleName;
                    userDetail.LastName = doctorDTO.detailsDTO.LastName;
                    userDetail.Dob = doctorDTO.detailsDTO.Dob;
                    userDetail.GenderId = doctorDTO.detailsDTO.GenderId;
                    userDetail.EmailId = doctorDTO.detailsDTO.EmailId;
                    userDetail.PhoneNumber = "";
                    userDetail.FatherName = "";
                    userDetail.CountryId = doctorDTO.detailsDTO.CountryId;
                    userDetail.StateId = doctorDTO.detailsDTO.StateId;
                    userDetail.City = doctorDTO.detailsDTO.City;
                    userDetail.Address = "";
                    userDetail.PinCode = doctorDTO.detailsDTO.PinCode;
                    //userDetail.Photo { get; set; } = null!;
                    //userDetail.Occupation { get; set; }
                    //userDetail.IsMarried { get; set; }
                    //userDetail.NoOfChildren { get; set; }
                    //userDetail.IdproofTypeId { get; set; }
                    //userDetail.IdproofNumber { get; set; }
                    userDetail.UpdatedBy = doctorDTO.UpdatedBy;
                    userDetail.UpdatedOn = DateTime.Now;
                    await _teleMedecineContext.SaveChangesAsync();
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        public async Task<List<GetTodayesPatientsDTO>> GetTodayesPatients(DoctorVM doctorVM)
        {

            List<PatientQueue> masters = await _teleMedecineContext.PatientQueues
                .Include(d => d.PatientCase.Patient.Gender)
                .Include(c => c.AssignedByNavigation)
                .Include(a => a.PatientCase)
                .Include(b => b.PatientCase.Patient)
                .Where(a => a.CaseFileStatusId == 4 && a.AssignedDoctorId == doctorVM.DoctorID
                && a.AssignedOn.Year == DateTime.Now.Year
                && a.AssignedOn.Month == DateTime.Now.Month
                && a.AssignedOn.Day == DateTime.Now.Day
                ).ToListAsync();

            var DTOList = new List<GetTodayesPatientsDTO>();
            foreach (var item in masters)
            {
                //GetTodayesPatientsDTO mapdata = _mapper.Map<GetTodayesPatientsDTO>(item);
                GetTodayesPatientsDTO mapdata = new GetTodayesPatientsDTO();
                mapdata.PatientName = item.PatientCase.Patient.FirstName + " " + item.PatientCase.Patient.LastName;
                mapdata.PhoneNumber = item.PatientCase.Patient.PhoneNumber;
                mapdata.ReferredbyPHCName = item.AssignedByNavigation.Name;
                mapdata.Age = CommanFunction.GetAge(item.PatientCase.Patient.Dob);
                mapdata.Gender = item.PatientCase.Patient.Gender.Gender;
                mapdata.PatientID = item.PatientCase.Patient.PatientId;
                mapdata.id = item.PatientCase.Patient.Id;
                //mapdata.status = item.PatientCase.Patient.PatientStatus.PatientStatus;
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<GetTodayesPatientsDTO>> GetCompletedConsultationPatientsHistory(DoctorVM doctorVM)
        {
            var today = DateTime.Today;
            List<PatientQueue> masters = await _teleMedecineContext.PatientQueues
                .Include(d => d.PatientCase.Patient.Gender)
                .Include(c => c.AssignedByNavigation)
                .Include(a => a.PatientCase)
                .Include(b => b.PatientCase.Patient)
                .Where(a => a.CaseFileStatusId == 5 && a.AssignedDoctorId == doctorVM.DoctorID
                 && a.AssignedOn.Year == today.Year
                && a.AssignedOn.Month == today.Month
                && a.AssignedOn.Date == today.Date
                ).ToListAsync();

            var DTOList = new List<GetTodayesPatientsDTO>();
            foreach (var item in masters)
            {
                //GetTodayesPatientsDTO mapdata = _mapper.Map<GetTodayesPatientsDTO>(item);
                GetTodayesPatientsDTO mapdata = new GetTodayesPatientsDTO();
                mapdata.PatientName = item.PatientCase.Patient.FirstName + " " + item.PatientCase.Patient.LastName;
                mapdata.PhoneNumber = item.PatientCase.Patient.PhoneNumber;
                mapdata.ReferredbyPHCName = item.AssignedByNavigation.Name;
                mapdata.Age = CommanFunction.GetAge(item.PatientCase.Patient.Dob);
                mapdata.Gender = item.PatientCase.Patient.Gender.Gender;
                mapdata.PatientID = item.PatientCase.Patient.PatientId;
                mapdata.id = item.PatientCase.Patient.Id;
                //mapdata.status = item.PatientCase.Patient.PatientStatus.PatientStatus;
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<GetTodayesPatientsDTO>> GetYesterdayPatientsHistory(DoctorVM doctorVM)
        {
            var today = DateTime.Today.AddDays(-1);
            List<PatientQueue> masters = await _teleMedecineContext.PatientQueues
                .Include(d => d.PatientCase.Patient.Gender)
                .Include(c => c.AssignedByNavigation)
                .Include(a => a.PatientCase)
                .Include(b => b.PatientCase.Patient)
                .Where(a => a.CaseFileStatusId == 5 && a.AssignedDoctorId == doctorVM.DoctorID
                 && a.AssignedOn.Year == today.Year
                && a.AssignedOn.Month == today.Month
                && a.AssignedOn.Date == today.Date
                ).ToListAsync();

            var DTOList = new List<GetTodayesPatientsDTO>();
            foreach (var item in masters)
            {
                //GetTodayesPatientsDTO mapdata = _mapper.Map<GetTodayesPatientsDTO>(item);
                GetTodayesPatientsDTO mapdata = new GetTodayesPatientsDTO();
                mapdata.PatientName = item.PatientCase.Patient.FirstName + " " + item.PatientCase.Patient.LastName;
                mapdata.PhoneNumber = item.PatientCase.Patient.PhoneNumber;
                mapdata.ReferredbyPHCName = item.AssignedByNavigation.Name;
                mapdata.Age = CommanFunction.GetAge(item.PatientCase.Patient.Dob);
                mapdata.Gender = item.PatientCase.Patient.Gender.Gender;
                mapdata.PatientID = item.PatientCase.Patient.PatientId;
                mapdata.id = item.PatientCase.Patient.Id;
                //mapdata.status = item.PatientCase.Patient.PatientStatus.PatientStatus;
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<GetTodayesPatientsDTO>> GetPastPatientsHistory(DoctorVM doctorVM)
        {
            var today = DateTime.Today.AddDays(-1);
            List<PatientQueue> masters = await _teleMedecineContext.PatientQueues
                .Include(d => d.PatientCase.Patient.Gender)
                .Include(c => c.AssignedByNavigation)
                .Include(a => a.PatientCase)
                .Include(b => b.PatientCase.Patient)
                .Where(a => a.CaseFileStatusId == 5 && a.AssignedDoctorId == doctorVM.DoctorID
                 && a.AssignedOn.Year <= today.Year
                && a.AssignedOn.Month <= today.Month
                && a.AssignedOn.Date < today.Date
                ).ToListAsync();

            var DTOList = new List<GetTodayesPatientsDTO>();
            foreach (var item in masters)
            {
                //GetTodayesPatientsDTO mapdata = _mapper.Map<GetTodayesPatientsDTO>(item);
                GetTodayesPatientsDTO mapdata = new GetTodayesPatientsDTO();
                mapdata.PatientName = item.PatientCase.Patient.FirstName + " " + item.PatientCase.Patient.LastName;
                mapdata.PhoneNumber = item.PatientCase.Patient.PhoneNumber;
                mapdata.ReferredbyPHCName = item.AssignedByNavigation.Name;
                mapdata.Age = CommanFunction.GetAge(item.PatientCase.Patient.Dob);
                mapdata.Gender = item.PatientCase.Patient.Gender.Gender;
                mapdata.PatientID = item.PatientCase.Patient.PatientId;
                mapdata.id = item.PatientCase.Patient.Id;
                //mapdata.status = item.PatientCase.Patient.PatientStatus.PatientStatus;
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<GetPatientCaseDetailsDTO> GetPatientCaseDetailsAsync(GetPatientCaseDetailsVM vm)
        {
            GetPatientCaseDetailsDTO getPatientCaseDetails = new GetPatientCaseDetailsDTO();
            getPatientCaseDetails.getPatientCaseDocumentDTOs = new List<PatientCaseDocDTO>();
            getPatientCaseDetails.getPatientCaseVitalsDTOs = new List<GetPatientCaseVitalsDTO>();
            PatientQueue patientQueue = await _teleMedecineContext.PatientQueues
                .Include(d => d.PatientCase.Patient.Gender)
                .Include(d => d.PatientCase.Patient.Idproof)
                .Include(c => c.AssignedByNavigation)
                .Include(a => a.PatientCase)
                .Include(b => b.PatientCase.Patient)
                .Where(a => a.CaseFileStatusId == 4 && a.PatientCaseId == vm.PatientCaseID
                //&& a.AssignedOn.Year <= today.Year
                //&& a.AssignedOn.Month <= today.Month
                //&& a.AssignedOn.Date < today.Date
                ).FirstOrDefaultAsync();

            List<PatientCaseVital> vitalMasters = await _teleMedecineContext.PatientCaseVitals.Include(a => a.Vital)
                .Where(a => a.PatientCaseId == vm.PatientCaseID).ToListAsync();


            List<PatientCaseDocument> patientCaseDocuments = await _teleMedecineContext.PatientCaseDocuments
                .Where(a => a.PatientCaseId == vm.PatientCaseID).ToListAsync();
            if (patientQueue!=null)
            {
                getPatientCaseDetails.PatientName = patientQueue.PatientCase.Patient.FirstName + " " + patientQueue.PatientCase.Patient.LastName;
                getPatientCaseDetails.PatientId = patientQueue.PatientCase.Patient.PatientId;
                getPatientCaseDetails.CaseFileNumber = patientQueue.PatientCase.CaseFileNumber;
                getPatientCaseDetails.CaseHeading = patientQueue.PatientCase.CaseHeading;
                getPatientCaseDetails.Symptom = patientQueue.PatientCase.Symptom;
                getPatientCaseDetails.Observation = patientQueue.PatientCase.Observation;
                getPatientCaseDetails.Allergies = patientQueue.PatientCase.Allergies;
                getPatientCaseDetails.FamilyHistory = patientQueue.PatientCase.FamilyHistory;
                getPatientCaseDetails.UpdatedBy = patientQueue.PatientCase.UpdatedBy;
                getPatientCaseDetails.UpdatedOn = patientQueue.PatientCase.UpdatedOn;
                getPatientCaseDetails.CreatedBy = patientQueue.PatientCase.CreatedBy;
                getPatientCaseDetails.CreatedOn = patientQueue.PatientCase.CreatedOn;


                getPatientCaseDetails.FirstName = patientQueue.PatientCase.Patient.FirstName;
                getPatientCaseDetails.LastName = patientQueue.PatientCase.Patient.LastName;
                getPatientCaseDetails.PhoneNumber = patientQueue.PatientCase.Patient.PhoneNumber;
                getPatientCaseDetails.Idproof = patientQueue.PatientCase.Patient.Idproof.IdproofType;
                getPatientCaseDetails.IdproofNumber = patientQueue.PatientCase.Patient.IdproofNumber;
                getPatientCaseDetails.Gender = patientQueue.PatientCase.Patient.Gender.Gender;
                getPatientCaseDetails.Photo = patientQueue.PatientCase.Patient.Photo;
                getPatientCaseDetails.Age = CommanFunction.GetAge(patientQueue.PatientCase.Patient.Dob);


                foreach (var item in patientCaseDocuments)
                {
                    getPatientCaseDetails.getPatientCaseDocumentDTOs.Add(
                        new PatientCaseDocDTO
                        {
                            Description = item.Description,
                            DocumentName = item.DocumentName,
                            DocumentPath = item.DocumentPath,
                            Id = item.Id,
                        }
                        );
                }
                foreach (var item in vitalMasters)
                {
                    getPatientCaseDetails.getPatientCaseVitalsDTOs.Add(
                        new GetPatientCaseVitalsDTO
                        {
                            Date = item.Date,
                            Unit = item.Vital.Unit,
                            Value = item.Value,
                            Vital = item.Vital.Vital
                        }
                        );
                }

            }

            return getPatientCaseDetails;
        }
        public async Task<bool> PostTreatmentPlan(TreatmentVM treatmentVM)
        {
            //check caseid
            PatientQueue patientQueue = await _teleMedecineContext.PatientQueues
              .Include(d => d.PatientCase.Patient.Gender)
              .Include(c => c.AssignedByNavigation)
              .Include(a => a.PatientCase)
              .Include(b => b.PatientCase.Patient)
              .Where(a => a.CaseFileStatusId == 4 && a.PatientCaseId == treatmentVM.PatientCaseID
              ).FirstOrDefaultAsync();
            if (patientQueue != null)
            {
                //update case table
                var patientCase = patientQueue.PatientCase;
                patientCase.Diagnosis = treatmentVM.Diagnosis;
                patientCase.Instruction = treatmentVM.Instruction;
                patientCase.Test = treatmentVM.Test;
                patientCase.Finding = treatmentVM.Findings;
                patientCase.Prescription = treatmentVM.Prescription;
                //add in medicine 
                //delete medicine
                var patientCaseMedicine = await _teleMedecineContext.PatientCaseMedicines.Where(m => m.PatientCaseId == treatmentVM.PatientCaseID).ToListAsync();
                foreach (var item in patientCaseMedicine)
                {
                    _teleMedecineContext.Remove(item);
                }
                foreach (var item in treatmentVM.medicineVMs)
                {
                    _teleMedecineContext.PatientCaseMedicines.Add(
                        new PatientCaseMedicine
                        {
                            Dose = item.Dose,
                            Medicine = item.Medicine,
                            PatientCaseId = treatmentVM.PatientCaseID
                        });
                }
                _teleMedecineContext.SaveChanges();
                return true;

            }
            return false;
        }
        public async Task<bool> DeleteNotification(long NotificationID)
        {
            Notification notification = await _teleMedecineContext.Notifications.Where(a => a.Id == NotificationID).FirstOrDefaultAsync();
            if (notification != null)
            {
                _teleMedecineContext.Notifications.Remove(notification);
                _teleMedecineContext.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<GetEHRDTO> GetEHR(GetEHRVM getEHRVM)
        {
            PatientQueue masters = await _teleMedecineContext.PatientQueues
                .Include(d => d.PatientCase.Patient.Gender)
                .Include(c => c.AssignedByNavigation)
                .Include(c => c.AssignedDoctor)
                .Include(c => c.AssignedDoctor.User)
                .Include(a => a.PatientCase)
                .Include(b => b.PatientCase.Patient)
                .Where(a => a.PatientCaseId == getEHRVM.PatientCaseID
                ).FirstOrDefaultAsync();
            GetEHRDTO getEHRDTO = new GetEHRDTO
            {
                Age = CommanFunction.GetAge(masters.PatientCase.Patient.Dob),
                Sex = masters.PatientCase.Patient.Gender.Gender,
                Diagnosis = masters.PatientCase.Diagnosis,
                PatientName = masters.PatientCase.Patient.FirstName + " " + masters.PatientCase.Patient.LastName,
                Prescription = masters.PatientCase.Finding,
                PriviousCaseDate = masters.PatientCase.CreatedOn,
                PriviousCaseLable = masters.PatientCase.CaseHeading,
                PriviousDoctor = masters.AssignedDoctor.User.Name,

            };
            var medicne = await _teleMedecineContext.PatientCaseMedicines
                .Where(a => a.PatientCaseId == getEHRVM.PatientCaseID).ToArrayAsync();
            foreach (var item in medicne)
            {
                getEHRDTO.medicineVMs.Add(new MedicineDTO
                {
                    Dose = item.Dose,
                    Medicine = item.Medicine,

                });
            }
            return getEHRDTO;

        }
        public async Task<bool> PatientAbsent(PatientAbsentVM patientAbsentVM)
        {
            PatientQueue patientQueue = await _teleMedecineContext.PatientQueues
            .Where(a => a.PatientCaseId == patientAbsentVM.CaseID).FirstOrDefaultAsync();
            if (patientQueue != null)
            {
                CaseFileStatusMaster CaseFileStatus = await _teleMedecineContext.CaseFileStatusMasters.Where(a => a.FileStatus.ToLower() == "Pending patient absent".ToLower()).FirstOrDefaultAsync();
                if (CaseFileStatus != null && patientQueue.AssignedDoctorId == patientAbsentVM.DoctorID)
                {
                    patientQueue.StatusOn = DateTime.Now;
                    patientQueue.CaseFileStatusId = CaseFileStatus.Id;
                    patientQueue.Comment = patientAbsentVM.Comment;
                    _teleMedecineContext.SaveChanges();
                    return true;
                }

            }
            return false;
        }
        public async Task<bool> ReferHigherFacility(PatientAbsentVM patientAbsentVM)
        {
            PatientQueue patientQueue = await _teleMedecineContext.PatientQueues
             .Where(a => a.PatientCaseId == patientAbsentVM.CaseID).FirstOrDefaultAsync();
            if (patientQueue != null)
            {
                PatientCase patientCase = await _teleMedecineContext.PatientCases.Where(a => a.Id == patientAbsentVM.CaseID).FirstOrDefaultAsync();
                CaseFileStatusMaster CaseFileStatus = await _teleMedecineContext.CaseFileStatusMasters.Where(a => a.FileStatus.ToLower() == "Closed".ToLower()).FirstOrDefaultAsync();
                if (CaseFileStatus != null && patientQueue.AssignedDoctorId == patientAbsentVM.DoctorID)
                {
                    patientQueue.StatusOn = DateTime.Now;
                    patientQueue.CaseFileStatusId = CaseFileStatus.Id;
                    patientQueue.Comment = patientQueue.Comment + " | " + patientAbsentVM.Comment;
                    patientCase.Prescription = "Referred to higher authority.";
                    patientQueue.Comment = patientQueue.Comment.TrimStart(' ').TrimStart('|');
                    _teleMedecineContext.SaveChanges();
                    return true;
                }

            }
            return false;
        }
        public async Task<List<SearchPatientsDTO>> SearchPatientDrDashBoard(SearchPatientVM searchPatientVM)
        {

            var today = DateTime.Today;
            var matches = from m in _teleMedecineContext.PatientQueues
                          .Include(d => d.PatientCase.Patient.Gender)
                          .Include(c => c.AssignedByNavigation)
                          .Include(a => a.PatientCase)
                          .Include(b => b.PatientCase.Patient)
                          where m.PatientCase.Patient.FirstName.Contains(searchPatientVM.PatientName)
                            && m.AssignedDoctorId == searchPatientVM.DoctorID
                            && m.AssignedOn.Year == today.Year
                            && m.AssignedOn.Month == today.Month
                            && m.AssignedOn.Day == today.Day
                          select m;

            var DTOList = new List<SearchPatientsDTO>();
            foreach (var item in matches)
            {
                //GetTodayesPatientsDTO mapdata = _mapper.Map<GetTodayesPatientsDTO>(item);
                SearchPatientsDTO mapdata = new SearchPatientsDTO();
                mapdata.PatientName = item.PatientCase.Patient.FirstName + " " + item.PatientCase.Patient.LastName;
                mapdata.PhoneNumber = item.PatientCase.Patient.PhoneNumber;
                mapdata.ReferredbyPHCName = item.AssignedByNavigation.Name;
                mapdata.Age = CommanFunction.GetAge(item.PatientCase.Patient.Dob);
                mapdata.Gender = item.PatientCase.Patient.Gender.Gender;
                mapdata.PatientID = item.PatientCase.Patient.PatientId;
                //mapdata.status = item.PatientCase.Patient.PatientStatus.PatientStatus;
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<SearchPatientsDTO>> SearchPatientDrHistory(SearchPatientVM searchPatientVM)
        {
            DateTime yesterday = DateTime.Now.AddDays(-1);
            var matches = from m in _teleMedecineContext.PatientQueues
                           .Include(d => d.PatientCase.Patient.Gender)
              .Include(c => c.AssignedByNavigation)
              .Include(a => a.PatientCase)
              .Include(b => b.PatientCase.Patient)
                          where m.PatientCase.Patient.FirstName.Contains(searchPatientVM.PatientName)
                            && m.AssignedDoctorId == searchPatientVM.DoctorID
                            && m.AssignedOn.Year <= yesterday.Year
                            && m.AssignedOn.Month <= yesterday.Month
                            && m.AssignedOn.Day <= yesterday.Day
                          select m;

            var DTOList = new List<SearchPatientsDTO>();
            foreach (var item in matches)
            {
                //GetTodayesPatientsDTO mapdata = _mapper.Map<GetTodayesPatientsDTO>(item);
                SearchPatientsDTO mapdata = new SearchPatientsDTO();
                mapdata.PatientName = item.PatientCase.Patient.FirstName + " " + item.PatientCase.Patient.LastName;
                mapdata.PhoneNumber = item.PatientCase.Patient.PhoneNumber;
                mapdata.ReferredbyPHCName = item.AssignedByNavigation.Name;
                mapdata.Age = CommanFunction.GetAge(item.PatientCase.Patient.Dob);
                mapdata.Gender = item.PatientCase.Patient.Gender.Gender;
                mapdata.PatientID = item.PatientCase.Patient.PatientId;
                //mapdata.status = item.PatientCase.Patient.PatientStatus.PatientStatus;
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<GetCaseLabelDTO>> GetCaseLabel(GetCaseLabelVM getCaseLabelVM)
        {
            List<PatientCase> masters = await _teleMedecineContext.PatientCases
                 .Where(a => a.PatientId == getCaseLabelVM.PatientID).ToListAsync();
            List<GetCaseLabelDTO> getCaseLabelDTOs = new List<GetCaseLabelDTO>();
            var medicne = await _teleMedecineContext.PatientCaseMedicines
                .Where(a => a.PatientCaseId == getCaseLabelVM.PatientID).ToArrayAsync();
            foreach (var item in masters)
            {
                getCaseLabelDTOs.Add(new GetCaseLabelDTO
                {
                    CaseDateTime = item.CreatedOn,
                    CaseLabel = item.CaseHeading,
                    CaseID = item.Id
                });
            }
            return getCaseLabelDTOs;
        }
        public async Task<List<PHCHospitalDTO>> GetListOfPHCHospitalZoneWise(GetListOfPHCHospitalVM getListOfPHCHospitalVM)
        {
            List<Phcmaster> masters = await _teleMedecineContext.Phcmasters
                .Where(a => a.ZoneId == getListOfPHCHospitalVM.ZoneID)
                .ToListAsync();
            var DTOList = new List<PHCHospitalDTO>();
            foreach (var item in masters)
            {
                PHCHospitalDTO mapdata = _mapper.Map<PHCHospitalDTO>(item);
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<GetTodayesPatientsDTO>> GetLatestReferred(DoctorVM doctorVM)
        {
            DateTime fromDatetime = DateTime.Now.AddHours(-1);

            List<PatientQueue> masters = await _teleMedecineContext.PatientQueues
                .Include(d => d.PatientCase.Patient.Gender)
                .Include(c => c.AssignedByNavigation)
                .Include(a => a.PatientCase)
                .Include(b => b.PatientCase.Patient)
                .Where(a => a.CaseFileStatusId == 4 && a.AssignedDoctorId == doctorVM.DoctorID
                //&& a.AssignedOn.Year == DateTime.Now.Year
                //&& a.AssignedOn.Month == DateTime.Now.Month
                //&& a.AssignedOn.Day == DateTime.Now.Day
                && a.AssignedOn > fromDatetime
                ).ToListAsync();

            var DTOList = new List<GetTodayesPatientsDTO>();
            foreach (var item in masters)
            {
                //GetTodayesPatientsDTO mapdata = _mapper.Map<GetTodayesPatientsDTO>(item);
                GetTodayesPatientsDTO mapdata = new GetTodayesPatientsDTO();
                mapdata.PatientName = item.PatientCase.Patient.FirstName + " " + item.PatientCase.Patient.LastName;
                mapdata.PhoneNumber = item.PatientCase.Patient.PhoneNumber;
                mapdata.ReferredbyPHCName = item.AssignedByNavigation.Name;
                mapdata.Age = CommanFunction.GetAge(item.PatientCase.Patient.Dob);
                mapdata.Gender = item.PatientCase.Patient.Gender.Gender;
                mapdata.PatientID = item.PatientCase.Patient.PatientId;
                mapdata.id = item.PatientCase.Patient.Id;
                //mapdata.status = item.PatientCase.Patient.PatientStatus.PatientStatus;
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<bool> UpdateIsDrOnline(UpdateIsDrOnlineVM updateIsOnlineDrVM)
        {
            DoctorMaster doctorMaster = await _teleMedecineContext.DoctorMasters.Where(a => a.Id == updateIsOnlineDrVM.DoctorID).FirstOrDefaultAsync();
            if (doctorMaster == null)
            {
                return false;
            }
            else
            {
                doctorMaster.IsOnline = updateIsOnlineDrVM.IsOnline;
                if (updateIsOnlineDrVM.IsOnline)
                {
                    doctorMaster.LastOnlineAt = DateTime.Now;
                }
                _teleMedecineContext.SaveChanges();
                return true;
            }
        }
        public async Task<bool> IsDrOnline(DoctorVM doctorVM)
        {
            DoctorMaster doctorMaster = await _teleMedecineContext.DoctorMasters.Where(a => a.Id == doctorVM.DoctorID).FirstOrDefaultAsync();
            if (doctorMaster == null)
            {
                return false;
            }
            else
            {
                return doctorMaster.IsOnline;
            }
        }
        public async Task<List<OnlineDrListDTO>> OnlineDrList(OnlineDrListVM doctorVM)
        {
            List<OnlineDrListDTO> onlineDrList = new List<OnlineDrListDTO>();
            var doctorMaster = await _teleMedecineContext
                .DoctorMasters
                .Include(a => a.Specialization)
                .Where(a => a.ZoneId == doctorVM.ZoneID && a.IsOnline == true).ToListAsync();
            foreach (var item in doctorMaster)
            {
                UserDetail userDetail = _teleMedecineContext.UserDetails.Where(a => a.UserId == item.UserId).FirstOrDefault();
                onlineDrList.Add(new OnlineDrListDTO
                {
                    DoctorID = item.Id,
                    DoctorFName = userDetail.FirstName,
                    DoctorMName = userDetail.MiddleName,
                    DoctorLName = userDetail.LastName,
                    Photo = userDetail.Photo,
                    Specialty = item.Specialization.Specialization
                });
            }
            return onlineDrList;
        }
        public async Task<bool> UpdateIsDrOnlineByUserLoginName(UpdateIsDrOnlineByUserLoginNameVM updateIsOnlineDrVM)
        {

            UserMaster userMaster = await _teleMedecineContext.UserMasters.Where(a => a.Email.ToLower() == updateIsOnlineDrVM.UserLoginName.ToLower()).FirstOrDefaultAsync();
            if (userMaster == null)
            {
                return false;
            }
            else
            {
                DoctorMaster doctorMaster = await _teleMedecineContext.DoctorMasters.Where(a => a.UserId == userMaster.Id).FirstOrDefaultAsync();
                if (doctorMaster == null)
                {
                    return false;
                }
                doctorMaster.IsOnline = updateIsOnlineDrVM.IsOnline;
                _teleMedecineContext.SaveChanges();
                return true;
            }
        }
        public async Task<DoctorMaster> AddDoctor(DoctorMaster doctorMaster, UserMaster userMaster, UserDetail userDetail)
        {
            int i = 0;
            int j = 0;
            int K = 0;
            DoctorMaster doctor = new DoctorMaster();
            //using (TeleMedecineContext context = new TeleMedecineContext())
            //{
            if (doctorMaster.SubSpecializationId == 0)
            {
                doctorMaster.SubSpecializationId = null;
            }

            using (var transaction = _teleMedecineContext.Database.BeginTransaction())
            {
                try
                {
                    await _teleMedecineContext.UserMasters.AddAsync(userMaster);
                    i = await _teleMedecineContext.SaveChangesAsync();
                    if (i > 0 && userMaster.Id > 0)
                    {
                        doctorMaster.UserId = userMaster.Id;
                        await _teleMedecineContext.DoctorMasters.AddAsync(doctorMaster);
                        j = await _teleMedecineContext.SaveChangesAsync();

                        userDetail.UserId = userMaster.Id;
                        await _teleMedecineContext.UserDetails.AddAsync(userDetail);
                        K = await _teleMedecineContext.SaveChangesAsync();
                    }
                    if (i > 0 && j > 0 && K > 0)
                    {
                        transaction.Commit();
                        doctor = await _teleMedecineContext.DoctorMasters.FirstOrDefaultAsync(a => a.Id == doctorMaster.Id);
                        //phcmasternew = (Phcmaster)newPHC;
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    string excp = ex.Message;
                    transaction.Rollback();
                    doctor = null;
                }
            }
            //}

            return doctor;
        }
        public async Task<string> CheckEmail(string Email)
        {
            UserDetail userDetail = await _teleMedecineContext.UserDetails.FirstOrDefaultAsync(a => a.EmailId.ToLower() == Email.ToLower());
            UserMaster userMaster = await _teleMedecineContext.UserMasters.FirstOrDefaultAsync(a => a.Email.ToLower() == Email.ToLower());
            if (userDetail != null)
            {
                return "Email already exists in UserDetail!";
            }
            if (userMaster != null)
            {
                return "Email already exists in User!";
            }
            return "";
        }
        public async Task<string> CheckMobile(string Mobile)
        {
            UserDetail userDetail = await _teleMedecineContext.UserDetails.FirstOrDefaultAsync(a => a.PhoneNumber == Mobile);
            UserMaster userMaster = await _teleMedecineContext.UserMasters.FirstOrDefaultAsync(a => a.Mobile == Mobile);
            if (userDetail != null)
            {
                return "Mobile already exists in UserDetail!";
            }
            if (userMaster != null)
            {
                return "Mobile already exists in User!";
            }
            return "";
        }
        public Task<DoctorMaster> Create(DoctorMaster model)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<DoctorMaster>> Get(Func<DoctorMaster, bool> where)
        {
            throw new NotImplementedException();
        }
        public Task<DoctorMaster> Update(DoctorMaster model)
        {
            throw new NotImplementedException();
        }
        public Task<DoctorMaster> UpdateOnly(DoctorMaster model)
        {
            throw new NotImplementedException();
        }
        Task<IEnumerable<DoctorMaster>> IRepository<DoctorMaster>.Get()
        {
            throw new NotImplementedException();
        }
        Task<DoctorMaster> IRepository<DoctorMaster>.Get(int id)
        {
            throw new NotImplementedException();
        }
        Task<List<DoctorMaster>> IRepository<DoctorMaster>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
