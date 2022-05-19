using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class PatientCaseQueDetail
    {
        public string PHCName { get; set; }
        public string PatientName { get; set; }        
        public string Docter { get; set; }
        public string Cluster { get; set; }
        public string BlockName { get; set; }
        public int PHCID { get; set; }
        public string PatientCreatedBy { get; set; }
        public long PatientCaseID { get; set; }
        public long PatientQueueId { get; set; }
        public DateTime AssignedOn { get; set; }
        public int PatientID { get; set; }
        public int DoctorUserID { get; set; }

    }
}
