using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
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
    public class PHCRepository : Repository<Phcmaster>, IPHCRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        //private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PHCRepository> _logger;
        public PHCRepository(ILogger<PHCRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;


        }

        public async Task<Phcmaster> AddPHCUser(Phcmaster phcmaster, UserMaster userMaster)
        {
            int i = 0;
            int j = 0;
            Phcmaster phcmasternew = new Phcmaster();            

            using (TeleMedecineContext context = new TeleMedecineContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.UserMasters.AddAsync(userMaster);
                        i = await context.SaveChangesAsync();
                        if (i > 0 && userMaster.Id > 0)
                        {
                            phcmaster.UserId = userMaster.Id;
                            context.Phcmasters.AddAsync(phcmaster);
                            j = await context.SaveChangesAsync(); ;
                        }
                        if (i > 0 && j > 0)
                        {
                            transaction.Commit();

                            phcmasternew = context.Phcmasters.FirstOrDefault(a => a.Id == phcmaster.Id);
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
                    }
                }
            }        
           
            return phcmasternew;
        }

        public async Task<Phcmaster> GetByID(int id)
        {
            Phcmaster phcmaster = new Phcmaster();
            phcmaster = await _teleMedecineContext.Phcmasters.Where(a => a.Id == id).FirstOrDefaultAsync(); 
            return phcmaster;
        }

        public async Task<Phcmaster> GetByPHCUserID(int userId)
        {
            Phcmaster phcmaster = new Phcmaster();
            var phc = await _teleMedecineContext.Phcmasters.Where(a => a.UserId == userId).FirstOrDefaultAsync();
            phcmaster = (Phcmaster)phc;
            return phcmaster;
        }

        public async Task<PHCDetailsIdsVM> GetPHCDetailByEmailID(string email)
        {
            PHCDetailsIdsVM pHCDetails = new PHCDetailsIdsVM();
            var phcresult = await(from pm in _teleMedecineContext.Phcmasters
                                  join cm in _teleMedecineContext.ClusterMasters on pm.ClusterId equals cm.Id
                                  join zo in _teleMedecineContext.BlockMasters on pm.BlockId equals zo.Id
                                  join ur in _teleMedecineContext.UserMasters on pm.UserId equals ur.Id
                                  join ud in _teleMedecineContext.UserDetails on ur.Id equals ud.UserId into usr
                                  from usrdet in usr.DefaultIfEmpty()
                                  join st in _teleMedecineContext.StateMasters on usrdet.StateId equals st.Id into smast
                                  from satmas in smast.DefaultIfEmpty()
                                  join gn in _teleMedecineContext.GenderMasters on usrdet.GenderId equals gn.Id into gmast
                                  from genmas in gmast.DefaultIfEmpty()
                                  where ur.Email.Contains(email)  
                                  select new PHCDetailsIdsVM
                                  {
                                      Phcname = pm.Phcname,
                                      ClusterName = pm.Cluster.Cluster,
                                      BLockName = pm.Block.BlockName,
                                      Moname = pm.Moname,
                                      Address = pm.Address,
                                      PhoneNo = pm.PhoneNo,
                                      MailId = pm.MailId,
                                      FirstName = usrdet.FirstName,
                                      MiddleName = usrdet.MiddleName,
                                      LastName = usrdet.LastName,
                                      State = satmas.StateName,
                                      City = usrdet.City,
                                      PinCode = usrdet.PinCode,
                                      Gender = genmas.Gender,
                                      PHCId = pm.Id,
                                      BLockID = pm.BlockId,
                                      ClusterId = pm.ClusterId
                                  }).FirstOrDefaultAsync();

            pHCDetails = (PHCDetailsIdsVM)phcresult;
            return pHCDetails;
        }

        public async Task<PHCDetailsVM> GetPHCDetailByUserID(int userId)
        {
            PHCDetailsVM pHCDetails = new PHCDetailsVM();           
            var phcresult = await (from pm in _teleMedecineContext.Phcmasters
                          join cm in _teleMedecineContext.ClusterMasters on pm.ClusterId equals cm.Id
                          join zo in _teleMedecineContext.BlockMasters on pm.BlockId equals zo.Id
                          join ur in _teleMedecineContext.UserMasters on pm.UserId equals ur.Id
                          join ud in _teleMedecineContext.UserDetails on ur.Id equals ud.UserId into usr
                          from usrdet in usr.DefaultIfEmpty()
                          join st in _teleMedecineContext.StateMasters on usrdet.StateId equals st.Id into smast
                          from satmas in smast.DefaultIfEmpty()
                          join gn in _teleMedecineContext.GenderMasters on usrdet.GenderId equals gn.Id into gmast
                          from genmas in gmast.DefaultIfEmpty()
                          where pm.UserId == userId
                          select new PHCDetailsVM
                          {
                              Phcname = pm.Phcname,
                              ClusterName = pm.Cluster.Cluster,
                              BlockName = pm.Block.BlockName,
                               Moname = pm.Moname,
                               Address = pm.Address,
                               PhoneNo = pm.PhoneNo,
                               MailId = pm.MailId,
                               FirstName = usrdet.FirstName,
                               MiddleName = usrdet.MiddleName,
                               LastName = usrdet.LastName,
                               State = satmas.StateName,
                               City = usrdet.City,
                               PinCode = usrdet.PinCode,
                               Gender = genmas.Gender,
                               Id = pm.Id
                          }).FirstOrDefaultAsync();

            pHCDetails = (PHCDetailsVM)phcresult;      
            return pHCDetails;
        }

        public bool IsPHCExit(string name)
        {
           bool isExist = _teleMedecineContext.Phcmasters.Any(a => a.Phcname == name);
            return isExist;
        }

        public async Task<List<PHCMasterDTO>> GetAllPHC(int districtId)
        {
            List<PHCMasterDTO> phcList = new List<PHCMasterDTO>();
            PHCMasterDTO phcDTO;
           var phcs = await _teleMedecineContext.Phcmasters.Where(a => a.DistrictId == districtId).ToListAsync();
            foreach (var phc in phcs)
            {
                phcDTO = new PHCMasterDTO();
                phcDTO.ID = phc.Id;
                phcDTO.PHCName = phc.Phcname;
                phcList.Add(phcDTO);
            }
            return phcList;
        }

        public async Task<EmployeeTrainingDTO> AddEmployeeTraining(EmployeeTrainingDTO employeeTraining)
        {
            int i = 0;            
            EmployeeTraining employee = new EmployeeTraining();         

            EmployeeTrainingDTO insertedEmployeeTraining = new EmployeeTrainingDTO();
            try
            {
                if (employeeTraining != null)
                {
                    employee = _mapper.Map<EmployeeTraining>(employeeTraining);
                    employee.CreatedOn = UtilityMaster.GetLocalDateTime();
                    employee.UpdatedOn = UtilityMaster.GetLocalDateTime();
                    var ressult = _teleMedecineContext.EmployeeTrainings.AddAsync(employee);
                    i = await _teleMedecineContext.SaveChangesAsync();
                    if (i > 0)
                    {
                        insertedEmployeeTraining = _mapper.Map<EmployeeTrainingDTO>(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                string excpMessage = ex.Message;
            }
          
           
           

            return insertedEmployeeTraining;
        }

        public bool PostSpokeMaintenance(SpokeMaintenanceDTO spokeDTO, string contentRootPath)
        {
            SpokeMaintenance spokeMaintenance;
            //List<PatientCaseDocDTO> patientCaseDocuments = new List<PatientCaseDocDTO>();
            int l = 0;
            

            if (spokeDTO != null)
            {                
                string relativePath = @"/MyFiles/PHCDocuments/";
                var myfilename = string.Format(@"{0}", Guid.NewGuid());

                if (spokeDTO.file.Length > 0)
                {
                    l = 0;
                    myfilename = myfilename+"."+Path.GetExtension( spokeDTO.file.FileName);
                    string fileName = SaveDocument(spokeDTO.file, contentRootPath, myfilename);
                    if(fileName != null)
                    {
                        //string fullPath = Path.Combine(path, fileName);
                        string fullRelativePath = Path.Combine(relativePath, fileName);
                        spokeMaintenance = new SpokeMaintenance();
                        spokeMaintenance.Phcid = spokeDTO.Phcid;
                        spokeMaintenance.FilePath = fullRelativePath;
                        spokeMaintenance.Date = spokeDTO.dateTime ;

                        this._teleMedecineContext.Entry(spokeMaintenance).State = EntityState.Added;
                        l = this.Context.SaveChanges();
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

        public string SaveDocument(IFormFile file, string rootPath, string updatedFileName)
        {
            try
            {
             
                string saveFilename = "";

               string relativePath = @"\\MyStaticFiles\\PHCDocuments\\";

                //Create   
               // var filePath = Path.Combine(relativePath, updatedFileName);
                var fileType = Path.GetExtension(file.FileName);
                //Convert to base64
                string base64Value = UtilityMaster.ConvertToBase64(file);
                //Save File in disk
                saveFilename = UtilityMaster.SaveFileFromBase64(base64Value, rootPath, relativePath, fileType.ToLower());

                if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
                {
                    var filePath = Path.Combine(rootPath, updatedFileName);

                    using (Stream stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    }
                }
                return saveFilename;
            }
            catch
            {
                return "";
            }
           
        }
    }
}
