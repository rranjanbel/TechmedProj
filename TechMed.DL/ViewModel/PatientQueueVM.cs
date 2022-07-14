using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class PatientQueueVM
    {
        public long SrNo { get; set; }
        public string Patient { get; set; }
        public int PatientID { get; set; }
        public string CaseHeading { get; set; }
        public string Doctor { get; set; }
        public string Specialization { get; set; }
        public string Gender { get; set; }
        public long WaitList { get; set; }
       
    }
}
