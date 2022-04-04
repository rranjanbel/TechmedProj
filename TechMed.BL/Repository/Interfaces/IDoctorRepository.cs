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
    internal interface IDoctorRepository : IRepository<UserMaster>
    {
        void AddDoctorDetails();
        void GetDoctorDetails();
        void UpdateDoctorDetails();
        void GetListOfPHCHospital();
        List<NotificationDTO> GetListOfNotification(GetListOfNotificationVM getListOfNotificationVM);
        CdssguidelineMasterDTO GetCDSSGuideLines();
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
