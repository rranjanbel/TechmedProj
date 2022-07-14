using Microsoft.AspNetCore.Http;
using TechMed.BL.ViewModels;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IReportService
    {
        public Task<byte[]> GeneratePdfReport(Int64 PatientCaseID, string contentRootPath);
    }
}
