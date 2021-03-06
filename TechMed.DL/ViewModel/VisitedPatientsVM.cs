using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class VisitedPatientsVM
    {        
        public string PatientName { get; set; }
        public string Age { get; set; }
        public int ID { get; set; }
        public long PatientID { get; set; }
        public int Phcid { get; set; }
        public string Phcname { get; set; }
        public string PhoneNumber { get; set; }
        public int DocterID { get; set; }
        public string? Doctor { get; set; }
        public long PatientCaseID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ReferredBy { get; set; }
        public string Gender { get; set; }
        public string CaseHeading { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string CaseFileNumber { get; set; }
    }
}
