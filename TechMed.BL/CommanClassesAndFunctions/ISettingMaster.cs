using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.CommanClassesAndFunctions
{
    public interface ISettingMaster
    {
        long GetPatientNumber();
        long GetCaseFileNumber();
    }
}
