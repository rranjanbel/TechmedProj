using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IPHCRepository : IRepository<Phcmaster>
    {
        Task<Phcmaster> GetByID(int id);
        Task<Phcmaster> GetByPHCUserID(int userId);
        Task<PHCDetailsVM> GetPHCDetailByUserID(int userId);
        Task<PHCDetailsIdsVM> GetPHCDetailByEmailID(string email);
        Task<Phcmaster> AddPHCUser(Phcmaster phcmaster, UserMaster userMaster,string Password);
        Task<List<PHCMasterDTO>> GetAllPHC(int districtId);
        Task<bool> IsPHCExit(string name);
        Task<bool> IsUserMailExist(string email);
        bool PostSpokeMaintenance (SpokeMaintenanceDTO spokeDTO, string contentRootPath);
        Task<EmployeeTrainingDTO> AddEmployeeTraining(EmployeeTrainingDTO employeeTraining);
        public Task<List<SearchPHCDetailsIdsVM>> SearchPHCDetailByName(string email);
        public Task<List<string>> GetAllPHCName();
        public Task<SearchPHCDetailsIdsVM> GetPHCDetailByByName(string name);
        public Task<bool> UpdatePHCDetails(UpdatePHCDTO updatePHCDTO);
    }
}
