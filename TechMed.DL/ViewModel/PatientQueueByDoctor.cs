using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class PatientQueueByDoctor
    {
        public long SrNo { get; set; }
        public int NoOfPatientInQueue { get; set; }
        public string Doctor { get; set; }
        public int DoctorID { get; set; }
        public bool AddToQueue { get; set; }
    }
}
