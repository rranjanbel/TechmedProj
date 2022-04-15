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
            CreateMap<SpecializationMaster, SpecializationDTO>().ReverseMap();
            CreateMap<SubSpecializationMaster, SubSpecializationDTO>().ReverseMap();
            CreateMap<PatientCaseDocument, PatientCaseDocDTO>().ReverseMap();
            CreateMap<PatientCase, PatientCaseDTO>().ReverseMap();
        }
    }
}
