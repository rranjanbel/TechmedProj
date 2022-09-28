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
    public class PatientRepository : Repository<PatientMaster>, IPatientRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;
        public PatientRepository(ILogger<UserRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;


        }
        public async Task<PatientMaster> AddPatient(PatientMaster patientMaster)
        {
            PatientMaster updatedPatientMaster = new PatientMaster();
            try
            {
                //Setting setting = new Setting(); 
                if (patientMaster != null)
                {
                    //patientMaster.CreatedOn = DateTime.Now;
                    //patientMaster.UpdatedOn = DateTime.Now;
                    patientMaster.CreatedOn = UtilityMaster.GetLocalDateTime();
                    patientMaster.UpdatedOn = UtilityMaster.GetLocalDateTime();
                    if (patientMaster.Dob != null)
                    {
                        patientMaster.Dob = UtilityMaster.ConvertToLocalDateTime(patientMaster.Dob);
                    }
                    // patientMaster.PatientId = UtilityMaster.GetPatientNumber();
                    //patientMaster.PatientId = GetPatientId();
                    if (patientMaster.MobileNo == null || patientMaster.MobileNo == "")
                    {
                        patientMaster.MobileNo = " ";
                    }

                    if (patientMaster.Id == 0)
                    {
                        _logger.LogInformation($"Add Patient : call save method");
                        var patient = await _teleMedecineContext.PatientMasters.AddAsync(patientMaster);
                        System.Threading.Thread.Sleep(4000);//delay
                        int i = await _teleMedecineContext.SaveChangesAsync();
                        //updatedPatientMaster = await Create(patientMaster);

                        if (i > 0)
                        {
                            updatedPatientMaster = patientMaster;
                            _logger.LogInformation($"Add Patient : Patient added successfully");
                        }
                        else
                        {
                            _logger.LogInformation($"Add Patient : Patient did not add");
                        }


                        return updatedPatientMaster;
                    }
                    else
                    {
                        return updatedPatientMaster;
                    }
                }
                else
                {
                    _logger.LogInformation($"Add Patient : model get null value");
                    return updatedPatientMaster;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Add Patient : get exception " +ex.Message);
                //return updatedPatientMaster;
                _logger.LogError("Add Patient got exception when adding patient " + ex.Message);
                updatedPatientMaster = new PatientMaster();
                return updatedPatientMaster;

            }

        }

        public async Task<PatientAddStatusVM> CreatePatient(PatientMasterDTO patientMasterDTO,string webRootPath)
        {
            PatientMaster patientMaster = new PatientMaster();
            PatientAddStatusVM addStatusVM = new PatientAddStatusVM();
            string relativePathOfPatientImage = @"/MyFiles/Images/Patients/"; 
            try
            {

                //Setting setting = new Setting(); 
                if (patientMasterDTO != null)
                {
                    patientMaster = _mapper.Map<PatientMaster>(patientMasterDTO);
                    if (IsPatientExist(patientMaster))
                    {
                        addStatusVM.Status = "Fail";
                        addStatusVM.Message = "Patient already in system.";
                        addStatusVM.PatientMasterDTO = patientMasterDTO;
                        return addStatusVM;
                    }
                    else
                    {
                        if (webRootPath == String.Empty || webRootPath == null)
                        {
                            addStatusVM.Status = "Fail";
                            addStatusVM.Message = "webRootPath does not contain path.";
                            addStatusVM.PatientMasterDTO = patientMasterDTO;
                            return addStatusVM;
                        }
                        else
                        {
                            patientMaster.CreatedOn = UtilityMaster.GetLocalDateTime();
                            patientMaster.UpdatedOn = UtilityMaster.GetLocalDateTime();
                            if (patientMasterDTO.Dob != null)
                            {
                                patientMaster.Dob = UtilityMaster.ConvertToLocalDateTime(patientMaster.Dob);
                            }
                            
                            if (patientMaster.MobileNo == null || patientMaster.MobileNo == "")
                            {
                                patientMaster.MobileNo = " ";
                            }
                            // Save image of Patient 
                            string fileName = SaveImage(patientMasterDTO.Photo, webRootPath);
                            patientMaster.Photo = relativePathOfPatientImage + fileName;
                            // Get unique patient ID
                            patientMaster.PatientId = this.GetPatientId();

                            if( await _teleMedecineContext.PatientMasters.AnyAsync(a => a.PatientId == patientMaster.PatientId))
                            {
                                patientMaster.PatientId = this.GetPatientId();
                            }
                            
                            if (patientMaster.Id == 0)
                            {
                                _logger.LogInformation($"Add Patient : call save method");
                                _teleMedecineContext.Entry(patientMaster).State = EntityState.Added;
                                int i =  _teleMedecineContext.SaveChanges();                              

                                if (i > 0)
                                {
                                    patientMasterDTO = _mapper.Map<PatientMasterDTO>(patientMaster);
                                    _logger.LogInformation($"Add Patient : Patient added successfully.");
                                    patientMasterDTO.Age = UtilityMaster.GetAgeOfPatient(patientMaster.Dob);
                                    addStatusVM.Status = "Sucess";
                                    addStatusVM.Message = "Patient added successfully.";
                                    addStatusVM.PatientMasterDTO = patientMasterDTO;
                                    return addStatusVM;
                                }
                                else
                                {
                                    _logger.LogInformation($"Add Patient : Patient did not add");
                                    addStatusVM.Status = "Fail";
                                    addStatusVM.Message = "Patient did not add,Please contact admin.";
                                    addStatusVM.PatientMasterDTO = patientMasterDTO;
                                    return addStatusVM;
                                }
                            }
                            else
                            {
                                _logger.LogInformation($"Add Patient : Patient did not add.");
                                addStatusVM.Status = "Fail";
                                addStatusVM.Message = "Patient Id has value.Patient did not add.";
                                addStatusVM.PatientMasterDTO = patientMasterDTO;
                                return addStatusVM;
                            }
                        }
                    }
                }  
                else
                {
                    _logger.LogInformation($"Add Patient : model get null value");
                    PatientMasterDTO dto = new PatientMasterDTO();
                    addStatusVM.Status = "Fail";
                    addStatusVM.Message = "Add Patient : model get null value.";
                    addStatusVM.PatientMasterDTO = dto;
                    return addStatusVM;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Add Patient : get exception " +ex.Message);
                //return updatedPatientMaster;
                _logger.LogError("Add Patient got exception when adding patient " + ex.Message);                
                addStatusVM.Status = "Fail";
                addStatusVM.Message = "Add Patient got exception when adding patient.";
                addStatusVM.PatientMasterDTO = patientMasterDTO;
                return addStatusVM;

            }

        }

        public Task<bool> DeletePatient(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PatientMaster>> GetAllPatient()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TodaysPatientVM>> GetCheckedPatientList(int phcID)
        {
            int currentYear = UtilityMaster.GetLocalDateTime().Year;
            int currentMonth = UtilityMaster.GetLocalDateTime().Month;
            int currentDay = UtilityMaster.GetLocalDateTime().Day;
            List<TodaysPatientVM> todaysPatientList = new List<TodaysPatientVM>();
            List<TodaysPatientVM> todaysConsultantedPatientList = new List<TodaysPatientVM>();
            TodaysPatientVM todaysPatient;
            var Results = _teleMedecineContext.VisitedPatientsList.FromSqlInterpolated($"EXEC [dbo].[GetPatientList] @PHCID={phcID},@InputDate={UtilityMaster.GetLocalDateTime()},@IsConsultedPatient={1},@DocterID={0}");
            if (Results != null)
            {
                foreach (var item in Results)
                {
                    todaysPatient = new TodaysPatientVM();
                    todaysPatient.Age = item.Age;
                    todaysPatient.PatientName = item.PatientName;
                    todaysPatient.ID = item.ID;
                    todaysPatient.PhoneNumber = item.PhoneNumber;
                    todaysPatient.PatientID = item.PatientID;
                    todaysPatient.PHCUserID = item.Phcid;
                    todaysPatient.PHCUserName = item.Phcname;
                    todaysPatient.ReferredByPHCID = item.Phcid;
                    todaysPatient.ReferredByPHCName = item.Phcname;
                    todaysPatient.DocterID = item.DocterID;
                    todaysPatient.DoctorName = item.Doctor;
                    todaysPatient.Gender = item.Gender;
                    todaysPatient.CaseHeading = item.CaseHeading;
                    todaysPatient.DateOfRegistration = item.DateOfRegistration;
                    todaysPatient.CaseFileNumber = item.CaseFileNumber;
                    todaysPatientList.Add(todaysPatient);
                }
            }
            //var patientList = (from pm in _teleMedecineContext.PatientMasters where pm.CreatedOn.Value.Year == currentYear && pm.CreatedOn.Value.Month == currentMonth && pm.CreatedOn.Value.Day == currentDay
            //                   join phc in _teleMedecineContext.Phcmasters on pm.Phcid equals phc.Id
            //                   join pc in _teleMedecineContext.PatientCases on pm.Id equals pc.PatientId 
            //                   join pcq in _teleMedecineContext.PatientQueues on pc.Id equals pcq.PatientCaseId 
            //                   join d in _teleMedecineContext.DoctorMasters on pcq.AssignedDoctorId equals d.Id into dm
            //                   from doc in dm.DefaultIfEmpty()
            //                   join u in _teleMedecineContext.UserMasters on doc.UserId equals u.Id into um
            //                   from ud in um.DefaultIfEmpty()
            //                   where phc.Id == phcID && pcq.CaseFileStatusId == 5  // 5 - Closed
            //                   select new TodaysPatientVM
            //                   {
            //                       //Age = GetAge(pm.Dob),
            //                       Age = UtilityMaster.GetAgeOfPatient(pm.Dob),
            //                       PatientName = pm.FirstName + " " + pm.LastName,
            //                       ID = pm.Id,
            //                       PhoneNumber = pm.PhoneNumber,
            //                       PatientID = pm.PatientId,
            //                       PHCUserID = pm.Phcid,
            //                       PHCUserName = phc.Phcname,
            //                       ReferredByPHCID = pm.Phcid,
            //                       ReferredByPHCName = phc.Phcname,
            //                       DocterID = pcq.AssignedDoctorId > 0 ? pcq.AssignedDoctorId : 0,
            //                       DoctorName = ud.Name,
            //                       Gender = (pm.GenderId == 1 ? "Male" : "Female")
            //                   }).ToListAsync();
            //todaysPatientList = await patientList;
            //foreach (var item in todaysPatientList)
            //{
            //    if (item.DocterID > 0)
            //    {
            //        todaysConsultantedPatientList.Add(item);
            //    }
            //}

            return todaysPatientList;
        }

        public async Task<PatientMaster> GetPatientByID(int Id)
        {
            var patientMaster = new PatientMaster();
            if (Id > 0)
            {
                patientMaster = await _teleMedecineContext.PatientMasters.FirstOrDefaultAsync(a => a.Id == Id);
                return patientMaster;
            }
            else
            {
                return patientMaster;
            }


        }

        public Task<List<PatientMaster>> GetPendingPatientList(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TodaysPatientVM>> GetTodaysPatientList(int phcID)
        {
            //List<SPResultGetPatientDetails> sPResultGetPatientDetails = GetSPResult(phcID);
            int currentYear = UtilityMaster.GetLocalDateTime().Year;
            int currentMonth = UtilityMaster.GetLocalDateTime().Month;
            int currentDay = UtilityMaster.GetLocalDateTime().Day;
            int[] ids = { 1, 2, 4 };// 1- Pending Patient Absent, 2- Pending Doctor Absent, 4- Queued
            List<TodaysPatientVM> todaysPatientList = new List<TodaysPatientVM>();
            List<TodaysPatientVM> todaysNocConPatientList = new List<TodaysPatientVM>();
            TodaysPatientVM todaysPatient;
            var Results = _teleMedecineContext.VisitedPatientsList.FromSqlInterpolated($"EXEC [dbo].[GetPatientList] @PHCID={phcID},@InputDate={UtilityMaster.GetLocalDateTime()},@IsConsultedPatient={0},@DocterID={0}");
            if (Results != null)
            {
                foreach (var item in Results)
                {
                    todaysPatient = new TodaysPatientVM();
                    todaysPatient.Age = item.Age;
                    todaysPatient.PatientName = item.PatientName;
                    todaysPatient.ID = item.ID;
                    todaysPatient.PhoneNumber = item.PhoneNumber;
                    todaysPatient.PatientID = item.PatientID;
                    todaysPatient.PHCUserID = item.Phcid;
                    todaysPatient.PHCUserName = item.Phcname;
                    todaysPatient.ReferredByPHCID = item.Phcid;
                    todaysPatient.ReferredByPHCName = item.Phcname;
                    todaysPatient.DocterID = item.DocterID;
                    todaysPatient.DoctorName = item.Doctor;
                    todaysPatient.Gender = item.Gender;
                    todaysPatient.CaseHeading = item.CaseHeading;
                    todaysPatient.DateOfRegistration = item.DateOfRegistration;
                    todaysPatientList.Add(todaysPatient);
                }
            }

            //var patientList = (from pm in _teleMedecineContext.PatientMasters
            //                   where pm.CreatedOn.Value.Year == currentYear && pm.CreatedOn.Value.Month == currentMonth && pm.CreatedOn.Value.Day == currentDay
            //                   join phc in _teleMedecineContext.Phcmasters on pm.Phcid equals phc.Id
            //                   join pc in _teleMedecineContext.PatientCases on pm.Id equals pc.PatientId 
            //                   join pcq in _teleMedecineContext.PatientQueues on pc.Id equals pcq.PatientCaseId into pcqd
            //                   from pq in pcqd.DefaultIfEmpty() 
            //                   join d in _teleMedecineContext.DoctorMasters on pq.AssignedDoctorId equals d.Id into dm
            //                   from doc in dm.DefaultIfEmpty()
            //                   join u in _teleMedecineContext.UserMasters on doc.UserId equals u.Id into um
            //                   from ud in um.DefaultIfEmpty()
            //                       // where phc.Id == phcID && ids.Contains(pq.CaseFileStatusId)
            //                   where phc.Id == phcID 
            //                   select new TodaysPatientVM
            //                   { 
            //                       //Age = GetAge(pm.Dob),
            //                       Age = UtilityMaster.GetAgeOfPatient(pm.Dob),
            //                       PatientName = pm.FirstName + " " + pm.LastName,
            //                       ID = pm.Id,
            //                       PhoneNumber = pm.PhoneNumber,
            //                       PatientID = pm.PatientId,
            //                       PHCUserID = pm.Phcid,
            //                       PHCUserName = phc.Phcname,
            //                       ReferredByPHCID = pm.Phcid,
            //                       ReferredByPHCName = phc.Phcname,
            //                       DocterID = pq.AssignedDoctorId > 0 ? pq.AssignedDoctorId : 0,
            //                       DoctorName = ud.Name,
            //                       Gender = (pm.GenderId == 1 ? "Male" : "Female")
            //                   }).ToListAsync();
            //todaysPatientList = await patientList;
            //foreach (var item in todaysPatientList)
            //{
            //    if (item.DocterID == 0)
            //    {
            //        todaysNocConPatientList.Add(item);
            //    }
            //}



            return todaysPatientList;
        }


        public async Task<List<GetPendingByDoctorPatientDTO>> GetPendingByDoctorPatientList(int phcID)
        {
            List<GetPendingByDoctorPatientDTO> onlineDrList = new List<GetPendingByDoctorPatientDTO>();
            var doctorMaster = await _teleMedecineContext
                .DoctorMasters
                .Include(a => a.Specialization)
                .Where(a => a.IsOnline == true).ToListAsync();

            var patientMasters = await _teleMedecineContext
              .PatientQueues
              .Include(a => a.PatientCase)
              .Include(a => a.PatientCase.Patient)
              .Where(a => a.CaseFileStatusId == 2 && a.PatientCase.Patient.Phcid==phcID).ToListAsync();
            foreach (var item in doctorMaster)
            {
                UserDetail userDetail = _teleMedecineContext.UserDetails.Where(a => a.UserId == item.UserId).FirstOrDefault();
                GetPendingByDoctorPatientDTO getPendingByDoctorPatientDTO = new GetPendingByDoctorPatientDTO
                {
                    DoctorID = item.Id,
                    DoctorFName = userDetail.FirstName,
                    DoctorMName = userDetail.MiddleName,
                    DoctorLName = userDetail.LastName,
                    Photo = userDetail.Photo,
                    Specialty = item.Specialization.Specialization
                };

                //add patient
                var pForDoctor = patientMasters.Where(a => a.AssignedDoctorId == item.Id).ToList();
                int i = 1;
                foreach (var patient in pForDoctor)
                {
                    getPendingByDoctorPatientDTO.patientDTO.Add(new PatientDTO
                    {
                        Age = UtilityMaster.GetDetailsAgeOfPatient(patient.PatientCase.Patient.Dob),
                        CaseLabel = patient.PatientCase.CaseHeading,
                        LastReferredTime = null, //patient.PatientCase.Patient.CreatedOn,
                        PatientName = patient.PatientCase.Patient.FirstName + " " + patient.PatientCase.Patient.LastName,
                        PHCName = patient.PatientCase.Patient.Phc.Phcname,
                        RegisteredTime = patient.PatientCase.Patient.CreatedOn,
                        SerialNo = i++.ToString(),
                        Sex = patient.PatientCase.Patient.Gender.Gender,
                    });// ;// ; ;
                }
                onlineDrList.Add(getPendingByDoctorPatientDTO);
            }
            return onlineDrList;
        }

        public Task<List<PatientMaster>> GetUnCheckedPatientList(int Id)
        {
            throw new NotImplementedException();
        }

        public bool IsPatientExist(PatientMaster patientMaster)
        {
            bool result = _teleMedecineContext.PatientMasters.Any(a => a.FirstName == patientMaster.FirstName && a.LastName == patientMaster.LastName && a.MobileNo == patientMaster.MobileNo);
            return result;
        }

        public Task<PatientMaster> UpdatePatient(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<PHCPatientCount> GetPatientCount(int phcID)
        {
            PHCPatientCount pHCPatientCount = new PHCPatientCount();
            TodaysPatientCountVM todaysPatientCountVM;
            List<TodaysPatientCountVM> todaysPatientCountVMList = new List<TodaysPatientCountVM>();
            //int currentYear = DateTime.Now.Year;
            //int currentMonth = DateTime.Now.Month;
            //int currentDay = DateTime.Now.Day;
            //List<TodaysPatientVM> todaysPatientList = new List<TodaysPatientVM>();
            //List<TodaysPatientVM> todaysConsultantedPatientList = new List<TodaysPatientVM>();           
            //var patientList = (from pm in _teleMedecineContext.PatientMasters
            //                   where pm.CreatedOn.Value.Year == currentYear && pm.CreatedOn.Value.Month == currentMonth && pm.CreatedOn.Value.Day == currentDay
            //                   join phc in _teleMedecineContext.Phcmasters on pm.Phcid equals phc.Id
            //                   join pc in _teleMedecineContext.PatientCases on pm.Id equals pc.PatientId
            //                   join pcq in _teleMedecineContext.PatientQueues on pc.Id equals pcq.Id into pcqd
            //                   from pq in pcqd.DefaultIfEmpty()
            //                   join d in _teleMedecineContext.DoctorMasters on pq.AssignedDoctorId equals d.Id into dm
            //                   from doc in dm.DefaultIfEmpty()
            //                   join u in _teleMedecineContext.UserMasters on doc.UserId equals u.Id into um
            //                   from ud in um.DefaultIfEmpty()
            //                   where phc.Id == phcID
            //                   select new TodaysPatientVM
            //                   {
            //                       //Age = GetAge(pm.Dob),
            //                       Age = UtilityMaster.GetAgeOfPatient(pm.Dob),
            //                       PatientName = pm.FirstName + " " + pm.LastName,
            //                       ID = pm.Id,
            //                       PhoneNumber = pm.PhoneNumber,
            //                       PatientID = pm.PatientId,
            //                       PHCUserID = pm.Phcid,
            //                       PHCUserName = phc.Phcname,
            //                       ReferredByPHCID = pm.Phcid,
            //                       ReferredByPHCName = phc.Phcname,
            //                       DocterID = pq.AssignedDoctorId > 0 ? pq.AssignedDoctorId : 0,
            //                       DoctorName = ud.Name,
            //                       Gender = (pm.GenderId == 1 ? "Male" : "Female")
            //                   }).ToListAsync();
            //todaysPatientList = await patientList;

            //if (todaysPatientList.Count > 0)
            //{
            //    pHCPatientCount.PHCName = todaysPatientList.Select(s => s.ReferredByPHCName).FirstOrDefault();
            //    pHCPatientCount.ID = todaysPatientList.Select(s => s.ReferredByPHCID).FirstOrDefault();
            //    pHCPatientCount.TotalPatients = _teleMedecineContext.PatientMasters.Where(p => p.CreatedOn.Value.Year == currentYear && p.CreatedOn.Value.Month == currentMonth && p.CreatedOn.Value.Day == currentDay).Count();
            //    pHCPatientCount.TotalConsulted = _teleMedecineContext.PatientQueues.Include(p => p.PatientCase).Include(p => p.PatientCase.Patient).Where(p => p.StatusOn.Year == currentYear && p.StatusOn.Month == currentMonth && p.StatusOn.Day == currentDay && p.CaseFileStatusId == 5).Count();
            //    pHCPatientCount.TotalPending = _teleMedecineContext.PatientQueues.Include(p => p.PatientCase).Include(p => p.PatientCase.Patient).Where(p => p.StatusOn.Year == currentYear && p.StatusOn.Month == currentMonth && p.StatusOn.Day == currentDay && p.CaseFileStatusId != 5).Count();
            //}
            //else
            //{
            //    pHCPatientCount.PHCName = "";
            //    pHCPatientCount.ID = 0;
            //    pHCPatientCount.TotalPatients = 0;
            //    pHCPatientCount.TotalConsulted = 0;
            //    pHCPatientCount.TotalPending = 0;
            //}
            try
            {
                var Results = _teleMedecineContext.TodaysPatientCount.FromSqlInterpolated($"EXEC [dbo].[GetTotalCountOfPatientPHCWise] @PHCID={phcID}");
                if (Results != null)
                {
                    foreach (var item in Results.ToList())
                    {
                        todaysPatientCountVM = new TodaysPatientCountVM();
                        //todaysPatientCountVM.ID = item.ID;
                        todaysPatientCountVM.PHCID = item.PHCID;
                        todaysPatientCountVM.PHCName = item.PHCName;
                        todaysPatientCountVM.Count = item.Count;
                        todaysPatientCountVM.Type = item.Type;
                        todaysPatientCountVM.Description = item.Description;
                        todaysPatientCountVMList.Add(todaysPatientCountVM);
                    }

                    pHCPatientCount.PHCName = todaysPatientCountVMList.Select(s => s.PHCName).FirstOrDefault();
                    pHCPatientCount.ID = todaysPatientCountVMList.Select(s => s.PHCID).FirstOrDefault();
                    pHCPatientCount.TotalPatients = todaysPatientCountVMList.Where(s => s.Type == 1).Select(a => a.Count).FirstOrDefault();
                    pHCPatientCount.TotalConsulted = todaysPatientCountVMList.Where(s => s.Type == 2).Select(a => a.Count).FirstOrDefault();
                    pHCPatientCount.TotalPending = todaysPatientCountVMList.Where(s => s.Type == 3 || s.Type == 4).Select(a => a.Count).FirstOrDefault();

                }
                else
                {
                    pHCPatientCount.PHCName = "";
                    pHCPatientCount.ID = 0;
                    pHCPatientCount.TotalPatients = 0;
                    pHCPatientCount.TotalConsulted = 0;
                    pHCPatientCount.TotalPending = 0;
                }

            }
            catch (Exception ex)
            {
                string strMesg = ex.Message;
                throw;
            }



            return pHCPatientCount;
        }

        public async Task<List<TodaysPatientVM>> GetSearchedTodaysPatientList(string patientName)
        {
            int currentYear = UtilityMaster.GetLocalDateTime().Year;
            int currentMonth = UtilityMaster.GetLocalDateTime().Month;
            int currentDay = UtilityMaster.GetLocalDateTime().Day;
            List<TodaysPatientVM> todaysPatientList = new List<TodaysPatientVM>();
            var patientList = (from pm in _teleMedecineContext.PatientMasters
                               where pm.CreatedOn.Value.Year == currentYear && pm.CreatedOn.Value.Month == currentMonth && pm.CreatedOn.Value.Day == currentDay && pm.FirstName.Contains(patientName) || pm.LastName.Contains(patientName)
                               join pc in _teleMedecineContext.PatientCases on pm.Id equals pc.PatientId into patientcase
                               from pci in patientcase.DefaultIfEmpty()

                               join phc in _teleMedecineContext.Phcmasters on pci.CreatedBy equals phc.Id into phcmasters
                               from phcc in phcmasters.DefaultIfEmpty()

                               join pcq in _teleMedecineContext.PatientQueues on pci.Id equals pcq.Id into pcqd
                               from pq in pcqd.DefaultIfEmpty()
                               join d in _teleMedecineContext.DoctorMasters on pq.AssignedDoctorId equals d.Id into dm
                               from doc in dm.DefaultIfEmpty()
                               join u in _teleMedecineContext.UserMasters on doc.UserId equals u.Id into um
                               from ud in um.DefaultIfEmpty()
                                   //where pm.FirstName.Contains(patientName) || pm.LastName.Contains(patientName)
                               select new TodaysPatientVM
                               {
                                   //Age = GetAge(pm.Dob),
                                   Age = UtilityMaster.GetAgeOfPatient(pm.Dob),
                                   PatientName = pm.FirstName + " " + pm.LastName,
                                   ID = pm.Id,
                                   PhoneNumber = pm.PhoneNumber,
                                   PatientID = pm.PatientId,
                                   PHCUserID = pm.Phcid,
                                   PHCUserName = phcc.Phcname,
                                   ReferredByPHCID = pm.Phcid,
                                   ReferredByPHCName = phcc.Phcname,
                                   DocterID = pq.AssignedDoctorId > 0 ? pq.AssignedDoctorId : 0,
                                   DoctorName = ud.Name,
                                   Gender = (pm.GenderId == 1 ? "Male" : "Female")
                               }).ToListAsync();
            todaysPatientList = await patientList;

            return todaysPatientList;
        }

        public int GetAge(DateTime dateofbirth)
        {
            DateTime dtToday = UtilityMaster.GetLocalDateTime().Date;
            DateTime dtOfBirth = dateofbirth.Date;
            TimeSpan diffResult = dtToday - dtOfBirth;
            double totalDays = diffResult.TotalDays;

            if (totalDays > 365)
            {
                int year = (int)(totalDays / 365);
                return year;
            }
            else
            {
                return 0;
            }

        }

        public long GetPatientId()
        {
            Int64 currentNo = 0;
            int result = 0;
           Setting setting = new Setting();
            try
            {
                setting = _teleMedecineContext.Settings.FirstOrDefault( a => a.Id ==1);
                setting.PatientNumber = setting.PatientNumber + 1;
                _teleMedecineContext.Entry(setting).State = EntityState.Modified;
                result = _teleMedecineContext.SaveChanges();
                if (result > 0)
                {
                    setting = _teleMedecineContext.Settings.FirstOrDefault();
                    currentNo = setting.PatientNumber;
                }
                else
                {
                    currentNo = 0;
                }
                return currentNo;
            }
            catch (Exception)
            {
                throw;
            }
           

            
            //Int64 patientSerNo = 0;
            //patientSerNo = _teleMedecineContext.Settings.Select(a => a.PatientNumber).FirstOrDefault();
            //if (patientSerNo >= 0)
            //{
            //    currentNo = patientSerNo;
            //    setting = _teleMedecineContext.Settings.FirstOrDefault();
            //    if (setting != null)
            //    {
            //        setting.PatientNumber = currentNo + 1;
            //    }
            //    try
            //    {
            //        _teleMedecineContext.Entry(setting).State = EntityState.Modified;
            //        var result = _teleMedecineContext.SaveChangesAsync();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.ToString());
            //        throw;
            //    }
            //    return (currentNo + 1);
            //}
            //return 0;
        }

        public List<SPResultGetPatientDetails> GetSPResult(int patientId)
        {
            int PatientID = patientId;
            List<SPResultGetPatientDetails> sPResultGetPatientDetails = new List<SPResultGetPatientDetails>();
            SPResultGetPatientDetails sPResultGetPatientDetail;
            var Results = _teleMedecineContext.SPResultGetPatientDetails.FromSqlInterpolated($"EXEC [dbo].[GetPatientDetails] @PatientID ={PatientID}");
            foreach (var item in Results)
            {
                sPResultGetPatientDetail = new SPResultGetPatientDetails();
                sPResultGetPatientDetail.PhoneNo = item.PhoneNo;
                sPResultGetPatientDetail.PatientCreatedBy = item.PatientCreatedBy;
                sPResultGetPatientDetail.PatientName = item.PatientName;
                sPResultGetPatientDetail.MOName = item.MOName;
                sPResultGetPatientDetail.MailID = item.MailID;
                sPResultGetPatientDetail.BlockName = item.BlockName;
                sPResultGetPatientDetail.Cluster = item.Cluster;
                sPResultGetPatientDetail.PHCAddress = item.PHCAddress;
                sPResultGetPatientDetail.Docter = item.Docter;
                sPResultGetPatientDetail.PHCName = item.PHCName;
                sPResultGetPatientDetails.Add(sPResultGetPatientDetail);
            }
            return sPResultGetPatientDetails;
        }


        public async Task<List<PatientViewModel>> GetYesterdaysPatientList(int phcID)
        {
            int currentYear = UtilityMaster.GetLocalDateTime().Year;
            int currentMonth = UtilityMaster.GetLocalDateTime().Month;
            int currentDay = UtilityMaster.GetLocalDateTime().Day;
            DateTime yesterday = DateTime.Today.AddDays(-1);
            List<PatientViewModel> patientList = new List<PatientViewModel>();
            PatientViewModel todaysPatient;
            var Results = _teleMedecineContext.VisitedPatientsList.FromSqlInterpolated($"EXEC [dbo].[GetPatientList] @PHCID={phcID},@InputDate={yesterday},@IsConsultedPatient={0},@DocterID={0}");
            if (Results != null)
            {
                foreach (var item in Results)
                {
                    todaysPatient = new PatientViewModel();
                    todaysPatient.Age = item.Age;
                    todaysPatient.PatientName = item.PatientName;
                    todaysPatient.ID = item.ID;
                    todaysPatient.PhoneNumber = item.PhoneNumber;
                    todaysPatient.PatientID = item.PatientID;
                    todaysPatient.PHCUserID = item.Phcid;
                    todaysPatient.PHCUserName = item.Phcname;
                    todaysPatient.ReferredByPHCID = item.Phcid;
                    todaysPatient.ReferredByPHCName = item.Phcname;
                    todaysPatient.DocterID = item.DocterID;
                    todaysPatient.DoctorName = item.Doctor;
                    todaysPatient.Gender = item.Gender;
                    todaysPatient.CaseHeading = item.CaseHeading;
                    todaysPatient.DateOfRegistration = item.DateOfRegistration;
                    todaysPatient.Phcname = item.Phcname;

                    patientList.Add(todaysPatient);
                }
            }
            //var patientResult = (from pm in _teleMedecineContext.PatientMasters
            //                   where pm.CreatedOn.Value.Year == currentYear && pm.CreatedOn.Value.Month == currentMonth && pm.CreatedOn.Value.Day == currentDay-1
            //                   join phc in _teleMedecineContext.Phcmasters on pm.Phcid equals phc.Id
            //                   join pc in _teleMedecineContext.PatientCases on pm.Id equals pc.PatientId into patientcase
            //                   from pci in patientcase.DefaultIfEmpty()
            //                   join pcq in _teleMedecineContext.PatientQueues on pci.Id equals pcq.Id into pcqd
            //                   from pq in pcqd.DefaultIfEmpty()
            //                   join d in _teleMedecineContext.DoctorMasters on pq.AssignedDoctorId equals d.Id into dm
            //                   from doc in dm.DefaultIfEmpty()
            //                   join u in _teleMedecineContext.UserMasters on doc.UserId equals u.Id into um
            //                   from ud in um.DefaultIfEmpty()
            //                   where phc.Id == phcID
            //                   select new PatientViewModel
            //                   {
            //                       //Age = GetAge(pm.Dob),
            //                       Age = UtilityMaster.GetAgeOfPatient(pm.Dob),
            //                       PatientName = pm.FirstName + " " + pm.LastName,
            //                       ID = pm.Id,
            //                       PhoneNumber = pm.PhoneNumber,
            //                       PatientID = pm.PatientId,
            //                       PHCUserID = pm.Phcid,
            //                       PHCUserName = phc.Phcname,
            //                       ReferredByPHCID = pm.Phcid,
            //                       ReferredByPHCName = phc.Phcname,
            //                       DocterID = pq.AssignedDoctorId > 0 ? pq.AssignedDoctorId : 0,
            //                       DoctorName = ud.Name,
            //                       Gender = (pm.GenderId == 1 ? "Male" : "Female")
            //                   }).ToListAsync();
            //patientList = await patientResult;


            return patientList;
        }

        public string SaveImage(string ImgBase64Str, string rootPath)
        {
            string contentRootPath = rootPath;
            string path = @"\\MyStaticFiles\\Images\\Patients\\";
            path = contentRootPath + path;

            //Create     

            var myfilename = string.Format(@"{0}", Guid.NewGuid());

            //Generate unique filename
            string filepath = path + myfilename + ".jpeg";// png
            var bytess = Convert.FromBase64String(ImgBase64Str);
            using (var imageFile = new FileStream(filepath, FileMode.Create))
            {
                imageFile.Write(bytess, 0, bytess.Length);
                imageFile.Flush();
            }
            myfilename = myfilename + ".jpeg";
            return myfilename;
        }

        public List<PatientSearchResultVM> GetAdvanceSearchPatient(AdvanceSearchPatientVM searchParameter)
        {
            List<PatientSearchResultVM> patientSearchResults = new List<PatientSearchResultVM>();
            PatientSearchResultVM searchResult;
            int? PHCID = 0;
            //string? PatientFirstName = string.Empty;
            //string? PatientLastName = string.Empty;
            string? PatientName = string.Empty;
            long? PatientId = 0;
            string? contractNo = string.Empty;
            int? genderId = 0;
            DateTime? DateOfRegistration = DateTime.MinValue;
            DateTime? DateOfBirth = DateTime.MinValue;
            if (searchParameter != null)
            {
                if (searchParameter.PHCID > 0)
                    PHCID = searchParameter.PHCID;
                else
                    PHCID = null;

                if (searchParameter.PatientName == "")
                {
                    PatientName = null;
                    //PatientFirstName = null;
                    //PatientLastName = null;
                }
                else
                {
                    PatientName = searchParameter.PatientName;
                    //string[] patient = searchParameter.PatientName.Split(" ");
                    //if (patient.Length > 0)
                    //{
                    //    if (patient.Length == 2)
                    //    {
                    //        PatientFirstName = patient[0].ToString();
                    //        PatientLastName = patient[1].ToString();

                    //        if (PatientFirstName == "")
                    //        {
                    //            PatientFirstName = null;
                    //        }
                    //        if (PatientLastName == "")
                    //        {
                    //            PatientLastName = null;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        PatientFirstName = patient[0].ToString();
                    //        PatientLastName = null;
                    //    }

                    //}


                }

                if (searchParameter.PatientUID > 0)
                    PatientId = searchParameter.PatientUID;
                else
                    PatientId = null;

                if (searchParameter.ContactNo == "")
                    contractNo = null;
                else
                    contractNo = searchParameter.ContactNo;

                //if (searchParameter.ContactNo.ToLower().Trim() == "string")
                //    contractNo = null;
                //else
                //    contractNo = searchParameter.ContactNo;

                if (searchParameter.GenderId > 0)
                    genderId = searchParameter.GenderId;
                else
                    genderId = null;

                if (searchParameter.DateOfRegistration == null)
                    DateOfRegistration = null;
                else
                {
                    DateOfRegistration = UtilityMaster.ConvertToLocalDateTime(searchParameter.DateOfRegistration.Value);
                }


                if (searchParameter.DateOfBirth == null)
                    DateOfBirth = null;
                else
                    DateOfBirth = UtilityMaster.ConvertToLocalDateTime(searchParameter.DateOfBirth.Value);
            }

            var Results = _teleMedecineContext.PatientSearchResults
                .FromSqlInterpolated($"EXEC [dbo].[AdvanceSearchOfPatients] @PHCID ={PHCID},@PatientName={PatientName},@PatientUID={PatientId},@ContactNo={contractNo},@GenderId={genderId},@DateOfRegistration={DateOfRegistration},@DateOfBirth={DateOfBirth}");
            foreach (var item in Results)
            {
                searchResult = new PatientSearchResultVM();
                searchResult.PatientName = item.PatientName;
                searchResult.PatientID = item.PatientID;
                searchResult.Gender = item.Gender;
                searchResult.Age = item.Age;
                searchResult.PhoneNumber = item.PhoneNumber;
                searchResult.ID = item.ID;
                searchResult.DateOfRegistration = item.DateOfRegistration;
                searchResult.Phcname = item.Phcname;
                patientSearchResults.Add(searchResult);
            };
            return patientSearchResults.OrderByDescending(o => o.PatientID).ToList();
        }

        public async Task<List<SpecializationDTO>> GetSuggestedSpcialiazationByPatientCaseID(int Id)
        {
            int ageOfPatient = 0;
            int[] specIds;
            List<SpecializationDTO> specializations = new List<SpecializationDTO>();
            List<SpecializationMaster> specializationMasters = new List<SpecializationMaster>();
            try
            {
                var patient = _teleMedecineContext.PatientMasters.FirstOrDefault(a => a.Id == Id);
                if (patient != null)
                {
                    string fullAge = UtilityMaster.GetDetailsAgeOfPatient(patient.Dob);
                    ageOfPatient = UtilityMaster.GetAgeInYearOnly(patient.Dob);
                    if (fullAge.Contains("Years") && ageOfPatient <= 14)
                    {
                        specIds = _teleMedecineContext.AgeGroupMasters.Where(a => a.AgeMaxLimit < 15 && a.GenderID == patient.GenderId && a.DaysOrYear == 2).Select(s => s.SpecializationID).ToArray();
                        specializationMasters = await _teleMedecineContext.SpecializationMasters.Where(a => specIds.Contains(a.Id)).ToListAsync();
                    }
                    else if (fullAge.Contains("Months") && ageOfPatient <= 14)
                    {
                        specIds = _teleMedecineContext.AgeGroupMasters.Where(a => a.AgeMaxLimit < 15 && a.GenderID == patient.GenderId && a.DaysOrYear == 2).Select(s => s.SpecializationID).ToArray();
                        specializationMasters = await _teleMedecineContext.SpecializationMasters.Where(a => specIds.Contains(a.Id)).ToListAsync();
                    }
                    else if (fullAge.Contains("Days") && ageOfPatient <= 28)
                    {
                        specIds = _teleMedecineContext.AgeGroupMasters.Where(a => a.AgeMaxLimit < 29 && a.GenderID == patient.GenderId && a.DaysOrYear == 1).Select(s => s.SpecializationID).ToArray();
                        specializationMasters = await _teleMedecineContext.SpecializationMasters.Where(a => specIds.Contains(a.Id)).ToListAsync();
                    }                   
                    else
                    {
                        specIds = _teleMedecineContext.AgeGroupMasters.Where(a => a.AgeMaxLimit > 14 && a.GenderID == patient.GenderId && a.DaysOrYear == 2).Select(s => s.SpecializationID).ToArray();
                        specializationMasters = await _teleMedecineContext.SpecializationMasters.Where(a => specIds.Contains(a.Id)).ToListAsync();
                    }
                    foreach (var item in specializationMasters)
                    {
                        SpecializationDTO specializationDTO = new SpecializationDTO();
                        specializationDTO = _mapper.Map<SpecializationDTO>(item);
                        specializations.Add(specializationDTO);
                    }
                }

                return specializations;
            }
            catch (Exception ex)
            {
                string expMessage = ex.Message;
                throw;
            }


        }
    }
}
