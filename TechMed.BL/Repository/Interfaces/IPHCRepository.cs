using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        Task<Phcmaster> AddPHCUser(Phcmaster phcmaster, UserMaster userMaster);
        bool IsPHCExit(string name);
    }
}
