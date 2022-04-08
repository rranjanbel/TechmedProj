using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.Repository.Interfaces;
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
            if(patientMaster != null)
            {
                if (patientMaster.CreatedBy == 0)
                    patientMaster.CreatedBy = 2;
                if (patientMaster.UpdatedBy == 0)
                    patientMaster.UpdatedBy = 2;
                patientMaster.CreatedOn = DateTime.Now;
                patientMaster.UpdatedOn = DateTime.Now;
                //patientMaster.PatientId = setting.PatientNumber();

                if (patientMaster.Id ==0)
                {
                    _teleMedecineContext.Add(patientMaster);
                    int i = await _teleMedecineContext.SaveChangesAsync();
                    if(i > 0)
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

        public Task<List<PatientMaster>> GetCheckedPatientList(int Id)
        {
            throw new NotImplementedException();
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

        public Task<List<PatientMaster>> GetTodaysPatientList(int Id)
        {
            throw new NotImplementedException();
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
    }
}
