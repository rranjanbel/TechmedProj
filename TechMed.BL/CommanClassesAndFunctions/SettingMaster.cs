using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.DL.Models;

namespace TechMed.BL.CommanClassesAndFunctions
{
    public class SettingMaster
    {
        public TeleMedecineContext _teleMedecineContext;
        public SettingMaster(TeleMedecineContext teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
        }
        public int GetPatientNumber()
        {           
            return 0;
        }
    }
}
