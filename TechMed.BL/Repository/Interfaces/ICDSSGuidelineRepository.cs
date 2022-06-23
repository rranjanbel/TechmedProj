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
    public interface ICDSSGuidelineRepository : IRepository<CDSSGuideline>
    {
        Task<List<CDSSGuidelineVM>> GetAllCDSSGuideline();
        Task<List<CDSSGuidelineVM>> GetAllCDSSGuidelineByDiseases(string Diseases);
        Task<List<CDSSGuidelineVM>> GetCDSSGuideLinesByDiseasesAndAge(string Diseases,int Age);
        Task<List<CDSSGuidelineDiseasesVM>> GetCDSSGuideLinesDiseasesByDiseasesAndAge(string Diseases, int Age);
        Task<List<CDSSGuidelineVM>> GetDiseases(string Diseases);
        Task<CDSSGuidelineVM> GetAllCDSSGuidelineByID(Int64 ID);
    }
}
