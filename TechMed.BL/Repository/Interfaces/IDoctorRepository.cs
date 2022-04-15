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
        public Task<List<PHCHospitalDTO>> GetListOfPHCHospitalZoneWise(GetListOfPHCHospitalVM getListOfPHCHospitalVM);
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
        public Task<bool> PatientAbsent(PatientAbsentVM patientAbsentVM);
        public Task<bool> ReferHigherFacility(PatientAbsentVM patientAbsentVM);
        public Task<List<GetCaseLabelDTO>> GetCaseLabel(GetCaseLabelVM getCaseLabelVM);
        void CallPHC();
        public Task<GetPatientCaseDetailsDTO> GetPatientCaseDetailsAsync(GetPatientCaseDetailsVM vm);
        public Task<List<SearchPatientsDTO>> SearchPatientDrDashBoard(SearchPatientVM searchPatientVM);
        public Task<List<SearchPatientsDTO>> SearchPatientDrHistory(SearchPatientVM searchPatientVM);
        public Task<List<GetTodayesPatientsDTO>> GetLatestReferred(DoctorVM doctorVM);
        public Task<bool> UpdateIsDrOnline(UpdateIsDrOnlineVM updateIsOnlineDrVM);
        public Task<bool> UpdateIsDrOnlineByUserLoginName(UpdateIsDrOnlineByUserLoginNameVM updateIsOnlineDrVM);
        public Task<bool> IsDrOnline(DoctorVM doctorVM);
        public Task<List<OnlineDrListDTO>> OnlineDrList(OnlineDrListVM doctorVM);
        public Task<DoctorMaster> AddDoctor(DoctorMaster doctorMaster, UserMaster userMaster, UserDetail userDetail);

        public Task<string> CheckEmail(string Email);
        public Task<string> CheckMobile(string Mobile);

    }
}
