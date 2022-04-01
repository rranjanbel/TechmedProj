using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.Adapters;
using TechMed.BL.DTOMaster;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.BL.ModelMaster
{
    public class DoctorBusinessMaster : BaseAdapter
    {
        public DoctorBusinessMaster(TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext, mapper)
        {

        }
        public void GetDoctorDetails()
        {

        }
        public void UpdateDoctorDetails()
        {

        }
        public void GetListOfPHCHospital()
        {
        }
        public List<NotificationDTO> GetListOfNotification(GetListOfNotificationVM getListOfNotificationVM)
        {
            List<Notification> notifications = teleMedecineContext.Notifications.Where(o => o.ToUserNavigation.Email.ToLower() == getListOfNotificationVM.UserEmail.ToLower()).ToList();
            var DTOList = new List<NotificationDTO>();

            foreach (var Notification in notifications)
            {
                NotificationDTO mapdata = mapper.Map<NotificationDTO>(Notification);
                DTOList.Add(mapdata);
            }
            return DTOList;

        }
        public CdssguidelineMasterDTO GetCDSSGuideLines()
        {
            CdssguidelineMaster cdssguidelineMaster = teleMedecineContext.CdssguidelineMasters.FirstOrDefault();
            var DTOList = new List<NotificationDTO>();
            CdssguidelineMasterDTO mapdata = mapper.Map<CdssguidelineMasterDTO>(cdssguidelineMaster);
            return mapdata;
        }

        public void GetYesterdayPatientsHistory()
        {

        }
        public void GetAfterYesterdayPatientsHistory()
        {

        }
        public void GetTodayesPatients()
        {

        }
        public void GetCompletedConsultationPatientsHistory()
        {

        }
        public void GetListOfVital()
        {

        }
        public void GetListOfMedicine()
        {

        }

        public void PostTreatmentPlan()
        {

        }
        public void PatientAbsent()
        {

        }
        public void ReferHigherFacilityAbsent()
        {

        }
        public void CallPHC()
        {

        }

        public void GetPatientCaseDetails()
        {

        }
        public void GetPatientCaseFiles()
        {

        }

    }
}
