using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IPatientCaseRepository : IRepository<PatientCase>
    {
        Task<PatientCase> CreateAsync(PatientCase patientCase);
        Task<PatientCase> GetByID(int id);
        Task<PatientCase> GetByPHCUserID(int userId);
        bool IsPatientCaseExist(PatientCaseCreateVM patientCase);
        long GetCaseFileNumber();
    }
}
