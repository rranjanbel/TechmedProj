using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class PatientReferToDoctorVM
    {       
        public long PatientCaseID { get; set; }
        public int AssignedDocterID { get; set; }
        public int PHCID { get; set; }
    }
}
