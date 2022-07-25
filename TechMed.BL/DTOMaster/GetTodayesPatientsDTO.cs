using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class GetTodayesPatientsDTO
    {
        //PatientName
        //PatientID
        //Gender
        //Age
        //PhoneNumber
        //ReferredbyPHCName
        //Casefile 
        //status(Queued or Pending). 
        public int id { get; set; }
        public long PatientCaseID { get; set; }
        public string PatientName { get; set; }
        public Int64 PatientID { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string PhoneNumber { get; set; }
        public string ReferredbyPHCName { get; set; }
        public string CaseHeading { get; set; }
        public string status { get; set; }
        public bool canCallInitiate { get; set; }
        public DateTime? DateOfRegistration { get; set; }
        public string CaseFileNumber { get; set; }  

    }
}
