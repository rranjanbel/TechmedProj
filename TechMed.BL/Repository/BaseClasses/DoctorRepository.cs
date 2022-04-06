using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class DoctorRepository : Repository<DoctorMaster>, IDoctorRepository
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

        public void GetCompletedConsultationPatientsHistory()
        {
            throw new NotImplementedException();
        }

        public async Task<DoctorDTO> GetDoctorDetails(GetDoctorDetailVM getDoctorDetailVM)
        {
            //
            DoctorMaster doctorMaster = await _teleMedecineContext.DoctorMasters.Where(o => o.User.Email.ToLower() == getDoctorDetailVM.UserEmailID.ToLower()).FirstOrDefaultAsync();
            UserDetail userDetail = await _teleMedecineContext.UserDetails.Where(o => o.UserId == doctorMaster.UserId).FirstOrDefaultAsync();
            var DTO = new DoctorDTO();
            DTO= _mapper.Map<DoctorDTO>(doctorMaster);
            DTO.detailsDTO = _mapper.Map<DetailsDTO>(userDetail);
            return DTO;
        }
        public async Task<List<MedicineMasterDTO>> GetListOfMedicine()
        {
            List<MedicineMaster> masters = await _teleMedecineContext.MedicineMasters.ToListAsync();
            var DTOList = new List<MedicineMasterDTO>();
            foreach (var item in masters)
            {
                MedicineMasterDTO mapdata = _mapper.Map<MedicineMasterDTO>(item);
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<NotificationDTO>> GetListOfNotification(GetListOfNotificationVM getListOfNotificationVM)
        {
            List<Notification> notifications = await _teleMedecineContext.Notifications.Where(o => o.ToUserNavigation.Email.ToLower() == getListOfNotificationVM.UserEmail.ToLower()).ToListAsync();
            var DTOList = new List<NotificationDTO>();

            foreach (var Notification in notifications)
            {
                NotificationDTO mapdata = _mapper.Map<NotificationDTO>(Notification);
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<VitalMasterDTO>> GetListOfVital()
        {
            List<VitalMaster> masters = await _teleMedecineContext.VitalMasters.ToListAsync();
            var DTOList = new List<VitalMasterDTO>();
            foreach (var item in masters)
            {
                VitalMasterDTO mapdata = _mapper.Map<VitalMasterDTO>(item);
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<CdssguidelineMasterDTO> GetCDSSGuideLines()
        {
            CdssguidelineMaster cdssguidelineMaster = await _teleMedecineContext.CdssguidelineMasters.FirstOrDefaultAsync();
            var DTOList = new List<NotificationDTO>();
            CdssguidelineMasterDTO mapdata = _mapper.Map<CdssguidelineMasterDTO>(cdssguidelineMaster);
            return mapdata;
        }
        public async Task<List<PHCHospitalDTO>> GetListOfPHCHospital()
        {
            List<Phcmaster> masters = await _teleMedecineContext.Phcmasters.ToListAsync();
            var DTOList = new List<PHCHospitalDTO>();
            foreach (var item in masters)
            {
                PHCHospitalDTO mapdata = _mapper.Map<PHCHospitalDTO>(item);
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<SpecializationDTO>> GetListOfSpecializationMaster()
        {
            List<SpecializationMaster> masters = await _teleMedecineContext.SpecializationMasters.ToListAsync();
            var DTOList = new List<SpecializationDTO>();
            foreach (var item in masters)
            {
                SpecializationDTO mapdata = _mapper.Map<SpecializationDTO>(item);
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public async Task<List<SubSpecializationDTO>> GetListOfSubSpecializationMaster(int SpecializationId)
        {
            List<SubSpecializationMaster> masters = await _teleMedecineContext.SubSpecializationMasters.Where(a=>a.SpecializationId== SpecializationId).ToListAsync();
            var DTOList = new List<SubSpecializationDTO>();
            foreach (var item in masters)
            {
                SubSpecializationDTO mapdata = _mapper.Map<SubSpecializationDTO>(item);
                DTOList.Add(mapdata);
            }
            return DTOList;
        }
        public void UpdateDoctorDetails()
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

        public Task<UserMaster> UpdateOnly(UserMaster model)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<UserMaster>> IRepository<UserMaster>.Get()
        {
            throw new NotImplementedException();
        }

        Task<UserMaster> IRepository<UserMaster>.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<UserMaster>> IRepository<UserMaster>.GetAll()
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
