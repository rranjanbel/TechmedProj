using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    public class PatientsVM
    {
        public int SrNo { get; set; }
        public string PatientName { get; set; }
        public string AgeAndSex { get; set; }
        public string PHCName { get; set; }
        public string CaseTitle { get; set; }
        public TimeOnly RegistrationTime { get; set; }
        public TimeOnly LastRefTime { get; set; }
        public int PatientId { get; set; }
    }
}
