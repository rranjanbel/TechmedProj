using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IPatientRepository: IRepository<PatientMaster>
    {
        Task<List<PatientMaster>> GetAllPatient();
        Task<PatientMaster> AddPatient(PatientMaster patientMaster);
        Task<PatientMaster> GetPatientByID(int Id);
        Task<PatientMaster> UpdatePatient(int Id);
        Task<bool> DeletePatient(int Id);
        bool IsPatientExist(PatientMaster patientMaster);
        Task<List<TodaysPatientVM>> GetTodaysPatientList(int phcID);
        Task<List<TodaysPatientVM>> GetCheckedPatientList(int phcID);
        Task<List<PatientMaster>> GetUnCheckedPatientList(int Id);
        Task<List<PatientMaster>> GetPendingPatientList(int Id);
        Task<PHCPatientCount> GetPatientCount(int phcID);
        Task<List<TodaysPatientVM>> GetSearchedTodaysPatientList(string patientName);
        Task<List<PatientViewModel>> GetYesterdaysPatientList(int phcID);
        List<PatientSearchResultVM> GetAdvanceSearchPatient(AdvanceSearchPatientVM searchParameter);
        Task<List<SpecializationDTO>> GetSuggestedSpcialiazationByPatientCaseID(int Id);
        long GetPatientId();
        public int GetAge(DateTime dateofbirth);
        string SaveImage(string ImgBase64Str, string rootPath);
    }
}
