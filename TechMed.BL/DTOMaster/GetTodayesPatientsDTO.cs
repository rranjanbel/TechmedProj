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
        public string PatientName { get; set; }
        public string PatientID { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string PhoneNumber { get; set; }
        public string ReferredbyPHCName { get; set; }
        public string Casefile { get; set; }
        public string status { get; set; }
    }
}
