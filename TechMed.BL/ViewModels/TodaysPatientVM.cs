using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class TodaysPatientVM
    {
        public int ID { get; set; }
        public string PatientName { get; set; }
        public long PatientID { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string ReferredByPHCName { get; set; }
        public int ReferredByPHCID { get; set; }        
        public string DoctorName { get; set; }
        public int DocterID { get; set; }
        public string PHCUserName { get; set; }
        public int  PHCUserID { get; set; }
        public string CaseHeading { get; set; } 
        public DateTime DateOfRegistration { get; set; }

    }
}
