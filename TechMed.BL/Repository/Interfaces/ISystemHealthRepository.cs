using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.Repository.Interfaces
{
    public interface ISystemHealthRepository
    {
        public Task<bool> GetAPIStatus(string APIHost);
        public Task<bool> GetANGStatus(string ANGHost);
        public Task<bool> SaveStatusInDB(string APIHost, string ANGHost);
        public Task<bool> UpdateLogout();
        public Task<bool> UpdateYesterdayPedingCaseToOrphan();

    }
}
