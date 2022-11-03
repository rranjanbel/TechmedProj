using Microsoft.AspNetCore.Http;
using TechMed.BL.DTOMaster;
using TechMed.BL.ViewModels;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IReportService
    {
        public Task<ReportDTO> GeneratePdfReport(Int64 PatientCaseID, string contentRootPath);
    }
}
