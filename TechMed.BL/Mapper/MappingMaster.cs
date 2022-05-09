using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TechMed.BL.DTOMaster;
using TechMed.DL.Models;

namespace TechMed.BL.Mapper
{
    public class MappingMaster : Profile
    {
        public MappingMaster()
        {
            CreateMap<UserMaster, UserLoginDTO>().ReverseMap();
            CreateMap<Notification, NotificationDTO>().ReverseMap();
            CreateMap<CdssguidelineMaster, CdssguidelineMasterDTO>().ReverseMap();
            CreateMap<PatientMaster, PatientMasterDTO>().ReverseMap();
            CreateMap<DoctorMaster, DoctorDTO>().ReverseMap();
            CreateMap<UserDetail, DetailsDTO>().ReverseMap();
            CreateMap<MedicineMaster, MedicineMasterDTO>().ReverseMap();
            CreateMap<VitalMaster, VitalMasterDTO>().ReverseMap();
            CreateMap<Phcmaster, PHCHospitalDTO>().ReverseMap();
            CreateMap<PatientCaseDocument, PatientCaseDocDTO>().ReverseMap();
            CreateMap<PatientCase, PatientCaseDTO>().ReverseMap();
            CreateMap<PatientCaseFeedback, PatientFeedbackDTO>().ReverseMap();
            CreateMap<PatientCaseMedicine, PatientCaseMedicineDTO>().ReverseMap();

            //Master Mapping
            CreateMap<SpecializationMaster, SpecializationDTO>().ReverseMap();
            CreateMap<SubSpecializationMaster, SubSpecializationDTO>().ReverseMap();
            CreateMap<CaseFileStatusMaster, CaseFileStatusMasterDTO>().ReverseMap();
            CreateMap<CountryMaster, CountryMasterDTO>().ReverseMap();
            CreateMap<DistrictMaster, DistrictMasterDTO>().ReverseMap();
            CreateMap<GenderMaster, GenderMasterDTO>().ReverseMap();
            CreateMap<IdproofTypeMaster, IDProofTypeMasterDTO>().ReverseMap();
            CreateMap<TitleMaster, TitleMasterDTO>().ReverseMap();
            CreateMap<UserTypeMaster, UserTypeMasterDTO>().ReverseMap();
            CreateMap<ClusterMaster, ClusterMasterDTO>().ReverseMap();
            CreateMap<ZoneMaster, ZoneMasterDTO>().ReverseMap();
            CreateMap<StateMaster, StateMasterDTO>().ReverseMap();
            CreateMap<PatientStatusMaster, PatientStatusMastersDTO>().ReverseMap();
            CreateMap<MaritalStatus, MaritalStatusDTO>().ReverseMap();


        }
    }
}
