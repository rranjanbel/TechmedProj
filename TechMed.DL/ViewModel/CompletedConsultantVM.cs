using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class CompletedConsultantVM
    {
        public int SrNo { get; set; }
        public DateTime AssignedOn { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PHCName { get; set; }
        public string PHCTechnician { get; set; }
        public string Doctor { get; set; }
        public string PHCAddress { get; set; }
        public int CluserID { get; set; }
        public string Cluster { get; set; }
        public int BlockID { get; set; }
        public string BlockName { get; set; }
        public int PHCID { get; set; }
        public int CreatedBy { get; set; }
        public string PatientCreatedBy { get; set; }
    }
}
