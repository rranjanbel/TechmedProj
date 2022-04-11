using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;

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
            //Setting setting = new Setting(); 
            if (patientMaster != null)
            {
                if (patientMaster.CreatedBy == 0)
                    patientMaster.CreatedBy = 2;
                if (patientMaster.UpdatedBy == 0)
                    patientMaster.UpdatedBy = 2;
                patientMaster.CreatedOn = DateTime.Now;
                patientMaster.UpdatedOn = DateTime.Now;
                //patientMaster.PatientId = setting.PatientNumber();

                if (patientMaster.Id == 0)
                {
                    _teleMedecineContext.Add(patientMaster);
                    int i = await _teleMedecineContext.SaveChangesAsync();
                    if (i > 0)
                    {
                        _logger.LogInformation($"Add Patient : Patient added successfully");
                    }

                    updatedPatientMaster = patientMaster;
                    return updatedPatientMaster;
                }
                else
                {
                    return patientMaster;
                }
            }
            else
            {
                _logger.LogInformation($"Add Patient : model get null value");
                return patientMaster;
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
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;
            List<TodaysPatientVM> todaysPatientList = new List<TodaysPatientVM>();
            List<TodaysPatientVM> todaysConsultantedPatientList = new List<TodaysPatientVM>();
            var patientList = (from pm in _teleMedecineContext.PatientMasters where pm.CreatedOn.Value.Year == currentYear && pm.CreatedOn.Value.Month == currentMonth && pm.CreatedOn.Value.Day == currentDay
                               join phc in _teleMedecineContext.Phcmasters on pm.Phcid equals phc.Id
                               join pc in _teleMedecineContext.PatientCases on pm.Id equals pc.PatientId into patientcase
                               from pci in patientcase.DefaultIfEmpty()
                               join pcq in _teleMedecineContext.PatientQueues on pci.Id equals pcq.Id into pcqd
                               from pq in pcqd.DefaultIfEmpty()
                               join d in _teleMedecineContext.DoctorMasters on pq.AssignedDoctorId equals d.Id into dm
                               from doc in dm.DefaultIfEmpty()
                               join u in _teleMedecineContext.UserMasters on doc.UserId equals u.Id into um
                               from ud in um.DefaultIfEmpty()
                               where phc.Id == phcID
                               select new TodaysPatientVM
                               {
                                   //Age = GetAge(pm.Dob),
                                   Age = UtilityMaster.GetAgeOfPatient(pm.Dob),
                                   PatientName = pm.FirstName + " " + pm.LastName,
                                   ID = pm.Id,
                                   PhoneNumber = pm.PhoneNumber,
                                   PatientID = pm.PatientId,
                                   PHCUserID = pm.Phcid,
                                   PHCUserName = phc.Phcname,
                                   ReferredByPHCID = pm.Phcid,
                                   ReferredByPHCName = phc.Phcname,
                                   DocterID = pq.AssignedDoctorId > 0 ? pq.AssignedDoctorId : 0,
                                   DoctorName = ud.Name,
                                   Gender = (pm.GenderId == 1 ? "Male" : "Female")
                               }).ToListAsync();
            todaysPatientList = await patientList;
            foreach (var item in todaysPatientList)
            {
                if (item.DocterID > 0)
                {
                    todaysConsultantedPatientList.Add(item);
                }
            }

            return todaysConsultantedPatientList;
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
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;
            List<TodaysPatientVM> todaysPatientList = new List<TodaysPatientVM>();
            List<TodaysPatientVM> todaysNocConPatientList = new List<TodaysPatientVM>();
            var patientList = (from pm in _teleMedecineContext.PatientMasters
                               where pm.CreatedOn.Value.Year == currentYear && pm.CreatedOn.Value.Month == currentMonth && pm.CreatedOn.Value.Day == currentDay
                               join phc in _teleMedecineContext.Phcmasters on pm.Phcid equals phc.Id
                               join pc in _teleMedecineContext.PatientCases on pm.Id equals pc.PatientId into patientcase
                               from pci in patientcase.DefaultIfEmpty()
                               join pcq in _teleMedecineContext.PatientQueues on pci.Id equals pcq.Id into pcqd
                               from pq in pcqd.DefaultIfEmpty()
                               join d in _teleMedecineContext.DoctorMasters on pq.AssignedDoctorId equals d.Id into dm
                               from doc in dm.DefaultIfEmpty()
                               join u in _teleMedecineContext.UserMasters on doc.UserId equals u.Id into um
                               from ud in um.DefaultIfEmpty()
                               where phc.Id == phcID
                               select new TodaysPatientVM
                               {
                                   //Age = GetAge(pm.Dob),
                                   Age = UtilityMaster.GetAgeOfPatient(pm.Dob),
                                   PatientName = pm.FirstName + " " + pm.LastName,
                                   ID = pm.Id,
                                   PhoneNumber = pm.PhoneNumber,
                                   PatientID = pm.PatientId,
                                   PHCUserID = pm.Phcid,
                                   PHCUserName = phc.Phcname,
                                   ReferredByPHCID = pm.Phcid,
                                   ReferredByPHCName = phc.Phcname,
                                   DocterID = pq.AssignedDoctorId > 0 ? pq.AssignedDoctorId : 0,
                                   DoctorName = ud.Name,
                                   Gender = (pm.GenderId == 1 ? "Male" : "Female")
                               }).ToListAsync();
            todaysPatientList = await patientList;
            foreach (var item in todaysPatientList)
            {
                if (item.DocterID == 0)
                {
                    todaysNocConPatientList.Add(item);
                }
            }

            return todaysNocConPatientList;
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
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;
            List<TodaysPatientVM> todaysPatientList = new List<TodaysPatientVM>();
            List<TodaysPatientVM> todaysConsultantedPatientList = new List<TodaysPatientVM>();
            PHCPatientCount pHCPatientCount = new PHCPatientCount();    
            var patientList = (from pm in _teleMedecineContext.PatientMasters
                               where pm.CreatedOn.Value.Year == currentYear && pm.CreatedOn.Value.Month == currentMonth && pm.CreatedOn.Value.Day == currentDay
                               join phc in _teleMedecineContext.Phcmasters on pm.Phcid equals phc.Id
                               join pc in _teleMedecineContext.PatientCases on pm.Id equals pc.PatientId into patientcase
                               from pci in patientcase.DefaultIfEmpty()
                               join pcq in _teleMedecineContext.PatientQueues on pci.Id equals pcq.Id into pcqd
                               from pq in pcqd.DefaultIfEmpty()
                               join d in _teleMedecineContext.DoctorMasters on pq.AssignedDoctorId equals d.Id into dm
                               from doc in dm.DefaultIfEmpty()
                               join u in _teleMedecineContext.UserMasters on doc.UserId equals u.Id into um
                               from ud in um.DefaultIfEmpty()
                               where phc.Id == phcID
                               select new TodaysPatientVM
                               {
                                   //Age = GetAge(pm.Dob),
                                   Age = UtilityMaster.GetAgeOfPatient(pm.Dob),
                                   PatientName = pm.FirstName + " " + pm.LastName,
                                   ID = pm.Id,
                                   PhoneNumber = pm.PhoneNumber,
                                   PatientID = pm.PatientId,
                                   PHCUserID = pm.Phcid,
                                   PHCUserName = phc.Phcname,
                                   ReferredByPHCID = pm.Phcid,
                                   ReferredByPHCName = phc.Phcname,
                                   DocterID = pq.AssignedDoctorId > 0 ? pq.AssignedDoctorId : 0,
                                   DoctorName = ud.Name,
                                   Gender = (pm.GenderId == 1 ? "Male" : "Female")
                               }).ToListAsync();
            todaysPatientList = await patientList;

            if(todaysPatientList.Count > 0)
            {
                pHCPatientCount.PHCName = todaysPatientList.Select(s => s.ReferredByPHCName).FirstOrDefault();
                pHCPatientCount.ID = todaysPatientList.Select(s => s.ReferredByPHCID).FirstOrDefault();
                pHCPatientCount.TotalPatients = todaysPatientList.Count;
                pHCPatientCount.TotalConsulted = todaysPatientList.Where(a => a.DocterID > 0).Count();
                pHCPatientCount.TotalPending = todaysPatientList.Where(a => a.DocterID ==0).Count();
            } 
            else
            {
                pHCPatientCount.PHCName = "";
                pHCPatientCount.ID = 0;
                pHCPatientCount.TotalPatients = 0;
                pHCPatientCount.TotalConsulted = 0;
                pHCPatientCount.TotalPending = 0;
            }

           

            return pHCPatientCount;
        }

        public async Task<List<TodaysPatientVM>> GetSearchedTodaysPatientList(string patientName)
        {
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;
            List<TodaysPatientVM> todaysPatientList = new List<TodaysPatientVM>();           
            var patientList = (from pm in _teleMedecineContext.PatientMasters
                               where pm.CreatedOn.Value.Year == currentYear && pm.CreatedOn.Value.Month == currentMonth && pm.CreatedOn.Value.Day == currentDay && pm.FirstName.Contains(patientName) || pm.LastName.Contains(patientName)
                               join phc in _teleMedecineContext.Phcmasters on pm.Phcid equals phc.Id
                               join pc in _teleMedecineContext.PatientCases on pm.Id equals pc.PatientId into patientcase
                               from pci in patientcase.DefaultIfEmpty()
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
                                   PHCUserName = phc.Phcname,
                                   ReferredByPHCID = pm.Phcid,
                                   ReferredByPHCName = phc.Phcname,
                                   DocterID = pq.AssignedDoctorId > 0 ? pq.AssignedDoctorId : 0,
                                   DoctorName = ud.Name,
                                   Gender = (pm.GenderId == 1 ? "Male" : "Female")
                               }).ToListAsync();
            todaysPatientList = await patientList;         

            return todaysPatientList;
        }

        public int GetAge(DateTime dateofbirth)
        {
            DateTime dtToday = DateTime.Now.Date;
            DateTime dtOfBirth = dateofbirth.Date;
            TimeSpan diffResult = dtToday - dtToday;
            double totalDays = diffResult.TotalDays;
            if (diffResult != TimeSpan.Zero)
            {
                if (totalDays > 365)
                {
                    int year = (int)(totalDays / 365);
                    return year;
                }
                else if (totalDays < 365)
                {
                    int month = (int)(totalDays / 12);
                    return 365;
                }
                else
                {
                    return 0;
                }

            }
            else
                return 0;
            
        }
    }
}
