using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.Repository.Interfaces
{
    public interface ISystemHealthRepository
    {
        public Task<bool> GetAPIStatus();
        public Task<bool> GetANGStatus();
        public Task<bool> SaveStatusInDB(string APIHost, string ANGHost);
    }
}
