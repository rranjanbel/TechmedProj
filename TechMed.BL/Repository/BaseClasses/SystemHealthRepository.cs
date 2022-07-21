using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.BaseClasses
{
    public class SystemHealthRepository : ISystemHealthRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        public SystemHealthRepository(TeleMedecineContext teleMedecineContext)
        {
            _teleMedecineContext = teleMedecineContext;
        }

        public Task<bool> GetANGStatus()
        {
            var v = _teleMedecineContext.VitalMasters.ToList();
            throw new NotImplementedException();
        }

        public Task<bool> GetAPIStatus()
        {
            throw new NotImplementedException();
        }
    }
}
