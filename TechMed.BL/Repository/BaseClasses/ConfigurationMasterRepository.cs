using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Enums;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.BaseClasses
{
    public class ConfigurationMasterRepository : IConfigurationMasterRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        public ConfigurationMasterRepository(TeleMedecineContext teleMedecineContext)
        {
            _teleMedecineContext = teleMedecineContext;
        }

        public async Task<string> GetValueByName(string ConfigurationName)
        {
            var configuration = await _teleMedecineContext.Configurations.Where(c => c.Name.ToLower() == ConfigurationName.ToLower()).FirstOrDefaultAsync();
            return configuration.Value;
        }
        public async Task<VideoCallEnvironment> GetVideoCallEnvironment()
        {
            var configuration = await _teleMedecineContext.Configurations.Where(c => c.Name.ToLower() == "VideoCallEnvironment".ToLower()).FirstOrDefaultAsync();
            switch (configuration.Value.ToLower())
            {
                case "twilio":
                    return VideoCallEnvironment.Twilio;
                case "zoom":
                    return VideoCallEnvironment.Zoom;
                case "inhouse":
                    return VideoCallEnvironment.InHouse;
            }
            return VideoCallEnvironment.Twilio;

        }
    }
}
