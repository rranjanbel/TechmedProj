using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.BaseClasses
{
    public class DoctorRepository :Repository<DoctorMaster>, IDoctorRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        public DoctorRepository(TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
        }

        public void AddDoctorDetails()
        {
            throw new NotImplementedException();
        }

        public void CallPHC()
        {
            throw new NotImplementedException();
        }

        public Task<UserMaster> Create(UserMaster model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserMaster>> Get(Func<UserMaster, bool> where)
        {
            throw new NotImplementedException();
        }

        public void GetAfterYesterdayPatientsHistory()
        {
            throw new NotImplementedException();
        }

        public CdssguidelineMasterDTO GetCDSSGuideLines()
        {
            throw new NotImplementedException();
        }

        public void GetCompletedConsultationPatientsHistory()
        {
            throw new NotImplementedException();
        }

        public void GetDoctorDetails()
        {
            throw new NotImplementedException();
        }

        public void GetListOfMedicine()
        {
            throw new NotImplementedException();
        }

        public List<NotificationDTO> GetListOfNotification(GetListOfNotificationVM getListOfNotificationVM)
        {
            throw new NotImplementedException();
        }

        public void GetListOfPHCHospital()
        {
            throw new NotImplementedException();
        }

        public void GetListOfVital()
        {
            throw new NotImplementedException();
        }

        public void GetPatientCaseDetails()
        {
            throw new NotImplementedException();
        }

        public void GetPatientCaseFiles()
        {
            throw new NotImplementedException();
        }

        public void GetTodayesPatients()
        {
            throw new NotImplementedException();
        }

        public void GetYesterdayPatientsHistory()
        {
            throw new NotImplementedException();
        }

        public void PatientAbsent()
        {
            throw new NotImplementedException();
        }

        public void PostTreatmentPlan()
        {
            throw new NotImplementedException();
        }

        public void ReferHigherFacilityAbsent()
        {
            throw new NotImplementedException();
        }

        public Task<UserMaster> Update(UserMaster model)
        {
            throw new NotImplementedException();
        }

        public void UpdateDoctorDetails()
        {
            throw new NotImplementedException();
        }

        public Task<UserMaster> UpdateOnly(UserMaster model)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<UserMaster>> IRepository<UserMaster>.Get()
        {
            throw new NotImplementedException();
        }

        //Task<UserMaster> IRepository<UserMaster>.Get(long id)
        //{
        //    throw new NotImplementedException();
        //}

        Task<UserMaster> IRepository<UserMaster>.Get(int id)
        {
            throw new NotImplementedException();
        }
        //public void AddDoctorDetails()
        //{

        //}
        //public void GetDoctorDetails()
        //{

        //}
        //public void UpdateDoctorDetails()
        //{

        //}
        //public void GetListOfPHCHospital()
        //{
        //}
        //public List<NotificationDTO> GetListOfNotification(GetListOfNotificationVM getListOfNotificationVM)
        //{
        //    List<Notification> notifications = _teleMedecineContext.Notifications.Where(o => o.ToUserNavigation.Email.ToLower() == getListOfNotificationVM.UserEmail.ToLower()).ToList();
        //    var DTOList = new List<NotificationDTO>();

        //    foreach (var Notification in notifications)
        //    {
        //        NotificationDTO mapdata = _mapper.Map<NotificationDTO>(Notification);
        //        DTOList.Add(mapdata);
        //    }
        //    return DTOList;

        //}
        //public CdssguidelineMasterDTO GetCDSSGuideLines()
        //{
        //    CdssguidelineMaster cdssguidelineMaster = _teleMedecineContext.CdssguidelineMasters.FirstOrDefault();
        //    var DTOList = new List<NotificationDTO>();
        //    CdssguidelineMasterDTO mapdata = _mapper.Map<CdssguidelineMasterDTO>(cdssguidelineMaster);
        //    return mapdata;
        //}

        //public void GetYesterdayPatientsHistory()
        //{

        //}
        //public void GetAfterYesterdayPatientsHistory()
        //{

        //}
        //public void GetTodayesPatients()
        //{
        //    //List<Notification> notifications = teleMedecineContext.PatientCases.Where(o => o..Email.ToLower() == getListOfNotificationVM.UserEmail.ToLower()).ToList();
        //    //var DTOList = new List<NotificationDTO>();

        //    //foreach (var Notification in notifications)
        //    //{
        //    //    NotificationDTO mapdata = mapper.Map<NotificationDTO>(Notification);
        //    //    DTOList.Add(mapdata);
        //    //}
        //    //return DTOList;
        //}
        //public void GetCompletedConsultationPatientsHistory()
        //{

        //}
        //public void GetListOfVital()
        //{

        //}
        //public void GetListOfMedicine()
        //{

        //}

        //public void PostTreatmentPlan()
        //{

        //}
        //public void PatientAbsent()
        //{

        //}
        //public void ReferHigherFacilityAbsent()
        //{

        //}
        //public void CallPHC()
        //{

        //}

        //public void GetPatientCaseDetails()
        //{

        //}
        //public void GetPatientCaseFiles()
        //{

        //}

        //IQueryable<UserMaster> IRepository<UserMaster>.Get()
        //{
        //    throw new NotImplementedException();
        //}

        //UserMaster IRepository<UserMaster>.Get(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<UserMaster> Add(UserMaster entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<UserMaster> Update(UserMaster entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
