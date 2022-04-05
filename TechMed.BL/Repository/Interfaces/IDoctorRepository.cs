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
    public interface IDoctorRepository : IRepository<UserMaster>
    {
        void AddDoctorDetails();
        void GetDoctorDetails(GetDoctorDetailVM getDoctorDetailVM);
        void UpdateDoctorDetails();
        void GetListOfPHCHospital();
        public Task<List<NotificationDTO>> GetListOfNotification(GetListOfNotificationVM getListOfNotificationVM);
        public Task<CdssguidelineMasterDTO> GetCDSSGuideLines();
        void GetYesterdayPatientsHistory();
        void GetAfterYesterdayPatientsHistory();
        public void GetTodayesPatients();
        void GetCompletedConsultationPatientsHistory();
        void GetListOfVital();
        void GetListOfMedicine();
        void PostTreatmentPlan();
        void PatientAbsent();
        void ReferHigherFacilityAbsent();
        void CallPHC();
        void GetPatientCaseDetails();
        void GetPatientCaseFiles();

    }
}
