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
    public interface IPatientCaseRepository : IRepository<PatientCase>
    {
        Task<PatientCase> CreateAsync(PatientCase patientCase);
        Task<PatientCase> GetByID(int id);
        Task<PatientCase> GetByPHCUserID(int userId);
        Task<PatientCaseVM> GetPatientCaseDetails(int PHCID, int PatientID);
        Task<PatientCaseDetailsVM> PostPatientCaseDetails(PatientCaseDetailsVM patientCaseVM);
        Task<PatientCaseWithDoctorVM> GetPatientQueueDetails(int PHCID, int PatientID);
        Task<PatientReferToDoctorVM> PostPatientReferToDoctor(PatientReferToDoctorVM patientReferToDoctorVM);
        Task<PatientFeedbackDTO> PostPatientFeedBack(PatientFeedbackDTO patientFeedback);
        Task<PatientCaseVM> GetPatientCaseDetailsByCaseID(Int64 PatientCaseID, string contentRootPath);
        Task<PatientCaseLevelDTO> GetPatientCaseLevels(int patientID);
        Task<List<PatientCaseDocDTO>> GetPatientCaseDocList(int PatientCaseID, string rootUrl);
        bool IsPatientCaseExist(PatientCaseCreateVM patientCase);
        //bool SaveCaseDocument(List<CaseDocumentVM> caseDocuments, string contentRootPath);
        bool UploadCaseDoc(List<CaseDocumentVM> caseDocuments, string contentRootPath);
        long GetCaseFileNumber();
        int GetLoggedPHCID(string userId);
        Task<OnlineDoctorListVM> GetSelectedOnlineDoctors(long patientCaseID);
        Task<List<PatientQueueByDoctor>> GetPatientQueueByDoctor(int specializationID);
        Task<List<PatientQueueVM>> GetPatientQueue(int PHCID);

        bool UploadCaseDocFromByte(List<CaseDocumentBase64VM> caseDocuments, string contentRootPath);
        Task<PatientReferToDoctorVM> AddPatientInDoctorsQueue(PatientReferToDoctorVM patientReferToDoctorVM);


    }
}
