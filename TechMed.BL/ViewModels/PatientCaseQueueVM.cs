using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class PatientCaseQueueVM
    {
        public int PatientCaseID { get; set; }
        public int DoctorID { get; set; }
        public string DocterName { get; set; }
        public string Specialization { get; set; }
        public string Qualification { get; set; }
        public int AssignedBy { get; set; }
        public string AssigneeName { get; set; }
        public int CaseFileStatusID { get; set; }
        public string CaseStatus { get; set; }
        public DateTime AssignedOn { get; set; }
        public DateTime StatusOn { get; set; }
    }
}
