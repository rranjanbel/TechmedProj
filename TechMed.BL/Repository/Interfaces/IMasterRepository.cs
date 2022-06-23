using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IMasterRepository 
    {
        Task<List<DivisionDTO>> GetAllDivision();
        Task<List<DivisionDTO>> GetDivisionByClusterID(int clusterId);
        Task<DivisionDTO> GetDivisionById(int Id);
        Task<List<DistrictMasterDTO>> GetDistrictsByDivisionID(int divisionId);
        Task<List<BlockMasterDTO>> GetBlocksByDistrictID(int districtId);
    }
}
