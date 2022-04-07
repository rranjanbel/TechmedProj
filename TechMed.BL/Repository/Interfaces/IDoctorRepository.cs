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
        void GetYesterdayPatientsHistory();
        void GetPastPatientsHistory();
        public Task<List<GetTodayesPatientsDTO>> GetTodayesPatients(long DoctorID);
        public Task<List<GetTodayesPatientsDTO>> GetCompletedConsultationPatientsHistory(long DoctorID);
        public Task<List<VitalMasterDTO>> GetListOfVital();
        public Task<List<MedicineMasterDTO>> GetListOfMedicine();
        public Task<List<SpecializationDTO>> GetListOfSpecializationMaster();
        public Task<List<SubSpecializationDTO>> GetListOfSubSpecializationMaster(int SpecializationID);
        void PostTreatmentPlan();
        void PatientAbsent();
        void ReferHigherFacilityAbsent();
        void CallPHC();
        void GetPatientCaseDetails();
        void GetPatientCaseFiles();

    }
}
