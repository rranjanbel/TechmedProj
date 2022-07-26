using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    public class RemovePatientFromQueueVM
    {
        public long PatientCaseID { get; set; }
        public int PatientID { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }

    }
}
