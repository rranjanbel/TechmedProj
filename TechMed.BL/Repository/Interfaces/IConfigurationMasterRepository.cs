using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.DL.Enums;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IConfigurationMasterRepository
    {
        public Task<string> GetValueByName(string ConfigurationName);
        public Task<VideoCallEnvironment> GetVideoCallEnvironment();
    }
}
