using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.BL.ViewModels;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IDashBoardRepository
    {
        Task<List<DoctorDTO>> DoctorsLoggedInToday(DoctorsLoggedInTodayVM doctorsLoggedInTodayVM);
        Task<List<SpecializationReportVM>> GetTodaysRegistoredPatientList();
        Task<List<SpecializationReportVM>> GetTodaysConsultedPatientList();
        LoggedUserCountVM GetLoggedUserTypeCount(int usertTypeId);
        Task<List<LoggedUserCountVM>> GetTodaysLoggedUsersCount();
        Task<List<DashboardConsultationVM>> GetDashboardConsultation(GetDashboardConsultationVM getDashboardConsultationVM);


    }
}
