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
                    //if (patientMaster.CreatedBy == 0)
                    //    patientMaster.CreatedBy = 2;
                    //if (patientMaster.UpdatedBy == 0)
                    //    patientMaster.UpdatedBy = 2;
                    patientMaster.CreatedOn = DateTime.Now;
                    patientMaster.UpdatedOn = DateTime.Now;
                    // patientMaster.PatientId = UtilityMaster.GetPatientNumber();
                    //patientMaster.PatientId = GetPatientId();

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
            catch (Exception )
            {
                //_logger.LogInformation($"Add Patient : get exception " +ex.Message);
                //return updatedPatientMaster;
                throw;
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
            //List<SPResultGetPatientDetails> sPResultGetPatientDetails = GetSPResult(phcID);
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
            bool result = _teleMedecineContext.PatientMasters.Any(a => a.FirstName == patientMaster.FirstName && a.LastName == patientMaster.LastName || a.MobileNo == patientMaster.MobileNo);
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
            TimeSpan diffResult = dtToday - dtOfBirth;
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

        public long GetPatientId()
        {
            Setting setting = new Setting();
            Int64 currentNo = 0;
            Int64 patientSerNo = 0;
            patientSerNo = _teleMedecineContext.Settings.Select(a => a.PatientNumber).FirstOrDefault();
            if (patientSerNo > 0)
            {
                currentNo = patientSerNo;
                setting = _teleMedecineContext.Settings.FirstOrDefault();
                if (setting != null)
                {
                    setting.PatientNumber = currentNo + 1;
                }
                try
                {
                    _teleMedecineContext.Entry(setting).State = EntityState.Modified;
                    var result = _teleMedecineContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
                return currentNo + 1;
            }
            return 0;
        }      

        public List<SPResultGetPatientDetails> GetSPResult(int patientId)
        {
            int PatientID = patientId;
            List<SPResultGetPatientDetails> sPResultGetPatientDetails = new List<SPResultGetPatientDetails>();
            SPResultGetPatientDetails sPResultGetPatientDetail ;
            var Results = _teleMedecineContext.SPResultGetPatientDetails.FromSqlInterpolated($"EXEC [dbo].[GetPatientDetails] @PatientID ={PatientID}");
            foreach (var item in Results)
            {
                sPResultGetPatientDetail = new SPResultGetPatientDetails();
                sPResultGetPatientDetail.PhoneNo = item.PhoneNo;
                sPResultGetPatientDetail.PatientCreatedBy = item.PatientCreatedBy;
                sPResultGetPatientDetail.PatientName = item.PatientName;
                sPResultGetPatientDetail.MOName = item.MOName;
                sPResultGetPatientDetail.MailID = item.MailID;
                sPResultGetPatientDetail.Zone = item.Zone;
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
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;
            List<PatientViewModel> patientList = new List<PatientViewModel>();        
            var patientResult = (from pm in _teleMedecineContext.PatientMasters
                               where pm.CreatedOn.Value.Year == currentYear && pm.CreatedOn.Value.Month == currentMonth && pm.CreatedOn.Value.Day == currentDay-1
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
                               select new PatientViewModel
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
            patientList = await patientResult;
           

            return patientList;
        }

        public string SaveImage(string ImgBase64Str, string rootPath)
        {
            //string strm = "R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7";
            //ImgBase64Str = strm;
            //string webRootPath = _webHostEnvironment.WebRootPath;
         
            string contentRootPath = rootPath;
            string path = @"\\MyStaticFiles\\Images\\Patients\\";
            //path = Path.Combine(webRootPath, "CSS");
            //path = Path.Combine(contentRootPath, path);
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
            string? PatientName = string.Empty;
            long ? PatientId = 0;
            string? contractNo = string.Empty;
            int? genderId = 0;
            if (searchParameter != null)
            {
                if (searchParameter.PHCID > 0)
                    PHCID = searchParameter.PHCID;
                else
                    PHCID = null;
                if (searchParameter.PatientName == "")
                    PatientName = null;
                else
                    PatientName = searchParameter.PatientName;
                if(searchParameter.PatientName.ToLower().Trim() == "string")
                    PatientName = null;
                else
                    PatientName = searchParameter.PatientName;
                if (searchParameter.PatientUID > 0)
                    PatientId = searchParameter.PatientUID;
                else
                    PatientId = null;
                if (searchParameter.ContactNo == "")
                    contractNo = null; 
                else
                    contractNo = searchParameter.ContactNo;
                if (searchParameter.ContactNo.ToLower().Trim() == "string")
                    contractNo = null;
                else
                    contractNo = searchParameter.ContactNo;
                if (searchParameter.GenderId > 0)
                    genderId = searchParameter.GenderId;
                else
                    genderId = null;
            }
           
            var Results = _teleMedecineContext.PatientSearchResults
                .FromSqlInterpolated($"EXEC [dbo].[AdvanceSearchOfPatients] @PHCID ={PHCID},@PatientName={PatientName},@PatientUID={PatientId},@ContactNo={contractNo},@GenderId={genderId}");
            foreach (var item in Results)
            {
                searchResult = new PatientSearchResultVM();              
                searchResult.PatientName = item.PatientName;
                searchResult.PatientID = item.PatientID;
                searchResult.Gender = item.Gender;
                searchResult.Age = item.Age;
                searchResult.PhoneNumber = item.PhoneNumber;
                patientSearchResults.Add(searchResult);
            };
            return patientSearchResults;
        }
    }
}
