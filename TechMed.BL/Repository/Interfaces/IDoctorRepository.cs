using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IDoctorRepository : IRepository<DoctorMaster>
    {
        void AddDoctorDetails();
        public Task<DoctorDTO> GetDoctorDetails(GetDoctorDetailVM getDoctorDetailVM);
        public Task<bool> UpdateDoctorDetails(DoctorDTO doctorDTO);
        public Task<List<PHCHospitalDTO>> GetListOfPHCHospital();
        public Task<List<NotificationDTO>> GetListOfNotification(GetListOfNotificationVM getListOfNotificationVM);
        public Task<CdssguidelineMasterDTO> GetCDSSGuideLines();
        public Task<List<GetTodayesPatientsDTO>> GetYesterdayPatientsHistory(DoctorVM doctorVM);
        public Task<List<GetTodayesPatientsDTO>> GetPastPatientsHistory(DoctorVM doctorVM);
        public Task<List<GetTodayesPatientsDTO>> GetTodayesPatients(DoctorVM doctorVM);
        public Task<List<GetTodayesPatientsDTO>> GetCompletedConsultationPatientsHistory(DoctorVM doctorVM);
        public Task<List<VitalMasterDTO>> GetListOfVital();
        public Task<List<MedicineMasterDTO>> GetListOfMedicine();
        public Task<List<SpecializationDTO>> GetListOfSpecializationMaster();
        public Task<List<SubSpecializationDTO>> GetListOfSubSpecializationMaster(int SpecializationID);
        public Task<bool> DeleteNotification(long NotificationID);
        public Task<bool> PostTreatmentPlan(TreatmentVM treatmentVM);
        public Task<GetEHRDTO> GetEHR(GetEHRVM getEHRVM);
        void PatientAbsent();
        void ReferHigherFacilityAbsent();
        void CallPHC();
        public Task<GetPatientCaseDetailsDTO> GetPatientCaseDetailsAsync(GetPatientCaseDetailsVM vm);

    }
}
