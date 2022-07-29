using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class AllPendingPatient
    {  
        public long SrNo { get; set; }
        public string Patient { get; set; }
        public int CaseFileStatusID { get; set; }
        public string CaseHeading { get; set; }
        public int CaseStatusID { get; set; }
        public DateTime CreatedOn { get; set; }
        public long PatientCaseID { get; set; }       
        public int PatientID { get; set; }
        public string PHCName { get; set; }
        public long RegID { get; set; }        
        public string Specialization { get; set; }
        public int PHCID { get; set; }
    }
}
