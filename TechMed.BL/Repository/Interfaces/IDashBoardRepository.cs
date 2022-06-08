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
        List<PHCLoginHistoryReportVM> GetPHCLoginHistoryReport(int PHCId, DateTime? fromDate, DateTime? toDate);
        List<PHCConsultationVM> GetPHCConsultationReport(int PHCId, DateTime? fromDate, DateTime? toDate);
        Task<List<DashboardReportSummaryVM>> GetDashboardReportSummary(GetDashboardReportSummaryVM getDashboardReportSummaryVM);
        Task<List<DashboardReportSummaryVM>> GetDashboardReportSummaryMonthly(GetDashboardReportSummaryMonthVM getDashboardReportSummaryMonthVM);
        Task<List<DashboardReportConsultationVM>> GetDashboardReportConsultation(GetDashboardReportConsultationVM getDashboardReportSummaryVM);
        PHCMainpowerResultSetVM GetPHCManpowerReport(int year, int month);
        List<RegisterPatientVM> GetRegisterPatientReport(DateTime? fromDate, DateTime? toDate);
        Task<EquipmentUptimeReportDTO> AddEquipmentUptimeReport(EquipmentUptimeReportDTO equipmentUptimeReport);

        List<GetReferredPatientVM> GetReferredPatientReport(DateTime? fromDate, DateTime? toDate);
        List<GetReviewPatientVM> GetReviewPatientReport(DateTime? fromDate, DateTime? toDate);

        List<GetDashboardSpokeMaintenanceVM> GetDashboardSpokeMaintenance(DateTime? fromDate, DateTime? toDate);
        List<GetDashboardEmployeeFeedbackVM> GetDashboardEmployeeFeedback(int? Fromyear,string qtr);

        List<GetDashboardEquipmentUptimeReportVM> GetDashboardEquipmentUptimeReport(int month, int year);
        List<GetDashboardAppointmentVM> GetDashboardAppointment(DateTime? fromDate, DateTime? toDate);

        List<GetDashboardDoctorAvgTimeVM> GetDashboardDoctorAvgTime(DateTime? fromDate, DateTime? toDate);
        List<GetDashboardDoctorAvailabilityVM> GetDashboardDoctorAvailability(DateTime? fromDate, DateTime? toDate);
        List<GetDashboardEquipmentHeaderReportVM> GetDashboardEquipmentHeaderReport(int month, int year);
        Task<List<PrescribedMedicineVM>> GetPrescribedMedicineList(DateTime? fromDate, DateTime? toDate);
        Task<List<PrescribedMedicinePHCWiseVM>> GetPrescribedMedicinePHCWiseList(DateTime? fromDate, DateTime? toDate);
        Task<List<GetDashboardDiagnosticPrescribedTestWiseVM>> GetDashboardDiagnosticPrescribedTestWise(DateTime? fromDate, DateTime? toDate);
        Task<List<GetDashboardDiagnosticPrescribedPHCWiseVM>> GetDashboardDiagnosticPrescribedPHCWise(DateTime? fromDate, DateTime? toDate);
        Task<List<GetDashboardGraphVM>> GetDashboardGraph();
    }
}
